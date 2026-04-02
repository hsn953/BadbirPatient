namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted CAGE questionnaire (alcohol screening).
/// Replaces bbPappPatientCage.
/// </summary>
public class CageSubmission
{
    public int CageId { get; set; }
    public int VisitId { get; set; }

    public bool? Cutdown { get; set; }
    public bool? Annoyed { get; set; }
    public bool? Guilty { get; set; }
    public bool? Earlymorning { get; set; }

    public DateTime? DateCompleted { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
