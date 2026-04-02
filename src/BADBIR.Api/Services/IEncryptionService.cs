namespace BADBIR.Api.Services;

/// <summary>
/// Encryption / decryption of PII fields stored in the BADBIR Patient database.
/// The algorithm must be byte-for-byte compatible with BadbirWebApp.Services.EncryptionService
/// in the Clinician System so that both applications can read each other's encrypted columns.
/// See ADR-015 in docs/progress/DECISION-LOG.md for the confirmed algorithm specification.
/// </summary>
public interface IEncryptionService
{
    /// <summary>Encrypts a plain-text string. Returns empty string if input is null or empty.</summary>
    string Encrypt(string plainText);

    /// <summary>Decrypts a Base64-encoded cipher text. Returns empty string if input is null or empty.</summary>
    string Decrypt(string cipherText);

    /// <summary>
    /// Encrypts the PII fields (Birthtown, Birthcountry) on a
    /// <see cref="Data.Entities.LifestyleSubmission"/> entity.
    /// </summary>
    Data.Entities.LifestyleSubmission EncryptLifestyle(Data.Entities.LifestyleSubmission lifestyle);

    /// <summary>Decrypts the PII fields on a <see cref="Data.Entities.LifestyleSubmission"/> entity.</summary>
    Data.Entities.LifestyleSubmission DecryptLifestyle(Data.Entities.LifestyleSubmission lifestyle);
}
