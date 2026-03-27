namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientCohortTracking].
/// One row per follow-up visit. All clinical form submissions reference
/// this table via <see cref="FupId"/>.
/// This table is owned by the Clinician System.
/// </summary>
public class BbPatientCohortTracking
{
    public int FupId { get; set; }

    /// <summary>FK to bbPatientCohortHistory.chid.</summary>
    public int Chid { get; set; }

    /// <summary>0 = Baseline, 1..N = Follow-up number.</summary>
    public int Fupcode { get; set; }

    public int? Studynocurrent { get; set; }
    public int? Centreidcurrent { get; set; }
    public int? Consultantidcurrent { get; set; }

    public DateTime? ClinicVisitdate { get; set; }
    public DateTime? Duedate { get; set; }
    public DateTime? EditWindowFrom { get; set; }
    public DateTime? EditWindowTo { get; set; }

    /// <summary>0=Open, 1=Complete.</summary>
    public int? Fupstatus { get; set; }

    public int? Datavalid { get; set; }
    public int? Feedbackstatus { get; set; }
    public string? Comments { get; set; }
    public DateTime? Dateentered { get; set; }

    // ── Section completion flags ─────────────────────────────────────────────
    public bool? Hasnocurrenttherapy { get; set; }
    public bool Hasnobiologictherapy { get; set; }
    public bool Hasnoconventionaltherapy { get; set; }
    public bool Hasnoprevioustherapy { get; set; }
    public bool? Hasnocomorbidities { get; set; }
    public bool? Hasnolesions { get; set; }
    public bool? Hasnouvtherapy { get; set; }
    public bool Hasnolabvalues { get; set; }
    public bool Hasnoadverseevents { get; set; }
    public bool Hasnodiseaseseverity { get; set; }
    public bool? Hasnopasi { get; set; }
    public bool? Hasnoadditionalinfo { get; set; }
    public bool Hasnomedicalproblems { get; set; }
    public bool Hasnodlqi { get; set; }
    public bool Hasnolifestylefactors { get; set; }
    public bool? Cageinapplicable { get; set; }
    public bool? Haqinapplicable { get; set; }
    public bool? Discontinuedbiotherapy { get; set; }
    public bool? Haspreviousantipsoriaticdrugs { get; set; }
    public bool? Haschangedbiologictherapy { get; set; }
    public bool? Hasnewadverseevents { get; set; }
    public bool? HaspsoriaticArthiritis { get; set; }
    public bool? HasinflamatoryArthiritis { get; set; }
    public int? PsoriaticarthiritisonSet { get; set; }
    public int? TruncatedFupApplicable { get; set; }
    public DateTime? PsoriaticarthiritisonSetdate { get; set; }
    public bool? HasnoSMITherapy { get; set; }

    /// <summary>FK to bbPatientCohortTrackingClinicAttendanceLkp.</summary>
    public int? ClinicAttendance { get; set; }

    public int? HasPgaScore { get; set; }
    public int? DlqiCompleted { get; set; }
    public int? CdlqiCompleted { get; set; }
    public int? EuroqolCompleted { get; set; }
    public int? CageApplicable { get; set; }
    public int? CageCompleted { get; set; }
    public int? HadsCompleted { get; set; }
    public int? HaqApplicable { get; set; }
    public int? HaqCompleted { get; set; }
    public int? TruncatedFupQvis { get; set; }

    public int? Checkedbyid { get; set; }
    public DateTime? Checkedbydate { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPatientCohortHistory? CohortHistory { get; set; }
    public BbPatientDlqi? Dlqi { get; set; }
    public BbPatientLifestyle? Lifestyle { get; set; }
    public ICollection<BbPatientCage> CageSubmissions { get; set; } = [];
    public ICollection<BbPatientPasiScores> PasiScores { get; set; } = [];
}
