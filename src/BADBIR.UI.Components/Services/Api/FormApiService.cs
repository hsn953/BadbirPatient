using BADBIR.Shared.DTOs;
using BADBIR.UI.Components.Auth;

namespace BADBIR.UI.Components.Services.Api;

/// <summary>HTTP implementation of IFormApiService.</summary>
public class FormApiService : ApiServiceBase, IFormApiService
{
    public FormApiService(HttpClient http, BabdirAuthStateProvider authProvider)
        : base(http, authProvider) { }

    public Task<ApiResult<PgaDto>>       SubmitPgaAsync(PgaSubmitDto dto)           => PostAsync<PgaSubmitDto, PgaDto>("api/forms/pga", dto);
    public Task<ApiResult<DlqiDto>>      SubmitDlqiAsync(DlqiSubmitDto dto)         => PostAsync<DlqiSubmitDto, DlqiDto>("api/forms/dlqi", dto);
    public Task<ApiResult<LifestyleDto>> SubmitLifestyleAsync(LifestyleSubmitDto dto) => PostAsync<LifestyleSubmitDto, LifestyleDto>("api/forms/lifestyle", dto);
    public Task<ApiResult<CageDto>>      SubmitCageAsync(CageSubmitDto dto)          => PostAsync<CageSubmitDto, CageDto>("api/forms/cage", dto);
    public Task<ApiResult<EuroqolDto>>   SubmitEuroqolAsync(EuroqolSubmitDto dto)    => PostAsync<EuroqolSubmitDto, EuroqolDto>("api/forms/euroqol", dto);
    public Task<ApiResult<HadsDto>>      SubmitHadsAsync(HadsSubmitRequest dto)      => PostAsync<HadsSubmitRequest, HadsDto>("api/forms/hads", dto);
    public Task<ApiResult<HaqDto>>       SubmitHaqAsync(HaqSubmitRequest dto)        => PostAsync<HaqSubmitRequest, HaqDto>("api/forms/haq", dto);
    public Task<ApiResult<SapasiDto>>    SubmitSapasiAsync(SapasiSubmitDto dto)      => PostAsync<SapasiSubmitDto, SapasiDto>("api/forms/sapasi", dto);
}
