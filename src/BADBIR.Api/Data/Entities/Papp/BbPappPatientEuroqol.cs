namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for EuroQol EQ-5D-3L submissions.
/// Column names mirror the legacy papp API contract (mobility, selfcare, etc.).
/// <see cref="Comphealth"/> = overall self-rated health (VAS, 0–100).
/// <see cref="Howyoufeel"/> = the "how you feel today" VAS integer.
/// </summary>
public class BbPappPatientEuroqol
{
    public int PappEuroqolId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    // ── EQ-5D-3L dimensions (1=No problems, 2=Some, 3=Extreme) ──────────────
    public int? Mobility { get; set; }
    public int? Selfcare { get; set; }
    public int? Usualacts { get; set; }
    public int? Paindisc { get; set; }
    public int? Anxdepr { get; set; }

    /// <summary>Composite health state index (calculated from profile).</summary>
    public int? Comphealth { get; set; }

    /// <summary>VAS "How you feel today" (0–100).</summary>
    public int? Howyoufeel { get; set; }

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
