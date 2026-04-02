using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Shared.Constants;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Records informed consent submissions from patients.
/// </summary>
[ApiController]
[Route("api/consent")]
[Authorize]
public class ConsentController : ControllerBase
{
    private readonly BadbirDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConsentController(BadbirDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db          = db;
        _userManager = userManager;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/consent
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Records the patient's informed consent.
    /// Signature can be electronic (typed name) or drawn (base64 canvas PNG).
    /// Stores IP address and user-agent for audit purposes.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<ConsentResultDto>> SubmitConsent([FromBody] ConsentSubmitDto dto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

        var record = new ConsentRecord
        {
            UserId              = userId,
            ConsentFormVersion  = dto.ConsentFormVersion,
            ConsentTimestamp    = DateTime.UtcNow,
            IPAddress           = ipAddress,
            UserAgent           = userAgent,
            SignatureType       = dto.SignatureType,
            SignatureData       = dto.SignatureData
        };

        _db.ConsentRecords.Add(record);

        // Mark user as having consented and record the version
        user.ConsentGiven   = true;
        user.ConsentVersion = dto.ConsentFormVersion;

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConsentStatus), null, new ConsentResultDto
        {
            ConsentId          = record.ConsentId,
            ConsentTimestamp   = record.ConsentTimestamp,
            ConsentFormVersion = record.ConsentFormVersion
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/consent/status
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns whether the patient has given consent for the current form version.
    /// </summary>
    [HttpGet("status")]
    public async Task<IActionResult> GetConsentStatus()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var latest = await _db.ConsentRecords
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.ConsentTimestamp)
            .FirstOrDefaultAsync();

        var hasCurrentConsent = latest != null &&
                                latest.ConsentFormVersion == AppConstants.ConsentFormVersion;

        return Ok(new
        {
            consentGiven       = hasCurrentConsent,
            currentVersion     = AppConstants.ConsentFormVersion,
            lastConsentVersion = latest?.ConsentFormVersion,
            lastConsentDate    = latest?.ConsentTimestamp
        });
    }
}
