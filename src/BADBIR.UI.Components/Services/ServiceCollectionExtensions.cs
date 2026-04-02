using BADBIR.UI.Components.Auth;
using BADBIR.UI.Components.Services.Api;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BADBIR.UI.Components;

/// <summary>
/// Extension methods to register BADBIR.UI.Components services with the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all services required by the BADBIR.UI.Components Razor Class Library.
    /// Call this from both BADBIR.Web (Program.cs) and BADBIR.Mobile (MauiProgram.cs).
    /// </summary>
    /// <param name="apiBaseUrl">Base URL of the BADBIR.Api (e.g. "https://localhost:7100").</param>
    public static IServiceCollection AddBADBIRUIComponents(
        this IServiceCollection services,
        string apiBaseUrl)
    {
        // Auth infrastructure
        services.AddScoped<SessionStorageService>();
        services.AddScoped<BabdirAuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(sp =>
            sp.GetRequiredService<BabdirAuthStateProvider>());

        // Named HttpClient pointing at the API
        services.AddHttpClient<IAuthApiService, AuthApiService>(c =>
            c.BaseAddress = new Uri(apiBaseUrl));

        services.AddHttpClient<IPatientApiService, PatientApiService>(c =>
            c.BaseAddress = new Uri(apiBaseUrl));

        services.AddHttpClient<IConsentApiService, ConsentApiService>(c =>
            c.BaseAddress = new Uri(apiBaseUrl));

        services.AddHttpClient<IFormApiService, FormApiService>(c =>
            c.BaseAddress = new Uri(apiBaseUrl));

        return services;
    }
}
