using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Services;
using BADBIR.Shared.Constants;
using BADBIR.Shared.DTOs;
using BADBIR.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Custom authentication endpoints for the BADBIR Patient App.
///
/// <list type="bullet">
///   <item><c>POST /api/auth/register</c> — identity-verified registration (Pathway A matched)</item>
///   <item><c>POST /api/auth/register/new</c> — first-time unverified registration (Pathway A unmatched)</item>
///   <item><c>POST /api/auth/login</c> — authenticate and receive a signed JWT</item>
///   <item><c>POST /api/auth/send-verification-email</c> — resend email verification link</item>
///   <item><c>GET  /api/auth/verify-email</c> — confirm email address from link</item>
///   <item><c>POST /api/auth/recovery/request</c> — Step 1 of account recovery (verify identity)</item>
///   <item><c>POST /api/auth/recovery/reset</c> — Step 2 of account recovery (set new credentials)</item>
/// </list>
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser>  _userManager;
    private readonly IClinicianSystemClient        _clinicianClient;
    private readonly IEmailService                 _emailService;
    private readonly IConfiguration                _config;
    private readonly BadbirDbContext               _db;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IClinicianSystemClient       clinicianClient,
        IEmailService                emailService,
        IConfiguration               config,
        BadbirDbContext              db)
    {
        _userManager     = userManager;
        _clinicianClient = clinicianClient;
        _emailService    = emailService;
        _config          = config;
        _db              = db;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/register
    // Pathway A (matched) — identity pre-verified against Clinician System
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Patient self-registration with identity pre-verification.
    /// Identity is verified against the Clinician System; on success an Active account is created.
    /// At least one of NhsNumber, ChiNumber, or BadbirStudyNumber must be provided.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PatientRegisterRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NhsNumber) &&
            string.IsNullOrWhiteSpace(dto.ChiNumber) &&
            string.IsNullOrWhiteSpace(dto.BadbirStudyNumber))
        {
            return BadRequest(new { error = "At least one identification number (NhsNumber, ChiNumber, or BadbirStudyNumber) must be provided." });
        }

        var verified = await _clinicianClient.VerifyIdentityAsync(
            dto.DateOfBirth,
            dto.Initials.Trim(),
            dto.NhsNumber,
            dto.ChiNumber,
            dto.BadbirStudyNumber);

        if (!verified)
        {
            return BadRequest(new
            {
                error         = "Identity verification failed.",
                identityMatch = false
            });
        }

        var user = new ApplicationUser
        {
            UserName           = dto.Email,
            Email              = dto.Email,
            DateOfBirth        = dto.DateOfBirth,
            Initials           = dto.Initials.Trim().ToUpperInvariant(),
            RegistrationStatus = RegistrationStatus.Active
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

        await _userManager.AddToRoleAsync(user, "Patient");
        await _emailService.SendRegistrationConfirmationAsync(user.Email!);

        return Ok(new { message = "Registration successful. You may now log in." });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/register/new
    // Pathway A (unmatched) — first-time registration; holding state; 14-day window
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Creates an unverified (holding) account for a patient whose identity
    /// could not be matched in the Clinician System.
    /// The account and all its data will be permanently deleted after 14 days
    /// if not confirmed by a clinician.
    /// </summary>
    [HttpPost("register/new")]
    public async Task<IActionResult> RegisterNew([FromBody] NewPatientRegistrationDto dto)
    {
        var user = new ApplicationUser
        {
            UserName           = dto.Email,
            Email              = dto.Email,
            DateOfBirth        = dto.DateOfBirth,
            Initials           = dto.Initials.Trim().ToUpperInvariant(),
            ClinicalCentre     = dto.ClinicalCentre,
            RegistrationStatus = RegistrationStatus.Holding,
            HoldingExpiry      = DateTime.UtcNow.AddDays(AppConstants.HoldingAccountExpiryDays)
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors.Select(e => e.Description) });

        await _userManager.AddToRoleAsync(user, "Patient");

        // Send email verification
        var verifyToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var verifyUrl   = BuildVerificationUrl(user.Id, verifyToken);
        await _emailService.SendVerificationEmailAsync(user.Email!, verifyUrl);

        return Ok(new
        {
            message      = "Holding account created. Please verify your email and your clinical team will confirm your registration within 14 days.",
            holdingExpiry = user.HoldingExpiry
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/send-verification-email
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>Resend the email verification link to the specified address.</summary>
    [HttpPost("send-verification-email")]
    public async Task<IActionResult> SendVerificationEmail([FromBody] ResendVerificationEmailDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return Ok(); // don't reveal whether the email exists

        if (user.EmailConfirmed)
            return Ok(new { message = "Email already verified." });

        var verifyToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var verifyUrl   = BuildVerificationUrl(user.Id, verifyToken);
        await _emailService.SendVerificationEmailAsync(user.Email!, verifyUrl);

        return Ok(new { message = "Verification email sent." });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // GET /api/auth/verify-email
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Confirms a patient's email address from the link sent during registration.
    /// The userId and token parameters are embedded in the verification URL.
    /// </summary>
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string userId, [FromQuery] string token)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
            return BadRequest(new { error = "Invalid verification link." });

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return BadRequest(new { error = "Invalid verification link." });

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            return BadRequest(new { error = "Email verification failed. The link may have expired." });

        return Ok(new { message = "Email verified successfully. You can now log in." });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/login
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Authenticate with email and password. Returns a signed JWT on success.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized(new { error = "Invalid email or password." });

        var roles      = await _userManager.GetRolesAsync(user);
        var expiryMins = _config.GetValue("Jwt:ExpiryMinutes", 60);
        var expiry     = DateTime.UtcNow.AddMinutes(expiryMins);
        var token      = BuildJwt(user, roles, expiry);

        // Load latest consent version
        var latestConsent = await _db.ConsentRecords
            .Where(c => c.UserId == user.Id)
            .OrderByDescending(c => c.ConsentTimestamp)
            .FirstOrDefaultAsync();

        var consentGiven = latestConsent != null &&
                           latestConsent.ConsentFormVersion == AppConstants.ConsentFormVersion;

        return Ok(new LoginResponseDto
        {
            AccessToken        = new JwtSecurityTokenHandler().WriteToken(token),
            Expiry             = expiry,
            UserId             = user.Id,
            Email              = user.Email ?? string.Empty,
            Roles              = roles,
            RegistrationStatus = user.RegistrationStatus,
            ConsentGiven       = consentGiven,
            SelfReportedDiagnosis = user.SelfReportedDiagnosis,
            HoldingExpiry      = user.HoldingExpiry
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/recovery/request
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Step 1 of account recovery — verify identity using DOB + initials + one ID number.
    /// On success, returns a short-lived recovery token and a masked email address.
    /// </summary>
    [HttpPost("recovery/request")]
    public async Task<ActionResult<AccountRecoveryTokenDto>> RecoveryRequest(
        [FromBody] AccountRecoveryRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NhsNumber) &&
            string.IsNullOrWhiteSpace(dto.ChiNumber) &&
            string.IsNullOrWhiteSpace(dto.BadbirStudyNumber))
        {
            return BadRequest(new { error = "At least one identification number must be provided." });
        }

        var verified = await _clinicianClient.VerifyIdentityAsync(
            dto.DateOfBirth,
            dto.Initials.Trim(),
            dto.NhsNumber,
            dto.ChiNumber,
            dto.BadbirStudyNumber);

        if (!verified)
            return BadRequest(new { error = "Identity verification failed. Please check your details." });

        // Find the user by matching DOB + initials in our own DB
        var initials = dto.Initials.Trim().ToUpperInvariant();
        var user = await _db.Users.FirstOrDefaultAsync(u =>
            u.DateOfBirth == dto.DateOfBirth &&
            u.Initials    == initials);

        if (user is null)
            return BadRequest(new { error = "No account found matching those details." });

        // Generate a password-reset token (doubles as recovery token)
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Mask the email for display (e.g. j***@example.com)
        var email  = user.Email ?? string.Empty;
        var atIdx  = email.IndexOf('@');
        var masked = atIdx > 1
            ? email[..1] + new string('*', atIdx - 1) + email[atIdx..]
            : "****";

        return Ok(new AccountRecoveryTokenDto
        {
            RecoveryToken = $"{user.Id}:{Uri.EscapeDataString(resetToken)}",
            MaskedEmail   = masked
        });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/recovery/reset
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Step 2 of account recovery — set new email and password using the
    /// recovery token from step 1. Invalidates all existing sessions.
    /// </summary>
    [HttpPost("recovery/reset")]
    public async Task<IActionResult> RecoveryReset([FromBody] AccountRecoveryResetDto dto)
    {
        var parts = dto.RecoveryToken.Split(':', 2);
        if (parts.Length != 2)
            return BadRequest(new { error = "Invalid recovery token." });

        var userId = parts[0];
        var token  = Uri.UnescapeDataString(parts[1]);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return BadRequest(new { error = "Invalid recovery token." });

        // Check email uniqueness if changing
        if (!string.Equals(user.Email, dto.NewEmail, StringComparison.OrdinalIgnoreCase))
        {
            var existing = await _userManager.FindByEmailAsync(dto.NewEmail);
            if (existing is not null)
                return BadRequest(new { error = "That email address is already in use." });

            user.Email    = dto.NewEmail;
            user.UserName = dto.NewEmail;
            await _userManager.UpdateAsync(user);
        }

        var resetResult = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);
        if (!resetResult.Succeeded)
            return BadRequest(new { errors = resetResult.Errors.Select(e => e.Description) });

        // Invalidate all existing tokens by rotating the security stamp
        await _userManager.UpdateSecurityStampAsync(user);

        return Ok(new { message = "Account updated. You can now log in with your new credentials." });
    }

    // ─────────────────────────────────────────────────────────────────────────
    // Helpers
    // ─────────────────────────────────────────────────────────────────────────

    private JwtSecurityToken BuildJwt(ApplicationUser user, IList<string> roles, DateTime expiry)
    {
        var jwtSection = _config.GetSection("Jwt");
        var key        = jwtSection["Key"]
            ?? throw new InvalidOperationException("JWT key not configured.");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub,   user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer:             jwtSection["Issuer"],
            audience:           jwtSection["Audience"],
            claims:             claims,
            expires:            expiry,
            signingCredentials: credentials);
    }

    private string BuildVerificationUrl(string userId, string token)
    {
        var baseUrl = _config["App:BaseUrl"] ?? "https://localhost:7001";
        return $"{baseUrl}/verify-email?userId={Uri.EscapeDataString(userId)}&token={Uri.EscapeDataString(token)}";
    }
}
