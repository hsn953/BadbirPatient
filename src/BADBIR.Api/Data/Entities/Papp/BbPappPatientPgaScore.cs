namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for the patient-reported PGA (Physician Global Assessment)
/// equivalent — the patient self-assesses their skin on a 5-point scale.
/// Maps to bbPatientPASIScores.psglobid on promotion.
/// Scale: 1=Clear, 2=Almost clear, 3=Mild, 4=Moderate, 5=Severe.
/// </summary>
public class BbPappPatientPgaScore
{
    public int PappPgaId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    /// <summary>
    /// Patient-reported PGA (1=Clear … 5=Severe).
    /// Maps to bbPGASlkp.psglobid in the live table.
    /// </summary>
    public int? Pgascore { get; set; }

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
