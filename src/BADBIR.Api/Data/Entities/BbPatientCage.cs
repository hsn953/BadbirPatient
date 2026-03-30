namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatientCage].
/// CAGE alcohol screening questionnaire (4 Yes/No items).
/// <see cref="QSourceId"/> = 2 when submitted via the Patient Portal.
/// </summary>
public class BbPatientCage
{
    public int CageID { get; set; }

    /// <summary>FK to bbPatientCohortTracking.FupId.</summary>
    public int FupId { get; set; }

    public bool? Cutdown { get; set; }
    public bool? Annoyed { get; set; }
    public bool? Guilty { get; set; }
    public bool? Earlymorning { get; set; }

    public DateTime? Datecomp { get; set; }

    /// <summary>FK to bbPatientQuestionnaireSourcelkp. 1=Clinician, 2=Patient (via Portal), 3=Patient (via Clinician).</summary>
    public int? QSourceId { get; set; }

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
