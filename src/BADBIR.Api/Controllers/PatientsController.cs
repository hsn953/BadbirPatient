using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Shared.Constants;
using BADBIR.Shared.DTOs;
using BADBIR.Shared.Enums;
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

    // ─────────────────────────────────────────────────────────────────────────
    // GET api/patients/me
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>Returns the patient profile for the currently authenticated user.</summary>
    [HttpGet("me")]
    public async Task<ActionResult<PatientProfileDto>> GetMyProfile()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        return Ok(MapToProfile(user));
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET api/patients/me/dashboard
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the full dashboard state: registration status, consent/diagnosis flags,
    /// current follow-up number, form list with statuses, and next visit date.
    /// </summary>
    [HttpGet("me/dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        // Determine holding days remaining
        int? holdingDaysRemaining = null;
        if (user.RegistrationStatus == RegistrationStatus.Holding && user.HoldingExpiry.HasValue)
        {
            var remaining = (int)Math.Ceiling((user.HoldingExpiry.Value - DateTime.UtcNow).TotalDays);
            holdingDaysRemaining = Math.Max(0, remaining);
        }

        // Load the most recent visit (current follow-up)
        var latestVisit = await _db.VisitTrackings
            .AsNoTracking()
            .Include(v => v.Lifestyle)
            .Where(v => v.UserId == userId)
            .OrderByDescending(v => v.DateEntered)
            .FirstOrDefaultAsync();

        var followUpNumber = latestVisit?.PotentialFupCode ?? 0;
        var followUpLabel  = followUpNumber == 0 ? "Baseline" : $"Follow-Up {followUpNumber}";
        var nextVisitDate  = latestVisit?.VisitDate;

        // Check consent
        var hasCurrentConsent = await _db.ConsentRecords.AnyAsync(c =>
            c.UserId == userId &&
            c.ConsentFormVersion == AppConstants.ConsentFormVersion);

        // Build form list
        var forms = BuildFormList(followUpNumber, user, latestVisit);

        return Ok(new DashboardDto
        {
            Email                = user.Email ?? string.Empty,
            RegistrationStatus   = user.RegistrationStatus,
            HoldingExpiry        = user.HoldingExpiry,
            HoldingDaysRemaining = holdingDaysRemaining,
            ClinicalCentre       = user.ClinicalCentre,
            FollowUpNumber       = followUpNumber,
            FollowUpLabel        = followUpLabel,
            NextVisitDate        = nextVisitDate,
            ConsentRequired      = !hasCurrentConsent,
            DiagnosisRequired    = user.SelfReportedDiagnosis is null,
            Forms                = forms
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // PATCH api/patients/me/diagnosis
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Records the patient's self-reported inflammatory arthritis diagnosis.
    /// Determines whether the HAQ form is included in their follow-up sequence.
    /// </summary>
    [HttpPatch("me/diagnosis")]
    public async Task<IActionResult> UpdateDiagnosis([FromBody] DiagnosisUpdateDto dto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        user.SelfReportedDiagnosis = dto.SelfReportedDiagnosis;
        await _db.SaveChangesAsync();

        return Ok(new { selfReportedDiagnosis = user.SelfReportedDiagnosis });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // PATCH api/patients/me/preferences
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Updates the patient's notification preferences.
    /// Email remains mandatory and cannot be disabled here.
    /// </summary>
    [HttpPatch("me/preferences")]
    public async Task<IActionResult> UpdatePreferences([FromBody] PreferencesUpdateDto dto)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return NotFound();

        user.NotifyReminders = dto.NotifyReminders;
        user.NotifyInfoComms = dto.NotifyInfoComms;
        await _db.SaveChangesAsync();

        return Ok(new { notifyReminders = user.NotifyReminders, notifyInfoComms = user.NotifyInfoComms });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Helpers
    // ─────────────────────────────────────────────────────────────────────────

    private static PatientProfileDto MapToProfile(ApplicationUser user) => new()
    {
        UserId                = user.Id,
        Email                 = user.Email ?? string.Empty,
        ClinicianPatientId    = user.ClinicianPatientId,
        RegistrationStatus    = user.RegistrationStatus,
        HoldingExpiry         = user.HoldingExpiry,
        ConsentGiven          = user.ConsentGiven,
        SelfReportedDiagnosis = user.SelfReportedDiagnosis,
        DiagnosisConfirmedIA  = user.DiagnosisConfirmedIA,
        NotifyReminders       = user.NotifyReminders,
        NotifyInfoComms       = user.NotifyInfoComms,
        ClinicalCentre        = user.ClinicalCentre
    };

    private static List<FormStatusItemDto> BuildFormList(
        int followUpNumber, ApplicationUser user, VisitTracking? latestVisit)
    {
        // Determine whether the HAQ condition is met
        var haqConditionMet = user.DiagnosisConfirmedIA == true ||
                              (user.DiagnosisConfirmedIA is null &&
                               user.SelfReportedDiagnosis is
                                   SelfReportedDiagnosis.Yes or SelfReportedDiagnosis.NotSure);

        // Determine whether CAGE condition is met (Lifestyle answered + DrinkAlcohol = true)
        // At this stage we use the stored lifestyle submission if available
        // (simplified: if no visit, CAGE defaults to included for completeness)
        var cageConditionMet = latestVisit?.Lifestyle?.Drinkalcohol ?? true;

        var forms = new List<FormStatusItemDto>();

        if (followUpNumber == 0)
        {
            // Baseline sequence
            forms.Add(MakeForm(FormType.Lifestyle, "Lifestyle & Demographics", 1, latestVisit?.Lifestyle != null));
            forms.Add(MakeConditionalForm(FormType.Cage, "CAGE Alcohol Questionnaire", 2, cageConditionMet, latestVisit?.Cage != null));
            forms.Add(MakeForm(FormType.Dlqi, "DLQI", 3, latestVisit?.Dlqi != null));
            forms.Add(MakeForm(FormType.EuroQol, "EQ-5D (EuroQol)", 4, latestVisit?.Euroqol != null));
            forms.Add(MakeConditionalForm(FormType.Haq, "HAQ Disability Index", 5, haqConditionMet, latestVisit?.Haq != null));
            forms.Add(MakeForm(FormType.Hads, "HADS", 6, latestVisit?.Hads != null));
            forms.Add(MakeForm(FormType.Sapasi, "SAPASI", 7, latestVisit?.Sapasi != null));
        }
        else
        {
            // Follow-up sequence
            forms.Add(MakeForm(FormType.Pga, "Patient Global Assessment (PGA)", 1, latestVisit?.Pga != null));
            forms.Add(MakeForm(FormType.Lifestyle, "Medical Problems Update", 2, latestVisit?.Lifestyle != null));
            forms.Add(MakeForm(FormType.Dlqi, "DLQI", 3, latestVisit?.Dlqi != null));
            forms.Add(MakeForm(FormType.EuroQol, "EQ-5D (EuroQol)", 4, latestVisit?.Euroqol != null));
            forms.Add(MakeConditionalForm(FormType.Cage, "CAGE Alcohol Questionnaire", 5, cageConditionMet, latestVisit?.Cage != null));
            forms.Add(MakeForm(FormType.Hads, "HADS", 6, latestVisit?.Hads != null));
            forms.Add(MakeConditionalForm(FormType.Haq, "HAQ Disability Index", 7, haqConditionMet, latestVisit?.Haq != null));
            forms.Add(MakeForm(FormType.Sapasi, "SAPASI", 8, latestVisit?.Sapasi != null));
        }

        return forms;
    }

    private static FormStatusItemDto MakeForm(FormType type, string name, int order, bool completed) => new()
    {
        FormType      = type,
        FormName      = name,
        SequenceOrder = order,
        Status        = completed ? FormStatusEnum.Completed : FormStatusEnum.NotStarted,
        IsConditional = false,
        ConditionMet  = true
    };

    private static FormStatusItemDto MakeConditionalForm(
        FormType type, string name, int order, bool conditionMet, bool completed) => new()
    {
        FormType      = type,
        FormName      = name,
        SequenceOrder = order,
        Status        = completed ? FormStatusEnum.Completed : FormStatusEnum.NotStarted,
        IsConditional = true,
        ConditionMet  = conditionMet
    };
}
