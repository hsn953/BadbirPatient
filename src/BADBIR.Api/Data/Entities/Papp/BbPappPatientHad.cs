namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for HADS (Hospital Anxiety and Depression Scale) submissions.
/// 14 items: Q01–Q07 Anxiety, Q08–Q14 Depression (each 0–3).
/// Column names use the legacy papp naming convention (q01tense … q14goodbook).
/// <see cref="IsCountable"/> must be set to true only on full submission
/// (all 14 items answered) — used for GL Assessments pay-per-form billing (ADR-016).
/// </summary>
public class BbPappPatientHad
{
    public int PappHadId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    // ── Anxiety items (odd questions) ────────────────────────────────────────
    public int? Q01tense { get; set; }
    public int? Q03frightened { get; set; }
    public int? Q05worry { get; set; }
    public int? Q07relaxed { get; set; }
    public int? Q09butterflies { get; set; }
    public int? Q11restless { get; set; }
    public int? Q13panic { get; set; }

    // ── Depression items (even questions) ────────────────────────────────────
    public int? Q02enjoy { get; set; }
    public int? Q04laugh { get; set; }
    public int? Q06cheerful { get; set; }
    public int? Q08slowed { get; set; }
    public int? Q10appearence { get; set; }
    public int? Q12enjoyment { get; set; }
    public int? Q14goodbook { get; set; }

    /// <summary>Sum of odd items (0–21).</summary>
    public int? ScoreAnxiety { get; set; }

    /// <summary>0=Normal(0–7), 1=Borderline(8–10), 2=Abnormal(11–21).</summary>
    public int? ResultAnxiety { get; set; }

    /// <summary>Sum of even items (0–21).</summary>
    public int? ScoreDepression { get; set; }

    /// <summary>0=Normal(0–7), 1=Borderline(8–10), 2=Abnormal(11–21).</summary>
    public int? ResultDepression { get; set; }

    public DateTime? DateScored { get; set; }

    /// <summary>
    /// True once all 14 items have been answered and the form submitted.
    /// This flag drives the GL Assessments pay-per-form invoice count.
    /// </summary>
    public bool IsCountable { get; set; }

    /// <summary>0 = Holding, 1 = Approved, 2 = Rejected.</summary>
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
