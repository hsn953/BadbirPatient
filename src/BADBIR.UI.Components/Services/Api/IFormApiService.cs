using BADBIR.Shared.DTOs;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>
/// Wraps patient-reported outcome form submission API calls.
/// </summary>
public interface IFormApiService
{
    Task<ApiResult<PgaDto>>      SubmitPgaAsync(PgaSubmitDto dto);
    Task<ApiResult<DlqiDto>>     SubmitDlqiAsync(DlqiSubmitDto dto);
    Task<ApiResult<LifestyleDto>> SubmitLifestyleAsync(LifestyleSubmitDto dto);
    Task<ApiResult<CageDto>>     SubmitCageAsync(CageSubmitDto dto);
    Task<ApiResult<EuroqolDto>>  SubmitEuroqolAsync(EuroqolSubmitDto dto);
    Task<ApiResult<HadsDto>>     SubmitHadsAsync(HadsSubmitRequest dto);
    Task<ApiResult<HaqDto>>      SubmitHaqAsync(HaqSubmitRequest dto);
    Task<ApiResult<SapasiDto>>   SubmitSapasiAsync(SapasiSubmitDto dto);
}
