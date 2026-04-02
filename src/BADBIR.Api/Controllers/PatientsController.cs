using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Manages patient profiles. All endpoints require authentication.
/// </summary>
[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly BadbirDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatientsController(BadbirDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // GET api/patients/me
    /// <summary>Returns the patient profile for the currently authenticated user.</summary>
    [HttpGet("me")]
    public async Task<ActionResult<PatientProfileDto>> GetMyProfile()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        return Ok(new PatientProfileDto
        {
            UserId             = user.Id,
            Email              = user.Email ?? string.Empty,
            ClinicianPatientId = user.ClinicianPatientId
        });
    }
}
