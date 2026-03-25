using Microsoft.AspNetCore.Identity;

namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Custom Identity user – extends IdentityUser with BADBIR-specific profile data.
/// One-to-one with <see cref="Patient"/>.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>Navigation to the linked patient record (may be null for admin users).</summary>
    public Patient? Patient { get; set; }
}
