namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-reported PGA (Global Assessment) score.
/// Scale: 1=Clear, 2=Almost clear, 3=Mild, 4=Moderate, 5=Severe.
/// Replaces bbPappPatientPgaScore.
/// </summary>
public class PgaSubmission
{
    public int PgaId { get; set; }
    public int VisitId { get; set; }

    /// <summary>1=Clear … 5=Severe.</summary>
    public int? Pgascore { get; set; }

    public DateTime? DateScored { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
