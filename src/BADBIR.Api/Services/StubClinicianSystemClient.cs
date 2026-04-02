namespace BADBIR.Api.Services;

/// <summary>
/// Stub implementation of <see cref="IClinicianSystemClient"/> used during
/// development and testing when the real Clinician System is not available.
///
/// Behaviour is driven by <c>ClinicianSystem:StubMode</c> in appsettings:
/// <list type="bullet">
///   <item><c>AlwaysTrue</c>  — every identity check succeeds (default).</item>
///   <item><c>AlwaysFalse</c> — every identity check fails.</item>
/// </list>
/// </summary>
public sealed class StubClinicianSystemClient : IClinicianSystemClient
{
    private readonly bool _alwaysVerified;

    public StubClinicianSystemClient(IConfiguration configuration)
    {
        var mode = configuration["ClinicianSystem:StubMode"] ?? "AlwaysTrue";
        _alwaysVerified = !mode.Equals("AlwaysFalse", StringComparison.OrdinalIgnoreCase);
    }

    public Task<bool> VerifyIdentityAsync(
        DateOnly          dateOfBirth,
        string            initials,
        string?           nhsNumber,
        string?           chiNumber,
        string?           badbirStudyNumber,
        CancellationToken cancellationToken = default)
        => Task.FromResult(_alwaysVerified);
}
