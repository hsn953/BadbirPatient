using System.Text;
using BADBIR.Api.Data;
using BADBIR.Api.Data.Entities;
using BADBIR.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        options.Password.RequiredLength         = 8;
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

// ── 5. Controllers ───────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── 6. Native .NET 10 OpenAPI (no Swashbuckle) ───────────────────────────────
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info.Title       = "BADBIR Patient API";
        document.Info.Version     = "v1";
        document.Info.Description =
            "REST API for the BADBIR Patient Application. " +
            "Authenticate via the Identity endpoints to obtain a bearer token.";
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
    // Serves the OpenAPI JSON at /openapi/v1.json
    app.MapOpenApi();
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
app.MapGroup("api/auth")
   .MapIdentityApi<ApplicationUser>()
   .RequireCors("BadbirCorsPolicy");

await app.RunAsync();

// Needed for WebApplicationFactory in integration tests
public partial class Program { }

