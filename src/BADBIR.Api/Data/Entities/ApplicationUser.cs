using BADBIR.Shared.Enums;
using Microsoft.AspNetCore.Identity;

namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Portal user account. Extends IdentityUser with BADBIR-specific fields.
/// One user account corresponds to one patient accessing the portal.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Optional reference to the Clinician System's patient record (bbPatient.patientid).
    /// Set once identity is verified against the Clinician System at registration.
    /// Null for admin/clinician accounts that are not linked to a patient record.
    /// </summary>
    public int? ClinicianPatientId { get; set; }

    // ── Registration status ────────────────────────────────────────────────────

    /// <summary>
    /// Current registration status.
    /// Active = verified (identity matched or clinician-confirmed).
    /// Holding = first-time unverified account; expires at HoldingExpiry.
    /// InviteActive = clinician-initiated invite; active immediately.
    /// </summary>
    public RegistrationStatus RegistrationStatus { get; set; } = RegistrationStatus.Holding;

    /// <summary>
    /// UTC timestamp after which this holding account is permanently deleted if still unconfirmed.
    /// Null for Active / InviteActive accounts.
    /// </summary>
    public DateTime? HoldingExpiry { get; set; }

    // ── Patient profile (from registration) ───────────────────────────────────

    public DateOnly? DateOfBirth { get; set; }

    /// <summary>Patient initials as provided at registration (e.g. "JD").</summary>
    public string? Initials { get; set; }

    /// <summary>Clinical centre selected during registration.</summary>
    public string? ClinicalCentre { get; set; }

    // ── Diagnosis ─────────────────────────────────────────────────────────────

    /// <summary>Patient's self-reported inflammatory arthritis diagnosis answer.</summary>
    public SelfReportedDiagnosis? SelfReportedDiagnosis { get; set; }

    /// <summary>
    /// Authoritative IA diagnosis flag set by the clinician when promoting the patient.
    /// Null = not yet confirmed.
    /// </summary>
    public bool? DiagnosisConfirmedIA { get; set; }

    // ── Consent ───────────────────────────────────────────────────────────────

    /// <summary>True once the patient has submitted an informed consent record.</summary>
    public bool ConsentGiven { get; set; }

    /// <summary>Version of the consent form accepted. Used for re-consent detection.</summary>
    public string? ConsentVersion { get; set; }

    // ── Notification preferences ──────────────────────────────────────────────

    /// <summary>
    /// Whether the patient has opted in to clinical form reminders.
    /// Default: true (ON).
    /// </summary>
    public bool NotifyReminders { get; set; } = true;

    /// <summary>
    /// Whether the patient has opted in to informational / newsletter communications.
    /// Default: false (OFF).
    /// </summary>
    public bool NotifyInfoComms { get; set; } = false;

    // ── Navigation ────────────────────────────────────────────────────────────

    /// <summary>Navigation to this user's portal visits.</summary>
    public ICollection<VisitTracking> Visits { get; set; } = [];

    /// <summary>Navigation to this user's consent records.</summary>
    public ICollection<ConsentRecord> ConsentRecords { get; set; } = [];
}
