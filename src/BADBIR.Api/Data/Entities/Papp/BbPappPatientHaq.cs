namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for HAQ-DI (Health Assessment Questionnaire – Disability Index).
/// Stores individual item responses across 8 functional categories.
/// The HAQ-DI score is calculated as the mean of the 8 category scores (each = max
/// of its items, adjusted upward if aids/devices are used), rounded to 3 d.p.
/// </summary>
public class BbPappPatientHaq
{
    public int PappHaqId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    public bool? Missingdata { get; set; }
    public string? Missingdatadetails { get; set; }

    // ── Category 1: Dressing &amp; Grooming ─────────────────────────────────────
    public int? Dressself { get; set; }
    public int? Shampoo { get; set; }

    // ── Category 2: Arising ──────────────────────────────────────────────────
    public int? Standchair { get; set; }
    public int? Bed { get; set; }

    // ── Category 3: Eating ───────────────────────────────────────────────────
    public int? Cutmeat { get; set; }
    public int? Liftglass { get; set; }
    public int? Openmilk { get; set; }

    // ── Category 4: Walking ──────────────────────────────────────────────────
    public int? Walkflat { get; set; }
    public int? Climbsteps { get; set; }

    // ── Category 5: Hygiene ──────────────────────────────────────────────────
    public int? Washdry { get; set; }
    public int? Bath { get; set; }
    public int? Toilet { get; set; }

    // ── Category 6: Reach ────────────────────────────────────────────────────
    public int? Reachabove { get; set; }
    public int? Bend { get; set; }

    // ── Category 7: Grip ─────────────────────────────────────────────────────
    public int? Cardoor { get; set; }
    public int? Openjar { get; set; }
    public int? Turntap { get; set; }

    // ── Category 8: Activities ───────────────────────────────────────────────
    public int? Shop { get; set; }
    public int? Getincar { get; set; }
    public int? Housework { get; set; }

    // ── Aids &amp; Devices ────────────────────────────────────────────────────────
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

    // ── Computed category scores (0–3 each) ──────────────────────────────────
    public int? Dressgroom { get; set; }
    public int? Rising { get; set; }
    public int? Eating { get; set; }
    public int? Walking { get; set; }
    public int? Hygiene { get; set; }
    public int? Reach { get; set; }
    public int? Gripping { get; set; }
    public int? Errands { get; set; }

    /// <summary>Raw sum of category scores before division.</summary>
    public int? Totalscore { get; set; }

    /// <summary>HAQ Disability Index = mean of 8 category scores (0.000–3.000).</summary>
    public double? Haqscore { get; set; }

    public DateTime? DateScored { get; set; }

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
