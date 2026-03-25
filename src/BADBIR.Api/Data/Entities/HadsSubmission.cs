namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Hospital Anxiety and Depression Scale (HADS) submission.
/// Maps to [dbo].[HadsSubmissions].
/// 14 items total: A1–A7 (Anxiety) and D1–D7 (Depression), each 0–3.
/// Subscale totals: 0–7 normal, 8–10 borderline, 11–21 abnormal.
/// </summary>
public class HadsSubmission
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }

    // ── Anxiety items (0–3 each) ──────────────────────────────────────────
    public byte A1 { get; set; }
    public byte A2 { get; set; }
    public byte A3 { get; set; }
    public byte A4 { get; set; }
    public byte A5 { get; set; }
    public byte A6 { get; set; }
    public byte A7 { get; set; }

    // ── Depression items (0–3 each) ───────────────────────────────────────
    public byte D1 { get; set; }
    public byte D2 { get; set; }
    public byte D3 { get; set; }
    public byte D4 { get; set; }
    public byte D5 { get; set; }
    public byte D6 { get; set; }
    public byte D7 { get; set; }

    /// <summary>Sum of A1–A7 (0–21).</summary>
    public byte AnxietyScore { get; set; }

    /// <summary>Sum of D1–D7 (0–21).</summary>
    public byte DepressionScore { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Patient Patient { get; set; } = null!;
}
