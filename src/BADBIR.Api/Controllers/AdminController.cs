using BADBIR.Api.Data;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Internal administrative endpoints called by the Clinician System.
/// </summary>
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Clinician,Administrator")]
public class AdminController : ControllerBase
{
    private readonly BadbirDbContext _db;

    public AdminController(BadbirDbContext db) => _db = db;

    // GET api/admin/visits/pending
    /// <summary>Returns all visits with DataStatus=0 (awaiting clinician review).</summary>
    [HttpGet("visits/pending")]
    public async Task<ActionResult<IEnumerable<PendingVisitDto>>> GetPendingVisits()
    {
        var visits = await _db.VisitTrackings
            .AsNoTracking()
            .Where(v => v.DataStatus == 0)
            .Select(v => new PendingVisitDto
            {
                VisitId            = v.VisitId,
                UserId             = v.UserId,
                ClinicianPatientId = v.ClinicianPatientId,
                PotentialFupCode   = v.PotentialFupCode,
                VisitDate          = v.VisitDate,
                VisitStatus        = v.VisitStatus
            })
            .ToListAsync();

        return Ok(visits);
    }

    // POST api/admin/visits/{visitId}/approve
    /// <summary>
    /// Marks a visit as approved (DataStatus=1).
    /// Called by the Clinician System after data review.
    /// The optional ImportedFupId links this visit to the Clinician System's FupId.
    /// </summary>
    [HttpPost("visits/{visitId:int}/approve")]
    public async Task<IActionResult> ApproveVisit(int visitId, [FromBody] ApproveVisitDto request)
    {
        var visit = await _db.VisitTrackings.FirstOrDefaultAsync(v => v.VisitId == visitId && v.DataStatus == 0);
        if (visit is null)
            return NotFound(new { error = $"No pending visit found with id {visitId}." });

        visit.DataStatus      = 1;
        visit.ImportedFupId   = request.ImportedFupId;
        visit.LastUpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(new { visitId = visit.VisitId, status = "Approved", importedFupId = visit.ImportedFupId });
    }

    // POST api/admin/visits/{visitId}/reject
    /// <summary>Marks a visit as rejected (DataStatus=2).</summary>
    [HttpPost("visits/{visitId:int}/reject")]
    public async Task<IActionResult> RejectVisit(int visitId, [FromBody] RejectRequestDto request)
    {
        var visit = await _db.VisitTrackings.FirstOrDefaultAsync(v => v.VisitId == visitId && v.DataStatus == 0);
        if (visit is null)
            return NotFound(new { error = $"No pending visit found with id {visitId}." });

        visit.DataStatus      = 2;
        visit.LastUpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Ok(new { visitId = visit.VisitId, status = "Rejected", reason = request.Reason });
    }
}
