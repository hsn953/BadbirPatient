using BADBIR.Api.Data;
using BADBIR.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Services;

/// <summary>
/// Background hosted service that runs once per day and permanently deletes
/// any holding accounts whose HoldingExpiry timestamp has passed without
/// clinician confirmation (FR-REG-05).
/// </summary>
public class HoldingAccountExpiryService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<HoldingAccountExpiryService> _logger;
    private static readonly TimeSpan CheckInterval = TimeSpan.FromHours(24);

    public HoldingAccountExpiryService(
        IServiceScopeFactory scopeFactory,
        ILogger<HoldingAccountExpiryService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger       = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("HoldingAccountExpiryService started.");

        // Run once immediately on startup, then on a 24-hour cycle.
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessExpiredAccountsAsync(stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _logger.LogError(ex, "Error in HoldingAccountExpiryService.");
            }

            await Task.Delay(CheckInterval, stoppingToken);
        }
    }

    private async Task ProcessExpiredAccountsAsync(CancellationToken ct)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BadbirDbContext>();

        var now = DateTime.UtcNow;

        var expiredUsers = await db.Users
            .Where(u => u.RegistrationStatus == RegistrationStatus.Holding
                     && u.HoldingExpiry != null
                     && u.HoldingExpiry < now)
            .ToListAsync(ct);

        if (expiredUsers.Count == 0) return;

        _logger.LogWarning(
            "HoldingAccountExpiryService: deleting {Count} expired holding account(s).",
            expiredUsers.Count);

        foreach (var user in expiredUsers)
        {
            // Cascade delete removes VisitTrackings and all form submissions
            // (configured in BadbirDbContext OnModelCreating).
            db.Users.Remove(user);
        }

        await db.SaveChangesAsync(ct);

        _logger.LogInformation(
            "HoldingAccountExpiryService: {Count} expired account(s) deleted.",
            expiredUsers.Count);
    }
}
