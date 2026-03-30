namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientCohortHistory].
/// Each row represents a patient's enrolment into a BADBIR cohort.
/// PK is <see cref="Chid"/> which is used throughout the system as the main
/// patient identifier (one patient can have multiple cohort history records if
/// they switch cohorts).
/// This table is owned by the Clinician System.
/// </summary>
public class BbPatientCohortHistory
{
    /// <summary>Cohort history ID — the primary identifier used throughout BADBIR forms.</summary>
    public int Chid { get; set; }

    /// <summary>FK to bbPatient.patientid.</summary>
    public int Patientid { get; set; }

    public int? Studyno { get; set; }

    /// <summary>FK to bbCohortlkp.cohortid. 1=Biologic, 2=Conventional, 3=Small Molecule.</summary>
    public int Cohortid { get; set; }

    public DateTime? Datefrom { get; set; }
    public DateTime? Dateto { get; set; }
    public int? DatetoFUP { get; set; }

    /// <summary>FK to bbCentre.centreid.</summary>
    public int? Regcentreid { get; set; }

    public string Consentversion { get; set; } = "1";

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public BbPatient? Patient { get; set; }
    public ICollection<BbPatientCohortTracking> CohortTrackings { get; set; } = [];
}
