using Microsoft.AspNetCore.Identity;

namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Portal user account. Extends IdentityUser with BADBIR-specific fields.
/// One user account corresponds to one patient accessing the portal.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Optional reference to the Clinician System's patient record (bbPatient.patientid).
    /// Set once identity is verified against the Clinician System at registration.
    /// Null for admin/clinician accounts that are not linked to a patient record.
    /// </summary>
    public int? ClinicianPatientId { get; set; }

    /// <summary>Navigation to this user's portal visits.</summary>
    public ICollection<VisitTracking> Visits { get; set; } = [];
}
