using BADBIR.Shared.DTOs;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Wraps informed consent API calls.
/// </summary>
public interface IConsentApiService
{
    Task<ApiResult<ConsentResultDto>> SubmitConsentAsync(ConsentSubmitDto dto);
    Task<ApiResult> GetConsentStatusAsync();
}
