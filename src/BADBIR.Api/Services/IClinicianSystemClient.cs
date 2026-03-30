namespace BADBIR.Api.Services;

/// <summary>
/// Abstraction over the Clinician System's identity-verification endpoint.
/// In production this calls POST /api/internal/patients/verify-identity on
/// the Clinician System. During development a configurable stub is used.
/// </summary>
public interface IClinicianSystemClient
{
    /// <summary>
    /// Verifies that the supplied patient details (DOB, initials, and at least
    /// one identification number) match a record in the Clinician System.
    /// </summary>
    /// <param name="dateOfBirth">Patient date of birth.</param>
    /// <param name="initials">Patient initials, e.g. "JD" (case-insensitive).</param>
    /// <param name="nhsNumber">
    /// NHS number (England/Wales). Either this or <paramref name="chiNumber"/>
    /// must be supplied.
    /// </param>
    /// <param name="chiNumber">
    /// CHI / Health &amp; Care number (Scotland). Either this or
    /// <paramref name="nhsNumber"/> must be supplied.
    /// </param>
    /// <returns>
    /// <c>true</c> if a matching patient record is found; <c>false</c> otherwise.
    /// </returns>
    Task<bool> VerifyIdentityAsync(
        DateOnly dateOfBirth,
        string   initials,
        string?  nhsNumber,
        string?  chiNumber,
        CancellationToken cancellationToken = default);
}
