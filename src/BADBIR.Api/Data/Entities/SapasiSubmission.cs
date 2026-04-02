namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted SAPASI (Self-Administered PASI).
/// Four body regions, each with coverage band (0–4) and three severity scores (0–4).
/// Replaces bbPappPatientSapasi.
/// </summary>
public class SapasiSubmission
{
    public int SapasiId { get; set; }
    public int VisitId { get; set; }

    // Head (weight 0.1)
    public int? HeadCoverage { get; set; }
    public int? HeadErythema { get; set; }
    public int? HeadInduration { get; set; }
    public int? HeadDesquamation { get; set; }

    // Trunk (weight 0.3)
    public int? TrunkCoverage { get; set; }
    public int? TrunkErythema { get; set; }
    public int? TrunkInduration { get; set; }
    public int? TrunkDesquamation { get; set; }

    // Upper Limbs (weight 0.2)
    public int? UpperLimbsCoverage { get; set; }
    public int? UpperLimbsErythema { get; set; }
    public int? UpperLimbsInduration { get; set; }
    public int? UpperLimbsDesquamation { get; set; }

    // Lower Limbs (weight 0.4)
    public int? LowerLimbsCoverage { get; set; }
    public int? LowerLimbsErythema { get; set; }
    public int? LowerLimbsInduration { get; set; }
    public int? LowerLimbsDesquamation { get; set; }

    public float? SapasiScore { get; set; }
    public DateTime? DateScored { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
