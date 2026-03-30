namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientPASIScores].
/// Records both the PASI score and PGA (Physician Global Assessment) score
/// for a follow-up visit.
/// </summary>
public class BbPatientPasiScores
{
    public int PASIid { get; set; }

    /// <summary>FK to bbPatientCohortTracking.FupId.</summary>
    public int FupId { get; set; }

    /// <summary>PASI score (0–72).</summary>
    public float? Pasi { get; set; }

    /// <summary>FK to bbPGASlkp.psglobid — PGA category (1=Clear … 5=Severe).</summary>
    public int? Psglobid { get; set; }

    public bool? PsglobNotSupplied { get; set; }

    /// <summary>Body surface area percentage (0–100).</summary>
    public int? Bsa { get; set; }

    public DateTime? Pasidate { get; set; }

    public int? PasiAttendance { get; set; }

    /// <summary>Source of PGA entry: 1=Clinician direct, 2=Patient app derived.</summary>
    public int? PgaSource { get; set; }

    public int? PgaAttendance { get; set; }
    public int? PasiSource { get; set; }

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
