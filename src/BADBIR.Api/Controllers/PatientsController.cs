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
/// Patients may only access their own record; Admins/Clinicians can query any.
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
    public async Task<ActionResult<PatientDto>> GetMyProfile()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return Unauthorized();

        var patient = await _db.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);

        return patient is null
            ? NotFound()
            : Ok(MapToDto(patient));
    }

    // GET api/patients/{id}
    /// <summary>Returns a patient profile by ID. Requires Clinician or Administrator role.</summary>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Clinician,Administrator")]
    public async Task<ActionResult<PatientDto>> GetById(int id)
    {
        var patient = await _db.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PatientId == id && !p.IsDeleted);

        return patient is null ? NotFound() : Ok(MapToDto(patient));
    }

    // GET api/patients/{id}/diagnoses
    /// <summary>Returns diagnoses for a patient.</summary>
    [HttpGet("{id:int}/diagnoses")]
    [Authorize(Roles = "Clinician,Administrator")]
    public async Task<ActionResult<IEnumerable<PatientDiagnosisDto>>> GetDiagnoses(int id)
    {
        var exists = await _db.Patients.AnyAsync(p => p.PatientId == id && !p.IsDeleted);
        if (!exists)
            return NotFound();

        var diagnoses = await _db.PatientDiagnoses
            .AsNoTracking()
            .Where(d => d.PatientId == id)
            .Select(d => new PatientDiagnosisDto
            {
                DiagnosisId   = d.DiagnosisId,
                DiagnosisCode = d.DiagnosisCode,
                DiagnosisName = d.DiagnosisName,
                DiagnosedDate = d.DiagnosedDate,
                IsActive      = d.IsActive
            })
            .ToListAsync();

        return Ok(diagnoses);
    }

    // ── Mapping helper ────────────────────────────────────────────────────────
    private static PatientDto MapToDto(Patient p) => new()
    {
        PatientId   = p.PatientId,
        NhsNumber   = p.NhsNumber,
        FirstName   = p.FirstName,
        LastName    = p.LastName,
        DateOfBirth = p.DateOfBirth,
        Gender      = p.Gender,
        Ethnicity   = p.Ethnicity
    };
}
