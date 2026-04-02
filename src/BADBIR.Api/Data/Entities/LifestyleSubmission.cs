namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Patient-submitted Lifestyle / demographics form data.
/// Replaces bbPappPatientLifestyle.
/// Birthtown and Birthcountry are AES-encrypted.
/// </summary>
public class LifestyleSubmission
{
    public int LifestyleId { get; set; }
    public int VisitId { get; set; }

    public string? Birthtown { get; set; }
    public string? Birthcountry { get; set; }
    public int? Workstatusid { get; set; }
    public string? Occupation { get; set; }
    public int? Ethnicityid { get; set; }
    public string? Otherethnicity { get; set; }
    public bool? Outdooroccupation { get; set; }
    public bool? Livetropical { get; set; }
    public bool? Eversmoked { get; set; }
    public int? Eversmokednumbercigsperday { get; set; }
    public int? Agestart { get; set; }
    public int? Agestop { get; set; }
    public bool? Currentlysmoke { get; set; }
    public int? Currentlysmokenumbercigsperday { get; set; }
    public int? Drnkbeeravg { get; set; }
    public int? Drnkwineavg { get; set; }
    public int? Drnkspiritsavg { get; set; }
    public bool? Drinkalcohol { get; set; }
    public int? Drnkunitsavg { get; set; }
    public int? Admittedtohospital { get; set; }
    public int? Newdrugs { get; set; }
    public int? Newclinics { get; set; }
    public float? Systolic { get; set; }
    public float? Diastolic { get; set; }
    public float? Height { get; set; }
    public float? Weight { get; set; }
    public float? Waist { get; set; }
    public bool WeightMissing { get; set; }
    public bool WaistMissing { get; set; }
    public bool SmokingMissing { get; set; }
    public bool DrinkingMissing { get; set; }
    public DateTime? DateCompleted { get; set; }

    /// <summary>0=Holding, 1=Approved, 2=Rejected.</summary>
    public byte DataStatus { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }

    public VisitTracking? Visit { get; set; }
}
