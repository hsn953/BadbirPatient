using Microsoft.AspNetCore.Identity;

namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Custom Identity user – extends IdentityUser with BADBIR-specific profile data.
/// One-to-one with <see cref="BbPatient"/> via <see cref="PatientId"/>.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// FK to bbPatient.patientid — set when a patient registers via the portal.
    /// Null for clinician / admin accounts that don't have a patient record.
    /// </summary>
    public int? PatientId { get; set; }

    /// <summary>Navigation to the linked patient record (may be null for admin/clinician users).</summary>
    public BbPatient? Patient { get; set; }
}
