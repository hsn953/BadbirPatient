using BADBIR.Api.Data.Entities;
using BADBIR.Api.Data.Entities.Papp;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BADBIR.Api.Data;

/// <summary>
/// Main EF Core DbContext for the BADBIR Patient Application.
/// Inherits from IdentityDbContext to include ASP.NET Core Identity tables.
///
/// Shared live tables (read/insert by Patient App, schema owned by Clinician System):
///   bbPatient, bbPatientCohortHistory, bbPatientCohortTracking,
///   bbPatientDLQI, bbPatientLifestyle, bbPatientCage, bbPatientPASIScores.
///
/// Papp holding tables (owned by Patient App — defined in tests/TestData/papp_tables_ddl.sql):
///   bbPappPatientCohortTracking, bbPappPatientDlqi, bbPappPatientLifestyle,
///   bbPappPatientCage, bbPappPatientEuroqol, bbPappPatientHad,
///   bbPappPatientHaq, bbPappPatientPgaScore, bbPappPatientSapasi.
/// </summary>
public class BadbirDbContext : IdentityDbContext<ApplicationUser>
{
    public BadbirDbContext(DbContextOptions<BadbirDbContext> options) : base(options) { }

    // ── Shared live tables (read-from / insert-into) ──────────────────────────
    public DbSet<BbPatient>                  BbPatients                  => Set<BbPatient>();
    public DbSet<BbPatientCohortHistory>     BbPatientCohortHistories    => Set<BbPatientCohortHistory>();
    public DbSet<BbPatientCohortTracking>    BbPatientCohortTrackings    => Set<BbPatientCohortTracking>();
    public DbSet<BbPatientDlqi>              BbPatientDlqis              => Set<BbPatientDlqi>();
    public DbSet<BbPatientLifestyle>         BbPatientLifestyles         => Set<BbPatientLifestyle>();
    public DbSet<BbPatientCage>              BbPatientCages              => Set<BbPatientCage>();
    public DbSet<BbPatientPasiScores>        BbPatientPasiScores         => Set<BbPatientPasiScores>();

    // ── Papp holding tables ───────────────────────────────────────────────────
    public DbSet<BbPappPatientCohortTracking> PappCohortTrackings        => Set<BbPappPatientCohortTracking>();
    public DbSet<BbPappPatientDlqi>           PappDlqis                  => Set<BbPappPatientDlqi>();
    public DbSet<BbPappPatientLifestyle>      PappLifestyles             => Set<BbPappPatientLifestyle>();
    public DbSet<BbPappPatientCage>           PappCages                  => Set<BbPappPatientCage>();
    public DbSet<BbPappPatientEuroqol>        PappEuroqols               => Set<BbPappPatientEuroqol>();
    public DbSet<BbPappPatientHad>            PappHads                   => Set<BbPappPatientHad>();
    public DbSet<BbPappPatientHaq>            PappHaqs                   => Set<BbPappPatientHaq>();
    public DbSet<BbPappPatientPgaScore>       PappPgaScores              => Set<BbPappPatientPgaScore>();
    public DbSet<BbPappPatientSapasi>         PappSapasis                => Set<BbPappPatientSapasi>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // must be called first – configures Identity tables

        // ── Shared live tables — map to exact column names from badbir_synthetic.db ──

        builder.Entity<BbPatient>(e =>
        {
            e.ToTable("bbPatient");
            e.HasKey(p => p.Patientid);
            e.Property(p => p.AddressTown).HasColumnName("address_town");
            e.Property(p => p.AddressCounty).HasColumnName("address_county");
            e.Property(p => p.AddressPostcode).HasColumnName("address_postcode");
            e.Property(p => p.LosttoFUP).HasColumnName("losttoFUP");
            e.Property(p => p.AltStudyID234Digits).HasColumnName("altStudyID234Digits");
            e.Property(p => p.AltDeathDate).HasColumnName("altDeathDate");
            e.Property(p => p.ConsentedByBadbirUserID).HasColumnName("consentedByBadbirUserID");
            e.Property(p => p.RemoteConsent).HasColumnName("remoteConsent");
            e.Property(p => p.PortalIsRegistered).HasColumnName("Portal_IsRegistered");
            e.Property(p => p.PortalDateRegistered).HasColumnName("Portal_DateRegistered");
            // Portal user link (nullable — patient may not have registered via portal yet)
            e.HasOne(p => p.PortalUser)
             .WithOne(u => u.Patient)
             .HasForeignKey<ApplicationUser>(u => u.PatientId)
             .IsRequired(false);
        });

        builder.Entity<BbPatientCohortHistory>(e =>
        {
            e.ToTable("bbPatientCohortHistory");
            e.HasKey(h => h.Chid);
            e.Property(h => h.DatetoFUP).HasColumnName("datetoFUP");
            e.HasOne(h => h.Patient)
             .WithMany(p => p.CohortHistories)
             .HasForeignKey(h => h.Patientid);
        });

        builder.Entity<BbPatientCohortTracking>(e =>
        {
            e.ToTable("bbPatientCohortTracking");
            e.HasKey(t => t.FupId);
            e.Property(t => t.ClinicVisitdate).HasColumnName("clinicVisitdate");
            e.Property(t => t.EditWindowFrom).HasColumnName("editWindowFrom");
            e.Property(t => t.EditWindowTo).HasColumnName("editWindowTo");
            e.Property(t => t.ClinicAttendance).HasColumnName("clinicAttendance");
            e.Property(t => t.HasPgaScore).HasColumnName("hasPgaScore");
            e.Property(t => t.DlqiCompleted).HasColumnName("dlqiCompleted");
            e.Property(t => t.CdlqiCompleted).HasColumnName("cdlqiCompleted");
            e.Property(t => t.EuroqolCompleted).HasColumnName("euroqolCompleted");
            e.Property(t => t.CageApplicable).HasColumnName("cageApplicable");
            e.Property(t => t.CageCompleted).HasColumnName("cageCompleted");
            e.Property(t => t.HadsCompleted).HasColumnName("hadsCompleted");
            e.Property(t => t.HaqApplicable).HasColumnName("haqApplicable");
            e.Property(t => t.HaqCompleted).HasColumnName("haqCompleted");
            e.Property(t => t.TruncatedFupQvis).HasColumnName("truncated_fup_qvis");
            e.Property(t => t.TruncatedFupApplicable).HasColumnName("truncated_fup_applicable");
            e.Property(t => t.HaspsoriaticArthiritis).HasColumnName("haspsoriaticarthiritis");
            e.Property(t => t.HasinflamatoryArthiritis).HasColumnName("hasinflamatoryarthiritis");
            e.Property(t => t.PsoriaticarthiritisonSet).HasColumnName("psoriaticarthiritisonset");
            e.Property(t => t.PsoriaticarthiritisonSetdate).HasColumnName("psoriaticarthiritisonsetdate");
            e.Property(t => t.HasnoSMITherapy).HasColumnName("hasnoSMITherapy");
            e.HasOne(t => t.CohortHistory)
             .WithMany(h => h.CohortTrackings)
             .HasForeignKey(t => t.Chid);
            e.HasOne(t => t.Dlqi).WithOne(d => d.CohortTracking).HasForeignKey<BbPatientDlqi>(d => d.FupId);
            e.HasOne(t => t.Lifestyle).WithOne(l => l.CohortTracking).HasForeignKey<BbPatientLifestyle>(l => l.FupId);
            e.HasMany(t => t.CageSubmissions).WithOne(c => c.CohortTracking).HasForeignKey(c => c.FupId);
            e.HasMany(t => t.PasiScores).WithOne(s => s.CohortTracking).HasForeignKey(s => s.FupId);
        });

        builder.Entity<BbPatientDlqi>(e =>
        {
            e.ToTable("bbPatientDLQI");
            e.HasKey(d => d.DlqiID);
            e.Property(d => d.ItchsoreScore).HasColumnName("itchsore_score");
            e.Property(d => d.EmbscScore).HasColumnName("embsc_score");
            e.Property(d => d.ShophgScore).HasColumnName("shophg_score");
            e.Property(d => d.ClothesScore).HasColumnName("clothes_score");
            e.Property(d => d.SocleisScore).HasColumnName("socleis_score");
            e.Property(d => d.SportScore).HasColumnName("sport_score");
            e.Property(d => d.WorkstudScore).HasColumnName("workstud_score");
            e.Property(d => d.WorkstudnoScore).HasColumnName("workstudno_score");
            e.Property(d => d.PartcrfScore).HasColumnName("partcrf_score");
            e.Property(d => d.SexdifScore).HasColumnName("sexdif_score");
            e.Property(d => d.TreatmentScore).HasColumnName("treatment_score");
            e.Property(d => d.TotalScore).HasColumnName("total_score");
            e.Property(d => d.SkipBreakup).HasColumnName("skipBreakup");
            e.Property(d => d.QSourceId).HasColumnName("qSourceID");
        });

        builder.Entity<BbPatientLifestyle>(e =>
        {
            e.ToTable("bbPatientLifestyle");
            e.HasKey(l => l.FupId);
            e.Property(l => l.WeightMissing).HasColumnName("weightMissing");
            e.Property(l => l.WaistMissing).HasColumnName("waistMissing");
            e.Property(l => l.SmokingMissing).HasColumnName("smokingMissing");
            e.Property(l => l.DrinkingMissing).HasColumnName("drinkingMissing");
        });

        builder.Entity<BbPatientCage>(e =>
        {
            e.ToTable("bbPatientCage");
            e.HasKey(c => c.CageID);
            e.Property(c => c.QSourceId).HasColumnName("qSourceID");
        });

        builder.Entity<BbPatientPasiScores>(e =>
        {
            e.ToTable("bbPatientPASIScores");
            e.HasKey(s => s.PASIid);
            e.Property(s => s.PsglobNotSupplied).HasColumnName("psglob_not_supplied");
            e.Property(s => s.PasiAttendance).HasColumnName("pasiAttendance");
            e.Property(s => s.PgaSource).HasColumnName("pgaSource");
            e.Property(s => s.PgaAttendance).HasColumnName("pgaAttendance");
            e.Property(s => s.PasiSource).HasColumnName("pasiSource");
        });

        // ── Papp holding tables ───────────────────────────────────────────────

        builder.Entity<BbPappPatientCohortTracking>(e =>
        {
            e.ToTable("bbPappPatientCohortTracking");
            e.HasKey(t => t.PappFupId);
            e.HasOne(t => t.Patient).WithMany().HasForeignKey(t => t.PatientId);
            e.HasOne(t => t.Dlqi).WithOne(d => d.CohortTracking).HasForeignKey<BbPappPatientDlqi>(d => d.PappFupId);
            e.HasOne(t => t.Lifestyle).WithOne(l => l.CohortTracking).HasForeignKey<BbPappPatientLifestyle>(l => l.PappFupId);
            e.HasOne(t => t.Cage).WithOne(c => c.CohortTracking).HasForeignKey<BbPappPatientCage>(c => c.PappFupId);
            e.HasOne(t => t.Euroqol).WithOne(eq => eq.CohortTracking).HasForeignKey<BbPappPatientEuroqol>(eq => eq.PappFupId);
            e.HasOne(t => t.Had).WithOne(h => h.CohortTracking).HasForeignKey<BbPappPatientHad>(h => h.PappFupId);
            e.HasOne(t => t.Haq).WithOne(h => h.CohortTracking).HasForeignKey<BbPappPatientHaq>(h => h.PappFupId);
            e.HasOne(t => t.PgaScore).WithOne(p => p.CohortTracking).HasForeignKey<BbPappPatientPgaScore>(p => p.PappFupId);
            e.HasOne(t => t.Sapasi).WithOne(s => s.CohortTracking).HasForeignKey<BbPappPatientSapasi>(s => s.PappFupId);
        });

        builder.Entity<BbPappPatientDlqi>(e =>
        {
            e.ToTable("bbPappPatientDlqi");
            e.HasKey(d => d.PappDlqiId);
            e.Property(d => d.ItchsoreScore).HasColumnName("itchsore_score");
            e.Property(d => d.EmbscScore).HasColumnName("embsc_score");
            e.Property(d => d.ShophgScore).HasColumnName("shophg_score");
            e.Property(d => d.ClothesScore).HasColumnName("clothes_score");
            e.Property(d => d.SocleisScore).HasColumnName("socleis_score");
            e.Property(d => d.SportScore).HasColumnName("sport_score");
            e.Property(d => d.WorkstudScore).HasColumnName("workstud_score");
            e.Property(d => d.WorkstudnoScore).HasColumnName("workstudno_score");
            e.Property(d => d.PartcrfScore).HasColumnName("partcrf_score");
            e.Property(d => d.SexdifScore).HasColumnName("sexdif_score");
            e.Property(d => d.TreatmentScore).HasColumnName("treatment_score");
            e.Property(d => d.TotalScore).HasColumnName("total_score");
            e.Property(d => d.SkipBreakup).HasColumnName("skipBreakup");
        });

        builder.Entity<BbPappPatientLifestyle>(e =>
        {
            e.ToTable("bbPappPatientLifestyle");
            e.HasKey(l => l.PappLifestyleId);
            e.Property(l => l.WeightMissing).HasColumnName("weightMissing");
            e.Property(l => l.WaistMissing).HasColumnName("waistMissing");
            e.Property(l => l.SmokingMissing).HasColumnName("smokingMissing");
            e.Property(l => l.DrinkingMissing).HasColumnName("drinkingMissing");
        });

        builder.Entity<BbPappPatientCage>(e =>
        {
            e.ToTable("bbPappPatientCage");
            e.HasKey(c => c.PappCageId);
        });

        builder.Entity<BbPappPatientEuroqol>(e =>
        {
            e.ToTable("bbPappPatientEuroqol");
            e.HasKey(eq => eq.PappEuroqolId);
        });

        builder.Entity<BbPappPatientHad>(e =>
        {
            e.ToTable("bbPappPatientHad");
            e.HasKey(h => h.PappHadId);
        });

        builder.Entity<BbPappPatientHaq>(e =>
        {
            e.ToTable("bbPappPatientHaq");
            e.HasKey(h => h.PappHaqId);
        });

        builder.Entity<BbPappPatientPgaScore>(e =>
        {
            e.ToTable("bbPappPatientPgaScore");
            e.HasKey(p => p.PappPgaId);
        });

        builder.Entity<BbPappPatientSapasi>(e =>
        {
            e.ToTable("bbPappPatientSapasi");
            e.HasKey(s => s.PappSapasiId);
        });
    }
}
