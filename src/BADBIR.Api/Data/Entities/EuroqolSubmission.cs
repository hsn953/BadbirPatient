namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted EuroQol EQ-5D-3L form.
/// Replaces bbPappPatientEuroqol.
/// </summary>
public class EuroqolSubmission
{
    public int EuroqolId { get; set; }
    public int VisitId { get; set; }

    public int? Mobility { get; set; }
    public int? Selfcare { get; set; }
    public int? Usualacts { get; set; }
    public int? Paindisc { get; set; }
    public int? Anxdepr { get; set; }
    public int? Comphealth { get; set; }
    public int? Howyoufeel { get; set; }

    public DateTime? DateCompleted { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
