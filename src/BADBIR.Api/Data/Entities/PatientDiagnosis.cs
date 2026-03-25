namespace BADBIR.Api.Data.Entities;

/// <summary>
/// A patient's diagnosis entry.
/// Maps to [dbo].[PatientDiagnoses].
/// </summary>
public class PatientDiagnosis
{
    public int DiagnosisId { get; set; }
    public int PatientId { get; set; }

    public string DiagnosisCode { get; set; } = string.Empty;
    public string DiagnosisName { get; set; } = string.Empty;
    public DateOnly? DiagnosedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    // Navigation
    public Patient Patient { get; set; } = null!;
}
