using BADBIR.Shared.DTOs;
using BADBIR.UI.Components.Auth;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>HTTP implementation of IConsentApiService.</summary>
public class ConsentApiService : ApiServiceBase, IConsentApiService
{
    public ConsentApiService(HttpClient http, BabdirAuthStateProvider authProvider)
        : base(http, authProvider) { }

    public Task<ApiResult<ConsentResultDto>> SubmitConsentAsync(ConsentSubmitDto dto) =>
        PostAsync<ConsentSubmitDto, ConsentResultDto>("api/consent", dto);

    public async Task<ApiResult> GetConsentStatusAsync()
    {
        var result = await GetAsync<object>("api/consent/status");
        return result.IsSuccess ? ApiResult.Success() : ApiResult.Failure(result.ErrorMessage ?? "");
    }
}
