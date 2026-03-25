namespace BADBIR.Api.Data.Entities;

/// <summary>
/// EuroQol EQ-5D-5L patient-reported outcome submission.
/// Maps to [dbo].[EuroQolSubmissions].
/// Dimensions are scored 1 (no problems) to 5 (extreme problems).
/// VAS is 0–100.
/// </summary>
public class EuroQolSubmission
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }

    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte Mobility { get; set; }

    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte SelfCare { get; set; }

    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte UsualActivities { get; set; }

    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte PainDiscomfort { get; set; }

    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte AnxietyDepression { get; set; }

    /// <summary>0–100 visual analogue scale (self-rated health state)</summary>
    public byte VasScore { get; set; }

    /// <summary>Utility index value calculated from the 5-digit profile (e.g. −0.594 to 1.000).</summary>
    public decimal? IndexValue { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Patient Patient { get; set; } = null!;
}
