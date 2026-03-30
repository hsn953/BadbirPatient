namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for CAGE alcohol screening submissions.
/// Mirrors <see cref="BbPatientCage"/> column-for-column.
/// </summary>
public class BbPappPatientCage
{
    public int PappCageId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    public bool? Cutdown { get; set; }
    public bool? Annoyed { get; set; }
    public bool? Guilty { get; set; }
    public bool? Earlymorning { get; set; }

    public DateTime? Datecomp { get; set; }

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
