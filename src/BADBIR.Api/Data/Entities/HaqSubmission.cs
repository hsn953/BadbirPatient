namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted HAQ-DI (Health Assessment Questionnaire – Disability Index).
/// Replaces bbPappPatientHaq.
/// </summary>
public class HaqSubmission
{
    public int HaqId { get; set; }
    public int VisitId { get; set; }

    public bool? Missingdata { get; set; }
    public string? Missingdatadetails { get; set; }

    // Category 1: Dressing & Grooming
    public int? Dressself { get; set; }
    public int? Shampoo { get; set; }
    // Category 2: Arising
    public int? Standchair { get; set; }
    public int? Bed { get; set; }
    // Category 3: Eating
    public int? Cutmeat { get; set; }
    public int? Liftglass { get; set; }
    public int? Openmilk { get; set; }
    // Category 4: Walking
    public int? Walkflat { get; set; }
    public int? Climbsteps { get; set; }
    // Category 5: Hygiene
    public int? Washdry { get; set; }
    public int? Bath { get; set; }
    public int? Toilet { get; set; }
    // Category 6: Reach
    public int? Reachabove { get; set; }
    public int? Bend { get; set; }
    // Category 7: Grip
    public int? Cardoor { get; set; }
    public int? Openjar { get; set; }
    public int? Turntap { get; set; }
    // Category 8: Activities
    public int? Shop { get; set; }
    public int? Getincar { get; set; }
    public int? Housework { get; set; }
    // Aids & Devices
    public int? Cane { get; set; }
    public int? Crutches { get; set; }
    public int? Walker { get; set; }
    public int? Wheelchair { get; set; }
    public int? Specialutensils { get; set; }
    public int? Specialchair { get; set; }
    public int? Dressing { get; set; }
    public string? Dressingdetails { get; set; }
    public int? Loolift { get; set; }
    public int? Bathseat { get; set; }
    public int? Bathrail { get; set; }
    public int? Longreach { get; set; }
    public int? Jaropener { get; set; }
    public string? Deviceother { get; set; }

    // Category scores
    public int? Dressgroom { get; set; }
    public int? Rising { get; set; }
    public int? Eating { get; set; }
    public int? Walking { get; set; }
    public int? Hygiene { get; set; }
    public int? Reach { get; set; }
    public int? Gripping { get; set; }
    public int? Errands { get; set; }
    public int? Totalscore { get; set; }
    public double? Haqscore { get; set; }

    public DateTime? DateScored { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
