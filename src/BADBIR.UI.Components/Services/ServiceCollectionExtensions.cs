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
    public static IServiceCollection AddBADBIRUIComponents(this IServiceCollection services)
    {
        // Future: register form state services, API client wrappers, etc.
        return services;
    }
}
