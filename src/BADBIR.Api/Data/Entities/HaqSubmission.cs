namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Health Assessment Questionnaire (HAQ-DI) submission.
/// Maps to [dbo].[HaqSubmissions].
/// Category scores: 0=Without any difficulty … 3=Unable to do.
/// HAQ-DI = mean of 8 category scores (0–3).
/// </summary>
public class HaqSubmission
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }

    // ── Category difficulty scores (0–3) ──────────────────────────────────
    public byte Dressing { get; set; }
    public byte Arising { get; set; }
    public byte Eating { get; set; }
    public byte Walking { get; set; }
    public byte Hygiene { get; set; }
    public byte Reach { get; set; }
    public byte Grip { get; set; }
    public byte Activities { get; set; }

    /// <summary>Calculated HAQ-DI score (mean of all 8 categories, 0–3).</summary>
    public decimal HaqDiScore { get; set; }

    // ── Aids/devices flags ────────────────────────────────────────────────
    public bool UsesDressingAids { get; set; }
    public bool UsesArisingAids { get; set; }
    public bool UsesEatingAids { get; set; }
    public bool UsesWalkingAids { get; set; }
    public bool UsesHygieneAids { get; set; }
    public bool UsesReachAids { get; set; }
    public bool UsesGripAids { get; set; }
    public bool UsesActivitiesAids { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Patient Patient { get; set; } = null!;
}
