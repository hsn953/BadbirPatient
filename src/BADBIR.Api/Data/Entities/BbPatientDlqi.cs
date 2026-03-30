namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientDLQI].
/// Dermatology Life Quality Index submission.
/// Each row corresponds to one follow-up visit via <see cref="FupId"/>.
/// <see cref="QSourceId"/> = 2 when submitted via the Patient Portal.
/// </summary>
public class BbPatientDlqi
{
    public int DlqiID { get; set; }

    /// <summary>FK to bbPatientCohortTracking.FupId.</summary>
    public int FupId { get; set; }

    public string? Diagnosis { get; set; }

    // ── DLQI items (0–3 each) ─────────────────────────────────────────────────
    public int? ItchsoreScore { get; set; }
    public int? EmbscScore { get; set; }
    public int? ShophgScore { get; set; }
    public int? ClothesScore { get; set; }
    public int? SocleisScore { get; set; }
    public int? SportScore { get; set; }
    public int? WorkstudScore { get; set; }
    public int? WorkstudnoScore { get; set; }
    public int? PartcrfScore { get; set; }
    public int? SexdifScore { get; set; }
    public int? TreatmentScore { get; set; }

    /// <summary>Sum of all 10 item scores (0–30).</summary>
    public int? TotalScore { get; set; }

    public DateTime? Datecomp { get; set; }

    /// <summary>1 if question 7 (work/study) is answered as N/A.</summary>
    public int SkipBreakup { get; set; }

    /// <summary>FK to bbPatientQuestionnaireSourcelkp. 1=Clinician, 2=Patient (via Portal), 3=Patient (via Clinician).</summary>
    public int? QSourceId { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPatientCohortTracking? CohortTracking { get; set; }
}
