using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Handles PRO form submissions and retrieval.
/// A patient may only submit/view their own forms.
/// </summary>
[ApiController]
[Route("api/forms")]
[Authorize]
public class FormsController : ControllerBase
{
    private readonly BadbirDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FormsController(BadbirDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // EuroQol EQ-5D-5L
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/euroqol
    /// <summary>Returns all EQ-5D-5L submissions for the authenticated patient.</summary>
    [HttpGet("euroqol")]
    public async Task<ActionResult<IEnumerable<EuroQolSubmissionDto>>> GetEuroQol()
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        var submissions = await _db.EuroQolSubmissions
            .AsNoTracking()
            .Where(e => e.PatientId == patientId)
            .OrderByDescending(e => e.SubmittedAt)
            .Select(e => MapEuroQol(e))
            .ToListAsync();

        return Ok(submissions);
    }

    // POST api/forms/euroqol
    /// <summary>Submits a new EQ-5D-5L form for the authenticated patient.</summary>
    [HttpPost("euroqol")]
    public async Task<ActionResult<EuroQolSubmissionDto>> SubmitEuroQol(EuroQolSubmitDto dto)
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        var entity = new EuroQolSubmission
        {
            PatientId          = patientId.Value,
            SubmittedAt        = DateTime.UtcNow,
            Mobility           = dto.Mobility,
            SelfCare           = dto.SelfCare,
            UsualActivities    = dto.UsualActivities,
            PainDiscomfort     = dto.PainDiscomfort,
            AnxietyDepression  = dto.AnxietyDepression,
            VasScore           = dto.VasScore,
            Notes              = dto.Notes
        };

        _db.EuroQolSubmissions.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEuroQol), MapEuroQol(entity));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // HAQ
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/haq
    /// <summary>Returns all HAQ-DI submissions for the authenticated patient.</summary>
    [HttpGet("haq")]
    public async Task<ActionResult<IEnumerable<HaqSubmissionDto>>> GetHaq()
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        var submissions = await _db.HaqSubmissions
            .AsNoTracking()
            .Where(h => h.PatientId == patientId)
            .OrderByDescending(h => h.SubmittedAt)
            .Select(h => MapHaq(h))
            .ToListAsync();

        return Ok(submissions);
    }

    // POST api/forms/haq
    /// <summary>Submits a new HAQ-DI form for the authenticated patient.</summary>
    [HttpPost("haq")]
    public async Task<ActionResult<HaqSubmissionDto>> SubmitHaq(HaqSubmitDto dto)
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        var entity = new HaqSubmission
        {
            PatientId           = patientId.Value,
            SubmittedAt         = DateTime.UtcNow,
            Dressing            = dto.Dressing,
            Arising             = dto.Arising,
            Eating              = dto.Eating,
            Walking             = dto.Walking,
            Hygiene             = dto.Hygiene,
            Reach               = dto.Reach,
            Grip                = dto.Grip,
            Activities          = dto.Activities,
            UsesDressingAids    = dto.UsesDressingAids,
            UsesArisingAids     = dto.UsesArisingAids,
            UsesEatingAids      = dto.UsesEatingAids,
            UsesWalkingAids     = dto.UsesWalkingAids,
            UsesHygieneAids     = dto.UsesHygieneAids,
            UsesReachAids       = dto.UsesReachAids,
            UsesGripAids        = dto.UsesGripAids,
            UsesActivitiesAids  = dto.UsesActivitiesAids,
            Notes               = dto.Notes,
            HaqDiScore          = CalculateHaqDi(dto)
        };

        _db.HaqSubmissions.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHaq), MapHaq(entity));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // HADS
    // ─────────────────────────────────────────────────────────────────────────

    // GET api/forms/hads
    /// <summary>Returns all HADS submissions for the authenticated patient.</summary>
    [HttpGet("hads")]
    public async Task<ActionResult<IEnumerable<HadsSubmissionDto>>> GetHads()
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        var submissions = await _db.HadsSubmissions
            .AsNoTracking()
            .Where(h => h.PatientId == patientId)
            .OrderByDescending(h => h.SubmittedAt)
            .Select(h => MapHads(h))
            .ToListAsync();

        return Ok(submissions);
    }

    // POST api/forms/hads
    /// <summary>Submits a new HADS form for the authenticated patient.</summary>
    [HttpPost("hads")]
    public async Task<ActionResult<HadsSubmissionDto>> SubmitHads(HadsSubmitDto dto)
    {
        var patientId = await GetCallerPatientIdAsync();
        if (patientId is null) return Forbid();

        byte anxietyScore    = (byte)(dto.A1 + dto.A2 + dto.A3 + dto.A4 + dto.A5 + dto.A6 + dto.A7);
        byte depressionScore = (byte)(dto.D1 + dto.D2 + dto.D3 + dto.D4 + dto.D5 + dto.D6 + dto.D7);

        var entity = new HadsSubmission
        {
            PatientId        = patientId.Value,
            SubmittedAt      = DateTime.UtcNow,
            A1 = dto.A1, A2 = dto.A2, A3 = dto.A3, A4 = dto.A4,
            A5 = dto.A5, A6 = dto.A6, A7 = dto.A7,
            D1 = dto.D1, D2 = dto.D2, D3 = dto.D3, D4 = dto.D4,
            D5 = dto.D5, D6 = dto.D6, D7 = dto.D7,
            AnxietyScore     = anxietyScore,
            DepressionScore  = depressionScore,
            Notes            = dto.Notes
        };

        _db.HadsSubmissions.Add(entity);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHads), MapHads(entity));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Private helpers
    // ─────────────────────────────────────────────────────────────────────────

    private async Task<int?> GetCallerPatientIdAsync()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return null;

        var patient = await _db.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);

        return patient?.PatientId;
    }

    private static decimal CalculateHaqDi(HaqSubmitDto dto) =>
        Math.Round(
            (dto.Dressing + dto.Arising + dto.Eating + dto.Walking +
             dto.Hygiene  + dto.Reach   + dto.Grip   + dto.Activities) / 8m,
            3);

    private static EuroQolSubmissionDto MapEuroQol(EuroQolSubmission e) => new()
    {
        SubmissionId       = e.SubmissionId,
        PatientId          = e.PatientId,
        SubmittedAt        = e.SubmittedAt,
        Mobility           = e.Mobility,
        SelfCare           = e.SelfCare,
        UsualActivities    = e.UsualActivities,
        PainDiscomfort     = e.PainDiscomfort,
        AnxietyDepression  = e.AnxietyDepression,
        VasScore           = e.VasScore,
        IndexValue         = e.IndexValue,
        Notes              = e.Notes
    };

    private static HaqSubmissionDto MapHaq(HaqSubmission h) => new()
    {
        SubmissionId        = h.SubmissionId,
        PatientId           = h.PatientId,
        SubmittedAt         = h.SubmittedAt,
        Dressing            = h.Dressing,
        Arising             = h.Arising,
        Eating              = h.Eating,
        Walking             = h.Walking,
        Hygiene             = h.Hygiene,
        Reach               = h.Reach,
        Grip                = h.Grip,
        Activities          = h.Activities,
        HaqDiScore          = h.HaqDiScore,
        UsesDressingAids    = h.UsesDressingAids,
        UsesArisingAids     = h.UsesArisingAids,
        UsesEatingAids      = h.UsesEatingAids,
        UsesWalkingAids     = h.UsesWalkingAids,
        UsesHygieneAids     = h.UsesHygieneAids,
        UsesReachAids       = h.UsesReachAids,
        UsesGripAids        = h.UsesGripAids,
        UsesActivitiesAids  = h.UsesActivitiesAids,
        Notes               = h.Notes
    };

    private static HadsSubmissionDto MapHads(HadsSubmission h) => new()
    {
        SubmissionId     = h.SubmissionId,
        PatientId        = h.PatientId,
        SubmittedAt      = h.SubmittedAt,
        A1 = h.A1, A2 = h.A2, A3 = h.A3, A4 = h.A4,
        A5 = h.A5, A6 = h.A6, A7 = h.A7,
        D1 = h.D1, D2 = h.D2, D3 = h.D3, D4 = h.D4,
        D5 = h.D5, D6 = h.D6, D7 = h.D7,
        AnxietyScore     = h.AnxietyScore,
        DepressionScore  = h.DepressionScore,
        Notes            = h.Notes
    };
}
