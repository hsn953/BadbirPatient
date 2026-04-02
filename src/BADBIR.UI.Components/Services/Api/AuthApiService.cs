using BADBIR.Shared.DTOs;
using BADBIR.UI.Components.Auth;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>HTTP implementation of IAuthApiService.</summary>
public class AuthApiService : ApiServiceBase, IAuthApiService
{
    public AuthApiService(HttpClient http, BabdirAuthStateProvider authProvider)
        : base(http, authProvider) { }

    public Task<ApiResult> RegisterAsync(PatientRegisterRequestDto dto) =>
        PostAsync("api/auth/register", dto);

    public Task<ApiResult> RegisterNewAsync(NewPatientRegistrationDto dto) =>
        PostAsync("api/auth/register/new", dto);

    public Task<ApiResult<LoginResponseDto>> LoginAsync(LoginRequestDto dto) =>
        PostAsync<LoginRequestDto, LoginResponseDto>("api/auth/login", dto);

    public Task<ApiResult> SendVerificationEmailAsync(string email) =>
        PostAsync("api/auth/send-verification-email", new ResendVerificationEmailDto { Email = email });

    public async Task<ApiResult> VerifyEmailAsync(string userId, string token)
    {
        try
        {
            var url      = $"api/auth/verify-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";
            var response = await Http.GetAsync(url);
            return response.IsSuccessStatusCode
                ? ApiResult.Success()
                : ApiResult.Failure("Verification failed. The link may have expired.");
        }
        catch
        {
            return ApiResult.Failure("Unable to connect to the verification server.");
        }
    }

    public Task<ApiResult<AccountRecoveryTokenDto>> RequestRecoveryAsync(AccountRecoveryRequestDto dto) =>
        PostAsync<AccountRecoveryRequestDto, AccountRecoveryTokenDto>("api/auth/recovery/request", dto);

    public Task<ApiResult> ResetAccountAsync(AccountRecoveryResetDto dto) =>
        PostAsync("api/auth/recovery/reset", dto);
}
