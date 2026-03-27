namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for Lifestyle / demographics submissions.
/// Mirrors <see cref="BbPatientLifestyle"/> column-for-column.
/// <see cref="Birthtown"/> and <see cref="Birthcountry"/> are AES-encrypted
/// using the same algorithm as the Clinician System.
/// </summary>
public class BbPappPatientLifestyle
{
    public int PappLifestyleId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    /// <summary>Birth town — AES encrypted.</summary>
    public string? Birthtown { get; set; }

    /// <summary>Birth country — AES encrypted.</summary>
    public string? Birthcountry { get; set; }

    /// <summary>FK to bbWorkStatuslkp.worstatuskid.</summary>
    public int? Workstatusid { get; set; }

    public string? Occupation { get; set; }

    /// <summary>FK to bbEthnicitylkp.ethnicityid.</summary>
    public int? Ethnicityid { get; set; }

    public string? Otherethnicity { get; set; }
    public bool? Outdooroccupation { get; set; }
    public bool? Livetropical { get; set; }

    // ── Smoking ──────────────────────────────────────────────────────────────
    public bool? Eversmoked { get; set; }
    public int? Eversmokednumbercigsperday { get; set; }
    public int? Agestart { get; set; }
    public int? Agestop { get; set; }
    public bool? Currentlysmoke { get; set; }
    public int? Currentlysmokenumbercigsperday { get; set; }

    // ── Alcohol ───────────────────────────────────────────────────────────────
    public int? Drnkbeeravg { get; set; }
    public int? Drnkwineavg { get; set; }
    public int? Drnkspiritsavg { get; set; }
    public bool? Drinkalcohol { get; set; }
    public int? Drnkunitsavg { get; set; }

    // ── Medical history ───────────────────────────────────────────────────────
    public int? Admittedtohospital { get; set; }
    public int? Newdrugs { get; set; }
    public int? Newclinics { get; set; }

    // ── Physical measurements ─────────────────────────────────────────────────
    public float? Systolic { get; set; }
    public float? Diastolic { get; set; }
    public float? Height { get; set; }
    public float? Weight { get; set; }
    public float? Waist { get; set; }

    // ── Missing-data flags ────────────────────────────────────────────────────
    public bool WeightMissing { get; set; }
    public bool WaistMissing { get; set; }
    public bool SmokingMissing { get; set; }
    public bool DrinkingMissing { get; set; }

    public DateTime? DateCompleted { get; set; }

    /// <summary>0 = Holding, 1 = Approved, 2 = Rejected.</summary>
    public byte DataStatus { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPappPatientCohortTracking? CohortTracking { get; set; }
}
