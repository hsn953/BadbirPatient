namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Papp holding table for SAPASI (Self-Assessment of Psoriasis Area and Severity Index).
/// New table — no equivalent in the legacy Clinician System.
/// Four body regions, each with an affected area percentage and severity score (0–3).
/// SAPASI total = sum of (area × severity × region_weight) across all four regions.
/// See FORM-007 for the full scoring formula.
/// </summary>
public class BbPappPatientSapasi
{
    public int PappSapasiId { get; set; }

    /// <summary>FK to bbPappPatientCohortTracking.PappFupId.</summary>
    public int PappFupId { get; set; }

    // ── Head region ──────────────────────────────────────────────────────────
    /// <summary>Percentage of head area affected (0–100).</summary>
    public float? HeadArea { get; set; }

    /// <summary>Head erythema severity (0–3).</summary>
    public int? HeadErythema { get; set; }

    /// <summary>Head induration/thickness severity (0–3).</summary>
    public int? HeadInduration { get; set; }

    /// <summary>Head desquamation/scaling severity (0–3).</summary>
    public int? HeadDesquamation { get; set; }

    // ── Trunk region ─────────────────────────────────────────────────────────
    public float? TrunkArea { get; set; }
    public int? TrunkErythema { get; set; }
    public int? TrunkInduration { get; set; }
    public int? TrunkDesquamation { get; set; }

    // ── Upper limbs ──────────────────────────────────────────────────────────
    public float? UpperLimbsArea { get; set; }
    public int? UpperLimbsErythema { get; set; }
    public int? UpperLimbsInduration { get; set; }
    public int? UpperLimbsDesquamation { get; set; }

    // ── Lower limbs ──────────────────────────────────────────────────────────
    public float? LowerLimbsArea { get; set; }
    public int? LowerLimbsErythema { get; set; }
    public int? LowerLimbsInduration { get; set; }
    public int? LowerLimbsDesquamation { get; set; }

    /// <summary>Calculated SAPASI total score (0–72).</summary>
    public float? SapasiScore { get; set; }

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
