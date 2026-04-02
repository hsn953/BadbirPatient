namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted DLQI (Dermatology Life Quality Index) form data.
/// Replaces bbPappPatientDlqi.
/// </summary>
public class DlqiSubmission
{
    public int DlqiId { get; set; }
    public int VisitId { get; set; }

    public string? Diagnosis { get; set; }

    // DLQI items (0–3 each; null = left blank)
    public int? ItchsoreScore { get; set; }
    public int? EmbscScore { get; set; }
    public int? ShophgScore { get; set; }
    public int? ClothesScore { get; set; }
    public int? SocleisScore { get; set; }
    public int? SportScore { get; set; }
    public int? WorkstudScore { get; set; }
    public int? WorkstudnoScore { get; set; }
    public int? PartcrfScore { get; set; }
    public int? SexdifScore { get; set; }
    public int? TreatmentScore { get; set; }
    public int? TotalScore { get; set; }

    public DateTime? DateCompleted { get; set; }

    /// <summary>1 if Q7 work/study is N/A.</summary>
    public int SkipBreakup { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
