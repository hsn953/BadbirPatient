using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Services;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Manages patient profiles. All endpoints require authentication.
/// Patients may only access their own record; Admins/Clinicians can query any.
/// </summary>
[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly BadbirDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEncryptionService _enc;

    public PatientsController(BadbirDbContext db, UserManager<ApplicationUser> userManager, IEncryptionService enc)
    {
        _db = db;
        _userManager = userManager;
        _enc = enc;
    }

    // GET api/patients/me
    /// <summary>Returns the patient profile for the currently authenticated user.</summary>
    [HttpGet("me")]
    public async Task<ActionResult<PatientDto>> GetMyProfile()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return Unauthorized();

        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user?.PatientId is null)
            return NotFound();

        var patient = await _db.BbPatients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Patientid == user.PatientId);

        if (patient is null)
            return NotFound();

        _enc.DecryptPatient(patient);
        return Ok(MapToDto(patient));
    }

    // GET api/patients/{patientId}
    /// <summary>Returns a patient profile by patientid. Requires Clinician or Administrator role.</summary>
    [HttpGet("{patientId:int}")]
    [Authorize(Roles = "Clinician,Administrator")]
    public async Task<ActionResult<PatientDto>> GetById(int patientId)
    {
        var patient = await _db.BbPatients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Patientid == patientId);

        if (patient is null)
            return NotFound();

        _enc.DecryptPatient(patient);
        return Ok(MapToDto(patient));
    }

    // GET api/patients/{patientId}/cohort-history
    /// <summary>Returns cohort history records for a patient. Requires Clinician or Administrator role.</summary>
    [HttpGet("{patientId:int}/cohort-history")]
    [Authorize(Roles = "Clinician,Administrator")]
    public async Task<ActionResult<IEnumerable<CohortHistoryDto>>> GetCohortHistory(int patientId)
    {
        var exists = await _db.BbPatients.AnyAsync(p => p.Patientid == patientId);
        if (!exists)
            return NotFound();

        var history = await _db.BbPatientCohortHistories
            .AsNoTracking()
            .Where(h => h.Patientid == patientId)
            .Select(h => new CohortHistoryDto
            {
                Chid       = h.Chid,
                Patientid  = h.Patientid,
                Cohortid   = h.Cohortid,
                Studyno    = h.Studyno,
                Datefrom   = h.Datefrom
            })
            .ToListAsync();

        return Ok(history);
    }

    // ── Mapping helper ────────────────────────────────────────────────────────
    private static PatientDto MapToDto(BbPatient p) => new()
    {
        Patientid    = p.Patientid,
        Phrn         = p.Phrn,
        Pnhs         = p.Pnhs,
        Title        = p.Title,
        Forenames    = p.Forenames,
        Surname      = p.Surname,
        Dateofbirth  = p.Dateofbirth,
        Genderid     = p.Genderid,
        Statusid     = p.Statusid
    };
}
