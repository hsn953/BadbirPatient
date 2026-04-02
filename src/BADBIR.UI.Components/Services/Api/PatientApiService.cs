using BADBIR.Shared.DTOs;
using BADBIR.UI.Components.Auth;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>HTTP implementation of IPatientApiService.</summary>
public class PatientApiService : ApiServiceBase, IPatientApiService
{
    public PatientApiService(HttpClient http, BabdirAuthStateProvider authProvider)
        : base(http, authProvider) { }

    public Task<ApiResult<PatientProfileDto>> GetProfileAsync() =>
        GetAsync<PatientProfileDto>("api/patients/me");

    public Task<ApiResult<DashboardDto>> GetDashboardAsync() =>
        GetAsync<DashboardDto>("api/patients/me/dashboard");

    public Task<ApiResult> UpdateDiagnosisAsync(DiagnosisUpdateDto dto) =>
        PatchAsync("api/patients/me/diagnosis", dto);

    public Task<ApiResult> UpdatePreferencesAsync(PreferencesUpdateDto dto) =>
        PatchAsync("api/patients/me/preferences", dto);
}
