using BADBIR.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BADBIR.Api.Data.Configuration;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");
        builder.HasKey(p => p.PatientId);

        builder.Property(p => p.NhsNumber).IsRequired().HasMaxLength(20);
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Gender).HasMaxLength(20);
        builder.Property(p => p.Ethnicity).HasMaxLength(100);
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        builder.Property(p => p.UpdatedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(p => p.NhsNumber).IsUnique();
        builder.HasIndex(p => p.UserId).IsUnique();

        // One-to-one: Patient ← ApplicationUser
        builder.HasOne(p => p.User)
               .WithOne(u => u.Patient)
               .HasForeignKey<Patient>(p => p.UserId);

        // One-to-many: Patient → Diagnoses
        builder.HasMany(p => p.Diagnoses)
               .WithOne(d => d.Patient)
               .HasForeignKey(d => d.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        // One-to-many: Patient → PRO Forms
        builder.HasMany(p => p.EuroQolSubmissions)
               .WithOne(e => e.Patient)
               .HasForeignKey(e => e.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.HaqSubmissions)
               .WithOne(h => h.Patient)
               .HasForeignKey(h => h.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.HadsSubmissions)
               .WithOne(h => h.Patient)
               .HasForeignKey(h => h.PatientId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
