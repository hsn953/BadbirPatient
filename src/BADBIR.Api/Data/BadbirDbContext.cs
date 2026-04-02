using BADBIR.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Data;

/// <summary>
/// EF Core DbContext for the standalone BADBIR Patient Application database.
///
/// This database is completely independent of the Clinician System's BADBIR database.
/// All communication with the Clinician System happens through API endpoints
/// (see IClinicianSystemClient and the /api/internal/* routes).
///
/// Tables owned by this database:
///   AspNet* — ASP.NET Core Identity (users, roles, claims)
///   VisitTracking — one row per portal visit session
///   DlqiSubmissions, LifestyleSubmissions, CageSubmissions,
///   EuroqolSubmissions, HadsSubmissions, HaqSubmissions,
///   PgaSubmissions, SapasiSubmissions — patient-submitted form data
/// </summary>
public class BadbirDbContext : IdentityDbContext<ApplicationUser>
{
    public BadbirDbContext(DbContextOptions<BadbirDbContext> options) : base(options) { }

    // ── Portal tables ─────────────────────────────────────────────────────────
    public DbSet<VisitTracking>       VisitTrackings       => Set<VisitTracking>();
    public DbSet<DlqiSubmission>      DlqiSubmissions      => Set<DlqiSubmission>();
    public DbSet<LifestyleSubmission> LifestyleSubmissions => Set<LifestyleSubmission>();
    public DbSet<CageSubmission>      CageSubmissions      => Set<CageSubmission>();
    public DbSet<EuroqolSubmission>   EuroqolSubmissions   => Set<EuroqolSubmission>();
    public DbSet<HadsSubmission>      HadsSubmissions      => Set<HadsSubmission>();
    public DbSet<HaqSubmission>       HaqSubmissions       => Set<HaqSubmission>();
    public DbSet<PgaSubmission>       PgaSubmissions       => Set<PgaSubmission>();
    public DbSet<SapasiSubmission>    SapasiSubmissions    => Set<SapasiSubmission>();
    public DbSet<ConsentRecord>       ConsentRecords       => Set<ConsentRecord>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(e =>
        {
            e.HasMany(u => u.Visits)
             .WithOne(v => v.User)
             .HasForeignKey(v => v.UserId)
             .IsRequired();
        });

        builder.Entity<VisitTracking>(e =>
        {
            e.ToTable("VisitTracking");
            e.HasKey(v => v.VisitId);
            e.HasOne(v => v.Dlqi).WithOne(d => d.Visit).HasForeignKey<DlqiSubmission>(d => d.VisitId);
            e.HasOne(v => v.Lifestyle).WithOne(l => l.Visit).HasForeignKey<LifestyleSubmission>(l => l.VisitId);
            e.HasOne(v => v.Cage).WithOne(c => c.Visit).HasForeignKey<CageSubmission>(c => c.VisitId);
            e.HasOne(v => v.Euroqol).WithOne(eq => eq.Visit).HasForeignKey<EuroqolSubmission>(eq => eq.VisitId);
            e.HasOne(v => v.Hads).WithOne(h => h.Visit).HasForeignKey<HadsSubmission>(h => h.VisitId);
            e.HasOne(v => v.Haq).WithOne(h => h.Visit).HasForeignKey<HaqSubmission>(h => h.VisitId);
            e.HasOne(v => v.Pga).WithOne(p => p.Visit).HasForeignKey<PgaSubmission>(p => p.VisitId);
            e.HasOne(v => v.Sapasi).WithOne(s => s.Visit).HasForeignKey<SapasiSubmission>(s => s.VisitId);
        });

        builder.Entity<DlqiSubmission>(e =>
        {
            e.ToTable("DlqiSubmissions");
            e.HasKey(d => d.DlqiId);
        });

        builder.Entity<LifestyleSubmission>(e =>
        {
            e.ToTable("LifestyleSubmissions");
            e.HasKey(l => l.LifestyleId);
        });

        builder.Entity<CageSubmission>(e =>
        {
            e.ToTable("CageSubmissions");
            e.HasKey(c => c.CageId);
        });

        builder.Entity<EuroqolSubmission>(e =>
        {
            e.ToTable("EuroqolSubmissions");
            e.HasKey(eq => eq.EuroqolId);
        });

        builder.Entity<HadsSubmission>(e =>
        {
            e.ToTable("HadsSubmissions");
            e.HasKey(h => h.HadsId);
        });

        builder.Entity<HaqSubmission>(e =>
        {
            e.ToTable("HaqSubmissions");
            e.HasKey(h => h.HaqId);
        });

        builder.Entity<PgaSubmission>(e =>
        {
            e.ToTable("PgaSubmissions");
            e.HasKey(p => p.PgaId);
        });

        builder.Entity<SapasiSubmission>(e =>
        {
            e.ToTable("SapasiSubmissions");
            e.HasKey(s => s.SapasiId);
        });

        builder.Entity<ConsentRecord>(e =>
        {
            e.ToTable("ConsentRecords");
            e.HasKey(c => c.ConsentId);
            e.HasOne(c => c.User)
             .WithMany(u => u.ConsentRecords)
             .HasForeignKey(c => c.UserId)
             .IsRequired();
        });
    }
}
