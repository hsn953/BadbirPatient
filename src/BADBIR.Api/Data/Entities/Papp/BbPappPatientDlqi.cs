namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for DLQI submissions.
/// Mirrors <see cref="BbPatientDlqi"/> column-for-column.
/// <see cref="IsCountable"/> must be set to true only when the form is fully
/// submitted — used for GL Assessments pay-per-form billing (see ADR-016).
/// </summary>
public class BbPappPatientDlqi
{
    public int PappDlqiId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

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

    /// <summary>1 if question 7 (work/study) is N/A.</summary>
    public int SkipBreakup { get; set; }

    /// <summary>
    /// Holding state:
    /// 0 = Holding, 1 = Approved (promoted), 2 = Rejected.
    /// </summary>
    public byte DataStatus { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPappPatientCohortTracking? CohortTracking { get; set; }
}
