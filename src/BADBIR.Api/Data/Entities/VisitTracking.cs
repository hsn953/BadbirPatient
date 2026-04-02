namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Tracks a patient's portal visit / form-fill session.
/// One row per visit (baseline or follow-up).
/// All form submissions reference this via <see cref="VisitId"/>.
/// Replaces the legacy bbPappPatientCohortTracking holding table.
/// </summary>
public class VisitTracking
{
    public int VisitId { get; set; }

    /// <summary>FK to AspNetUsers.Id — the portal user who owns this visit.</summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Optional reference to the Clinician System's patient ID (patientid).
    /// Set after identity verification succeeds at registration.
    /// </summary>
    public int? ClinicianPatientId { get; set; }

    /// <summary>0 = Baseline, 1..N = Follow-up number.</summary>
    public int PotentialFupCode { get; set; }

    /// <summary>Set to bbPatientCohortTracking.FupId after the Clinician System promotes this visit.</summary>
    public int? ImportedFupId { get; set; }

    public DateTime? VisitDate { get; set; }
    public DateTime DateEntered { get; set; }
    public string? Comments { get; set; }

    /// <summary>0 = Forms in progress, 1 = All mandatory forms complete, 2 = Cancelled.</summary>
    public int VisitStatus { get; set; }

    /// <summary>
    /// 0 = Holding (awaiting clinician review),
    /// 1 = Approved (promoted by Clinician System),
    /// 2 = Rejected.
    /// </summary>
    public byte DataStatus { get; set; }

    // ── Audit ────────────────────────────────────────────────────────────────
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public ApplicationUser? User { get; set; }
    public DlqiSubmission? Dlqi { get; set; }
    public LifestyleSubmission? Lifestyle { get; set; }
    public CageSubmission? Cage { get; set; }
    public EuroqolSubmission? Euroqol { get; set; }
    public HadsSubmission? Hads { get; set; }
    public HaqSubmission? Haq { get; set; }
    public PgaSubmission? Pga { get; set; }
    public SapasiSubmission? Sapasi { get; set; }
}
