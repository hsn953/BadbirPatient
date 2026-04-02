namespace BADBIR.Api.Services;

/// <summary>
/// Abstraction over the email delivery mechanism.
/// In development a stub implementation logs emails to the console.
/// In production this is replaced by a real SMTP / AWS SES implementation.
/// </summary>
public interface IEmailService
{
    /// <summary>Sends the email verification link to a newly registered patient.</summary>
    Task SendVerificationEmailAsync(string toEmail, string verificationUrl, CancellationToken ct = default);

    /// <summary>Sends a registration confirmation email.</summary>
    Task SendRegistrationConfirmationAsync(string toEmail, CancellationToken ct = default);

    /// <summary>Sends the 14-day holding expiry warning (at day 7 and day 13).</summary>
    Task SendHoldingExpiryWarningAsync(string toEmail, int daysRemaining, CancellationToken ct = default);

    /// <summary>Sends an account recovery link to an identified patient.</summary>
    Task SendAccountRecoveryEmailAsync(string toEmail, string recoveryUrl, CancellationToken ct = default);
}
