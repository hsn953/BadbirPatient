using BADBIR.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BADBIR.Api.Data.Configuration;

public class EuroQolSubmissionConfiguration : IEntityTypeConfiguration<EuroQolSubmission>
{
    public void Configure(EntityTypeBuilder<EuroQolSubmission> builder)
    {
        builder.ToTable("EuroQolSubmissions");
        builder.HasKey(e => e.SubmissionId);

        builder.Property(e => e.SubmittedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        builder.Property(e => e.IndexValue).HasColumnType("decimal(5,4)");

        builder.HasIndex(e => e.PatientId);
        builder.HasIndex(e => e.SubmittedAt);
    }
}

public class HaqSubmissionConfiguration : IEntityTypeConfiguration<HaqSubmission>
{
    public void Configure(EntityTypeBuilder<HaqSubmission> builder)
    {
        builder.ToTable("HaqSubmissions");
        builder.HasKey(h => h.SubmissionId);

        builder.Property(h => h.SubmittedAt).HasDefaultValueSql("SYSUTCDATETIME()");
        builder.Property(h => h.HaqDiScore).HasColumnType("decimal(4,3)");

        builder.HasIndex(h => h.PatientId);
        builder.HasIndex(h => h.SubmittedAt);
    }
}

public class HadsSubmissionConfiguration : IEntityTypeConfiguration<HadsSubmission>
{
    public void Configure(EntityTypeBuilder<HadsSubmission> builder)
    {
        builder.ToTable("HadsSubmissions");
        builder.HasKey(h => h.SubmissionId);

        builder.Property(h => h.SubmittedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(h => h.PatientId);
        builder.HasIndex(h => h.SubmittedAt);
    }
}
