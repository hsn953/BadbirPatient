using BADBIR.Shared.DTOs;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Wraps patient profile API calls.
/// </summary>
public interface IPatientApiService
{
    Task<ApiResult<PatientProfileDto>> GetProfileAsync();
    Task<ApiResult<DashboardDto>> GetDashboardAsync();
    Task<ApiResult> UpdateDiagnosisAsync(DiagnosisUpdateDto dto);
    Task<ApiResult> UpdatePreferencesAsync(PreferencesUpdateDto dto);
}
