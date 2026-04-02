using System.Text;
using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ── 1. Database ──────────────────────────────────────────────────────────────
// Use SQLite when the "Sqlite" connection string is present (dev / test),
// otherwise default to SQL Server (staging / production).
var sqliteConnStr = builder.Configuration.GetConnectionString("BadbirDbSqlite");
var sqlServerConnStr = builder.Configuration.GetConnectionString("BadbirDb");

if (!string.IsNullOrEmpty(sqliteConnStr))
{
    builder.Services.AddDbContext<BadbirDbContext>(options =>
        options.UseSqlite(sqliteConnStr));
}
else
{
    builder.Services.AddDbContext<BadbirDbContext>(options =>
        options.UseSqlServer(
            sqlServerConnStr
            ?? throw new InvalidOperationException("No database connection string configured."),
            sql => sql.EnableRetryOnFailure()));
}

// ── 2. ASP.NET Core Identity ─────────────────────────────────────────────────
builder.Services
    .AddIdentityCore<ApplicationUser>(options =>
    {
        // Password policy — these rules are shown in the OpenAPI docs and enforced here.
        // The frontend should also apply the same rules during field validation.
        options.Password.RequiredLength         = 8;
        options.Password.RequireUppercase       = true;
        options.Password.RequireLowercase       = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireDigit           = true;
        options.SignIn.RequireConfirmedEmail     = false; // set true in production
        options.User.RequireUniqueEmail          = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BadbirDbContext>()
    .AddApiEndpoints();  // enables MapIdentityApi<T> minimal-API endpoints

// ── 3. JWT Bearer Authentication ─────────────────────────────────────────────
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey     = jwtSection["Key"]
    ?? throw new InvalidOperationException("JWT key is not configured (Jwt:Key).");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwtSection["Issuer"],
            ValidAudience            = jwtSection["Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew                = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// ── 4. Application Services ───────────────────────────────────────────────────
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<IClinicianSystemClient, StubClinicianSystemClient>();

// ── 5. Controllers ───────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── 6. Native .NET 10 OpenAPI (no Swashbuckle) ───────────────────────────────
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info.Title   = "BADBIR Patient API";
        document.Info.Version = "v1";
        document.Info.Description =
            "REST API for the BADBIR Patient Application (v2).\n\n" +
            "## Authentication\n" +
            "Use `POST /api/auth/login` to obtain a Bearer JWT, then pass it in the " +
            "`Authorization: Bearer <token>` header on all protected endpoints.\n\n" +
            "## Password requirements\n" +
            "All patient passwords must satisfy the following rules:\n" +
            "- Minimum **8 characters**\n" +
            "- At least one **uppercase** letter (A–Z)\n" +
            "- At least one **lowercase** letter (a–z)\n" +
            "- At least one **digit** (0–9)\n" +
            "- At least one **non-alphanumeric** character (e.g. `!`, `@`, `#`, `$`, `%`)\n\n" +
            "These rules apply to `POST /api/auth/register`. " +
            "Validation errors are returned in the `errors` array of the 400 response.\n\n" +
            "## Registration identity verification\n" +
            "Before an account is created, the patient's identity is verified against the " +
            "Clinician System using date of birth, initials, and at least one of: " +
            "NHS number, CHI number, or BADBIR study number.";
        return Task.CompletedTask;
    });
});

// ── 7. CORS ───────────────────────────────────────────────────────────────────
var allowedOrigins = builder.Configuration
    .GetSection("AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
    options.AddPolicy("BadbirCorsPolicy", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()));

// ── Build ─────────────────────────────────────────────────────────────────────
var app = builder.Build();

// ── 8. Middleware pipeline ────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("BadbirCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

// ── 9. Route mapping ──────────────────────────────────────────────────────────
app.MapControllers();

// MapIdentityApi provides built-in endpoints:
//   POST /register, /login, /refresh, /confirmEmail, /resendConfirmationEmail
//   GET  /manage/info   POST /manage/info, /manage/2fa
// Routed under /api/identity so our custom AuthController owns /api/auth/register and /api/auth/login.
app.MapGroup("api/identity")
   .MapIdentityApi<ApplicationUser>()
   .RequireCors("BadbirCorsPolicy");

await app.RunAsync();

// Needed for WebApplicationFactory in integration tests
public partial class Program { }

