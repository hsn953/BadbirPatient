namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientLifestyle].
/// Lifestyle / demographics section submitted at baseline and each follow-up.
/// PK is FupId (one row per follow-up visit).
/// <see cref="Birthtown"/> and <see cref="Birthcountry"/> are AES-encrypted.
/// </summary>
public class BbPatientLifestyle
{
    /// <summary>FK &amp; PK — bbPatientCohortTracking.FupId.</summary>
    public int FupId { get; set; }

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

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPatientCohortTracking? CohortTracking { get; set; }
}
