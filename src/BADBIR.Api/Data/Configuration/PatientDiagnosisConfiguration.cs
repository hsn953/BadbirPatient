using BADBIR.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BADBIR.Api.Data.Configuration;

public class PatientDiagnosisConfiguration : IEntityTypeConfiguration<PatientDiagnosis>
{
    public void Configure(EntityTypeBuilder<PatientDiagnosis> builder)
    {
        builder.ToTable("PatientDiagnoses");
        builder.HasKey(d => d.DiagnosisId);

        builder.Property(d => d.DiagnosisCode).IsRequired().HasMaxLength(20);
        builder.Property(d => d.DiagnosisName).IsRequired().HasMaxLength(200);
        builder.Property(d => d.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(d => d.PatientId);
    }
}
