using BADBIR.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Data;

/// <summary>
/// Main EF Core DbContext for the BADBIR Patient Application.
/// Inherits from IdentityDbContext to include ASP.NET Core Identity tables.
/// </summary>
public class BadbirDbContext : IdentityDbContext<ApplicationUser>
{
    public BadbirDbContext(DbContextOptions<BadbirDbContext> options) : base(options) { }

    // ── Domain DbSets ────────────────────────────────────────────────────
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<PatientDiagnosis> PatientDiagnoses => Set<PatientDiagnosis>();
    public DbSet<EuroQolSubmission> EuroQolSubmissions => Set<EuroQolSubmission>();
    public DbSet<HaqSubmission> HaqSubmissions => Set<HaqSubmission>();
    public DbSet<HadsSubmission> HadsSubmissions => Set<HadsSubmission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // must be called first – configures Identity tables

        builder.ApplyConfigurationsFromAssembly(typeof(BadbirDbContext).Assembly);
    }
}
