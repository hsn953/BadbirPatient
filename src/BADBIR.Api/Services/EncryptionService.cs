using System.Security.Cryptography;
using System.Text;
using BADBIR.Api.Data.Entities;

namespace BADBIR.Api.Services;

/// <summary>
/// AES encryption service that is byte-for-byte compatible with the legacy
/// BadbirWebApp Clinician System EncryptionService.
///
/// Algorithm summary (confirmed in ADR-015):
///   - Cipher:         AES-CBC, 128-bit block
///   - Key derivation: PasswordDeriveBytes(password, salt) — PBKDF1 legacy derivation
///   - Salt:           ASCII bytes of password.Length.ToString()
///   - Key bytes:      GetBytes(32) → 256-bit key
///   - IV bytes:       GetBytes(16) → 128-bit IV (deterministic — same for every call)
///   - Plaintext:      Encoding.Unicode (UTF-16 LE)
///   - Ciphertext:     Convert.ToBase64String(encryptedBytes)
///
/// The password is read from IConfiguration["EncryptionServiceConfig:Password"].
/// It must be identical in both the Patient API and the Clinician System configuration.
/// </summary>
public class EncryptionService : IEncryptionService
{
    private readonly IConfiguration _config;
    private readonly ILogger<EncryptionService> _logger;

    public EncryptionService(IConfiguration config, ILogger<EncryptionService> logger)
    {
        _config = config;
        _logger = logger;
    }

    // ── Core encrypt / decrypt ────────────────────────────────────────────────

    /// <inheritdoc/>
    public string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        var password = GetPassword();
        var salt     = Encoding.ASCII.GetBytes(password.Length.ToString());

        using var aes = Aes.Create();
        aes.BlockSize = 128;
        aes.GenerateIV(); // not used — IV is overridden by PasswordDeriveBytes below

#pragma warning disable SYSLIB0041 // PasswordDeriveBytes is legacy but required for backward compat (ADR-015)
        var secretKey  = new PasswordDeriveBytes(password, salt);
#pragma warning restore SYSLIB0041

        using var encryptor = aes.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));

        var plainBytes = Encoding.Unicode.GetBytes(plainText);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    /// <inheritdoc/>
    public string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            return string.Empty;

        try
        {
            var encryptedBytes = Convert.FromBase64String(cipherText);
            var password       = GetPassword();
            var salt           = Encoding.ASCII.GetBytes(password.Length.ToString());

            using var aes = Aes.Create();
            aes.GenerateIV();

#pragma warning disable SYSLIB0041
            var secretKey = new PasswordDeriveBytes(password, salt);
#pragma warning restore SYSLIB0041

            using var decryptor = aes.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));

            using var ms       = new MemoryStream(encryptedBytes);
            using var cs       = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader   = new StreamReader(cs, Encoding.Unicode);

            return reader.ReadToEnd() ?? string.Empty;
        }
        catch (FormatException ex)
        {
            LogDecryptError(cipherText, ex);
            return "[Format decryption error]";
        }
        catch (Exception ex)
        {
            LogDecryptError(cipherText, ex);
            return "[Unknown decryption error]";
        }
    }

    // ── Entity convenience methods ────────────────────────────────────────────

    /// <inheritdoc/>
    public LifestyleSubmission EncryptLifestyle(LifestyleSubmission lifestyle)
    {
        lifestyle.Birthtown    = Encrypt(lifestyle.Birthtown ?? string.Empty);
        lifestyle.Birthcountry = Encrypt(lifestyle.Birthcountry ?? string.Empty);
        return lifestyle;
    }

    /// <inheritdoc/>
    public LifestyleSubmission DecryptLifestyle(LifestyleSubmission lifestyle)
    {
        lifestyle.Birthtown    = Decrypt(lifestyle.Birthtown ?? string.Empty);
        lifestyle.Birthcountry = Decrypt(lifestyle.Birthcountry ?? string.Empty);
        return lifestyle;
    }

    // ── Private helpers ───────────────────────────────────────────────────────

    private string GetPassword() =>
        _config["EncryptionServiceConfig:Password"]
        ?? throw new InvalidOperationException(
            "EncryptionServiceConfig:Password is not configured. " +
            "Set it via dotnet user-secrets or an environment variable.");

    private void LogDecryptError(string cipherText, Exception ex)
    {
        var preview = cipherText.Trim();
        var snippet = preview[..Math.Min(10, preview.Length)];
        _logger.LogError(ex, "Decryption failed for value beginning '{Snippet}'", snippet);
    }
}
