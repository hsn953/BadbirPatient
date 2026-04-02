using Microsoft.Extensions.Logging;

namespace BADBIR.Api.Services;

/// <summary>
/// Stub email service for development. Logs all emails to the application logger.
/// Replace with a real SMTP / AWS SES implementation for staging/production.
/// </summary>
public class StubEmailService : IEmailService
{
    private readonly ILogger<StubEmailService> _logger;

    public StubEmailService(ILogger<StubEmailService> logger)
        => _logger = logger;

    public Task SendVerificationEmailAsync(string toEmail, string verificationUrl, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "[STUB EMAIL] Verification email to {Email} — URL: {Url}",
            toEmail, verificationUrl);
        return Task.CompletedTask;
    }

    public Task SendRegistrationConfirmationAsync(string toEmail, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "[STUB EMAIL] Registration confirmation to {Email}",
            toEmail);
        return Task.CompletedTask;
    }

    public Task SendHoldingExpiryWarningAsync(string toEmail, int daysRemaining, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "[STUB EMAIL] Holding expiry warning to {Email} — {Days} day(s) remaining",
            toEmail, daysRemaining);
        return Task.CompletedTask;
    }

    public Task SendAccountRecoveryEmailAsync(string toEmail, string recoveryUrl, CancellationToken ct = default)
    {
        _logger.LogInformation(
            "[STUB EMAIL] Account recovery email to {Email} — URL: {Url}",
            toEmail, recoveryUrl);
        return Task.CompletedTask;
    }
}
