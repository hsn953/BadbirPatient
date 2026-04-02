namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted HADS (Hospital Anxiety and Depression Scale).
/// 14 items: Q01–Q07 Anxiety, Q02–Q14 Depression (each 0–3).
/// Replaces bbPappPatientHad.
/// </summary>
public class HadsSubmission
{
    public int HadsId { get; set; }
    public int VisitId { get; set; }

    // Anxiety items (odd)
    public int? Q01tense { get; set; }
    public int? Q03frightened { get; set; }
    public int? Q05worry { get; set; }
    public int? Q07relaxed { get; set; }
    public int? Q09butterflies { get; set; }
    public int? Q11restless { get; set; }
    public int? Q13panic { get; set; }

    // Depression items (even)
    public int? Q02enjoy { get; set; }
    public int? Q04laugh { get; set; }
    public int? Q06cheerful { get; set; }
    public int? Q08slowed { get; set; }
    public int? Q10appearence { get; set; }
    public int? Q12enjoyment { get; set; }
    public int? Q14goodbook { get; set; }

    public int? ScoreAnxiety { get; set; }
    public int? ResultAnxiety { get; set; }
    public int? ScoreDepression { get; set; }
    public int? ResultDepression { get; set; }
    public DateTime? DateScored { get; set; }

    /// <summary>True once all 14 items answered — used for GL Assessments billing.</summary>
    public bool IsCountable { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
