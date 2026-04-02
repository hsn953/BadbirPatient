using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Services;
using BADBIR.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BADBIR.Api.Controllers;

/// <summary>
/// Custom authentication endpoints for the BADBIR Patient App.
///
/// <list type="bullet">
///   <item>
///     <c>POST /api/auth/register</c> — patient self-registration with
///     Clinician System identity pre-verification.
///   </item>
///   <item>
///     <c>POST /api/auth/login</c> — authenticate and receive a signed JWT.
///   </item>
/// </list>
///
/// Other Identity-related endpoints (manage/info, refresh, 2FA) remain
/// available under <c>/api/identity</c> via <c>MapIdentityApi</c>.
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser>  _userManager;
    private readonly IClinicianSystemClient        _clinicianClient;
    private readonly IConfiguration                _config;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IClinicianSystemClient       clinicianClient,
        IConfiguration               config)
    {
        _userManager     = userManager;
        _clinicianClient = clinicianClient;
        _config          = config;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // POST /api/auth/register
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Patient self-registration with identity pre-verification.
    ///
    /// Step 1 — The patient's DOB, initials, and identification number are
    ///          checked against the Clinician System (currently a configurable
    ///          stub — see <c>ClinicianSystem:StubMode</c> in appsettings).
    ///
    /// Step 2 — If verification passes, an <see cref="ApplicationUser"/>
    ///          account is created via ASP.NET Core Identity.
    ///
    /// At least one of <c>NhsNumber</c> or <c>ChiNumber</c> must be provided.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] PatientRegisterRequestDto dto)
    {
        // ── 1. Basic guard: at least one ID number required ───────────────────
        if (string.IsNullOrWhiteSpace(dto.NhsNumber) &&
            string.IsNullOrWhiteSpace(dto.ChiNumber) &&
            string.IsNullOrWhiteSpace(dto.BadbirStudyNumber))
        {
            return BadRequest(new { error = "At least one identification number (NhsNumber, ChiNumber, or BadbirStudyNumber) must be provided." });
        }

        // ── 2. External identity verification (stub or real Clinician System) ─
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
                error = "Identity verification failed. " +
                        "Please check your date of birth, initials, and identification number, " +
                        "then try again or contact your clinical team."
            });
        }

        // ── 3. Create the Identity account ────────────────────────────────────
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email    = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                errors = result.Errors.Select(e => e.Description)
            });
        }

        await _userManager.AddToRoleAsync(user, "Patient");

        return Ok(new { message = "Registration successful. You may now log in." });
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

        var roles       = await _userManager.GetRolesAsync(user);
        var expiryMins  = _config.GetValue("Jwt:ExpiryMinutes", 60);
        var expiry      = DateTime.UtcNow.AddMinutes(expiryMins);
        var token       = BuildJwt(user, roles, expiry);

        return Ok(new LoginResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            Expiry      = expiry,
            UserId      = user.Id,
            Email       = user.Email ?? string.Empty,
            Roles       = roles
        });
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
}
