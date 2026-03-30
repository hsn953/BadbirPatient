namespace BADBIR.Api.Services;

/// <summary>
/// Encryption / decryption of PII fields stored in the shared BADBIR database.
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
    /// Encrypts all PII fields on a <see cref="Data.Entities.BbPatient"/> entity.
    /// Fields encrypted: Title, Forenames, Surname, Countryresidence, Phrn, Pnhs.
    /// </summary>
    Data.Entities.BbPatient EncryptPatient(Data.Entities.BbPatient patient);

    /// <summary>
    /// Decrypts all PII fields on a <see cref="Data.Entities.BbPatient"/> entity.
    /// </summary>
    Data.Entities.BbPatient DecryptPatient(Data.Entities.BbPatient patient);

    /// <summary>
    /// Encrypts the PII fields on a <see cref="Data.Entities.BbPatientLifestyle"/> entity.
    /// Fields encrypted: Birthtown, Birthcountry.
    /// </summary>
    Data.Entities.BbPatientLifestyle EncryptLifestyle(Data.Entities.BbPatientLifestyle lifestyle);

    /// <summary>Decrypts the PII fields on a <see cref="Data.Entities.BbPatientLifestyle"/> entity.</summary>
    Data.Entities.BbPatientLifestyle DecryptLifestyle(Data.Entities.BbPatientLifestyle lifestyle);

    /// <summary>Encrypts the PII fields on a papp lifestyle entity.</summary>
    Data.Entities.Papp.BbPappPatientLifestyle EncryptPappLifestyle(Data.Entities.Papp.BbPappPatientLifestyle lifestyle);

    /// <summary>Decrypts the PII fields on a papp lifestyle entity.</summary>
    Data.Entities.Papp.BbPappPatientLifestyle DecryptPappLifestyle(Data.Entities.Papp.BbPappPatientLifestyle lifestyle);
}
