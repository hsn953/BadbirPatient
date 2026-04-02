using System.ComponentModel.DataAnnotations;
using BADBIR.Shared.Enums;

namespace BADBIR.Shared.DTOs;

/// <summary>
/// Patient self-registration request.
/// The patient's identity is first verified against the Clinician System using
/// <see cref="DateOfBirth"/>, <see cref="Initials"/>, and at least one of
/// <see cref="NhsNumber"/> / <see cref="ChiNumber"/> before the account is
/// created.
/// </summary>
public class PatientRegisterRequestDto
{
    // ── Account credentials ──────────────────────────────────────────────────

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    // ── Identity verification fields ─────────────────────────────────────────

    /// <summary>Patient date of birth — must match the Clinician System record.</summary>
    [Required]
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Patient initials as registered (e.g. "JD"). Case-insensitive.
    /// At least one character required; trimmed before comparison.
    /// </summary>
    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public string Initials { get; set; } = string.Empty;

    /// <summary>NHS number (England/Wales). At least one of this or <see cref="ChiNumber"/> is required.</summary>
    public string? NhsNumber { get; set; }

    /// <summary>CHI / Health &amp; Care number (Scotland). At least one of this or <see cref="NhsNumber"/> is required.</summary>
    public string? ChiNumber { get; set; }

    /// <summary>
    /// BADBIR study number (if the patient was assigned one). Optional.
    /// At least one of NhsNumber, ChiNumber, or BadbirStudyNumber must be provided;
    /// this requirement is enforced at the API level in AuthController.
    /// </summary>
    public string? BadbirStudyNumber { get; set; }
}

/// <summary>Login request payload.</summary>
public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

/// <summary>Successful authentication response carrying a JWT.</summary>
public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = [];
    public RegistrationStatus RegistrationStatus { get; set; }
    public bool ConsentGiven { get; set; }
    public SelfReportedDiagnosis? SelfReportedDiagnosis { get; set; }
    public DateTime? HoldingExpiry { get; set; }
}

/// <summary>
/// Legacy registration DTO — kept for backwards compatibility with any
/// existing code that references it.
/// </summary>
public class RegisterRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NhsNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}

// ── New first-time / unverified registration ──────────────────────────────────

/// <summary>
/// Request body for first-time patient self-registration (Pathway A).
/// The patient's identity could NOT be verified against the Clinician System,
/// but they choose to create a holding account (14-day window for clinician confirmation).
/// </summary>
public class NewPatientRegistrationDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public string Initials { get; set; } = string.Empty;

    public string? NhsNumber { get; set; }
    public string? ChiNumber { get; set; }
    public string? BadbirStudyNumber { get; set; }

    /// <summary>The clinical centre the patient selected during registration.</summary>
    public string? ClinicalCentre { get; set; }
}

/// <summary>Request to resend the email verification link.</summary>
public class ResendVerificationEmailDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

// ── Account Recovery ─────────────────────────────────────────────────────────

/// <summary>Step 1 of account recovery — verify identity.</summary>
public class AccountRecoveryRequestDto
{
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(10)]
    public string Initials { get; set; } = string.Empty;

    public string? NhsNumber { get; set; }
    public string? ChiNumber { get; set; }
    public string? BadbirStudyNumber { get; set; }
}

/// <summary>
/// Step 2 of account recovery — set new email + password.
/// Requires the recovery token from step 1.
/// </summary>
public class AccountRecoveryResetDto
{
    [Required]
    public string RecoveryToken { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string NewEmail { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; } = string.Empty;
}

/// <summary>Response from account recovery step 1 — contains a short-lived token.</summary>
public class AccountRecoveryTokenDto
{
    public string RecoveryToken { get; set; } = string.Empty;
    public string MaskedEmail { get; set; } = string.Empty;
}
