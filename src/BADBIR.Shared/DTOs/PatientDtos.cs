namespace BADBIR.Shared.DTOs;

/// <summary>
/// Data Transfer Object for basic patient profile information.
/// </summary>
public class PatientDto
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string NhsNumber { get; set; } = string.Empty;
}

/// <summary>
/// DTO for submitting a patient-reported outcome form result.
/// </summary>
public class FormSubmissionDto
{
    public int PatientId { get; set; }
    public string FormType { get; set; } = string.Empty;  // e.g. "EuroQol", "HAQ", "HADS"
    public DateTime SubmittedAt { get; set; }
    public Dictionary<string, string> Answers { get; set; } = new();
}
