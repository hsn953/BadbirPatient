using BADBIR.Shared.DTOs;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Wraps all authentication API calls for the BADBIR Patient application.
/// </summary>
public interface IAuthApiService
{
    /// <summary>Pathway A matched — identity verified, create Active account.</summary>
    Task<ApiResult> RegisterAsync(PatientRegisterRequestDto dto);

    /// <summary>Pathway A unmatched — first-time patient, create Holding account.</summary>
    Task<ApiResult> RegisterNewAsync(NewPatientRegistrationDto dto);

    /// <summary>Authenticate and receive a JWT.</summary>
    Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto dto);

    /// <summary>Resend email verification link.</summary>
    Task<ApiResult> SendVerificationEmailAsync(string email);

    /// <summary>Confirm email address using userId + token from the verification link.</summary>
    Task<ApiResult> VerifyEmailAsync(string userId, string token);

    /// <summary>Step 1 of account recovery — verify identity.</summary>
    Task<ApiResult<AccountRecoveryTokenDto>> RequestRecoveryAsync(AccountRecoveryRequestDto dto);

    /// <summary>Step 2 of account recovery — set new credentials.</summary>
    Task<ApiResult> ResetAccountAsync(AccountRecoveryResetDto dto);
}
