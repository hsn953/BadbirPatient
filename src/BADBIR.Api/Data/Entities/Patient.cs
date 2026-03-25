namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Core patient demographic record.
/// Maps to [dbo].[Patients].
/// </summary>
public class Patient
{
    public int PatientId { get; set; }

    /// <summary>FK to AspNetUsers – one-to-one with ApplicationUser.</summary>
    public string UserId { get; set; } = string.Empty;

    public string NhsNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Ethnicity { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    // Navigation
    public ApplicationUser User { get; set; } = null!;
    public ICollection<PatientDiagnosis> Diagnoses { get; set; } = [];
    public ICollection<EuroQolSubmission> EuroQolSubmissions { get; set; } = [];
    public ICollection<HaqSubmission> HaqSubmissions { get; set; } = [];
    public ICollection<HadsSubmission> HadsSubmissions { get; set; } = [];
}
