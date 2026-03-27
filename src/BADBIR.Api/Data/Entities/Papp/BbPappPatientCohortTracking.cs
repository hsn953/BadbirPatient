namespace BADBIR.Api.Data.Entities.Papp;

/// <summary>
/// Patient-app holding record for a follow-up visit.
/// Mirrors <see cref="BbPatientCohortTracking"/> but lives in the papp schema
/// and remains here until the clinician promotes the patient to active status.
/// Once promoted, <see cref="ImportedFupId"/> is set to the newly created
/// bbPatientCohortTracking.FupId and <see cref="DataStatus"/> is set to 1.
/// </summary>
public class BbPappPatientCohortTracking
{
    public int PappFupId { get; set; }

    /// <summary>FK to bbPatient.patientid.</summary>
    public int PatientId { get; set; }

    /// <summary>Intended follow-up number once promoted (0 = Baseline).</summary>
    public int PotentialFupCode { get; set; }

    /// <summary>Set to bbPatientCohortTracking.FupId after promotion.</summary>
    public int? ImportedFupId { get; set; }

    public DateTime? VisitDate { get; set; }
    public DateTime Dateentered { get; set; }
    public string? Comments { get; set; }

    /// <summary>0 = Forms in progress, 1 = All mandatory forms complete, 2 = Cancelled.</summary>
    public int PappFupStatus { get; set; }

    /// <summary>
    /// Holding state flag:
    /// 0 = Holding (awaiting clinician review),
    /// 1 = Approved (promoted to live tables),
    /// 2 = Rejected (clinician rejected registration).
    /// </summary>
    public byte DataStatus { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPatient? Patient { get; set; }
    public BbPappPatientDlqi? Dlqi { get; set; }
    public BbPappPatientLifestyle? Lifestyle { get; set; }
    public BbPappPatientCage? Cage { get; set; }
    public BbPappPatientEuroqol? Euroqol { get; set; }
    public BbPappPatientHad? Had { get; set; }
    public BbPappPatientHaq? Haq { get; set; }
    public BbPappPatientPgaScore? PgaScore { get; set; }
    public BbPappPatientSapasi? Sapasi { get; set; }
}
