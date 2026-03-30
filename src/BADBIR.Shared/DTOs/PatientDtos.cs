namespace BADBIR.Shared.DTOs;

// ── Patient ───────────────────────────────────────────────────────────────────

/// <summary>Patient profile summary DTO — PII fields are already decrypted.</summary>
public class PatientDto
{
    public int Patientid { get; set; }

    /// <summary>CHI / Health &amp; Care number (Scotland) — decrypted.</summary>
    public string? Phrn { get; set; }

    /// <summary>NHS number (England/Wales) — decrypted.</summary>
    public string? Pnhs { get; set; }

    public string? Title { get; set; }
    public string? Forenames { get; set; }
    public string? Surname { get; set; }
    public DateTime? Dateofbirth { get; set; }
    public int? Genderid { get; set; }

    /// <summary>1=Current, 6=Registered awaiting consent, 7=Awaiting drug details.</summary>
    public int? Statusid { get; set; }
}

/// <summary>Cohort history record DTO.</summary>
public class CohortHistoryDto
{
    public int Chid { get; set; }
    public int Patientid { get; set; }
    public int Cohortid { get; set; }
    public int? Studyno { get; set; }
    public DateTime? Datefrom { get; set; }
}

// ── Admin / Promotion ─────────────────────────────────────────────────────────

/// <summary>Request body for POST /api/admin/patients/{patientId}/promote.</summary>
public class PromoteRequestDto
{
    /// <summary>
    /// bbPatientCohortTracking.FupId already created by the Clinician System.
    /// All papp form data will be written to the live tables using this FupId.
    /// </summary>
    public int FupId { get; set; }

    /// <summary>Clinician's BADBIR user ID (for audit fields).</summary>
    public int ClinicianId { get; set; }

    /// <summary>Clinician's display name (for audit fields).</summary>
    public string ClinicianName { get; set; } = string.Empty;
}

/// <summary>Response body for POST /api/admin/patients/{patientId}/promote.</summary>
public class PromoteResultDto
{
    public int PatientId { get; set; }
    public int FupId { get; set; }
    public List<string> PromotedForms { get; set; } = [];
    public DateTime PromotedAt { get; set; }
}

/// <summary>Request body for POST /api/admin/patients/{patientId}/reject.</summary>
public class RejectRequestDto
{
    public int ClinicianId { get; set; }
    public string ClinicianName { get; set; } = string.Empty;
    public string? Reason { get; set; }
}

// ── Papp form submission DTOs ─────────────────────────────────────────────────

public class PappDlqiSubmitDto
{
    public string? Diagnosis { get; set; }
    public int? ItchsoreScore { get; set; }
    public int? EmbscScore { get; set; }
    public int? ShophgScore { get; set; }
    public int? ClothesScore { get; set; }
    public int? SocleisScore { get; set; }
    public int? SportScore { get; set; }
    public int? WorkstudScore { get; set; }
    public int? WorkstudnoScore { get; set; }
    public int? PartcrfScore { get; set; }
    public int? SexdifScore { get; set; }
    public int? TreatmentScore { get; set; }
    public bool SkipBreakup { get; set; }
}

public class PappDlqiDto : PappDlqiSubmitDto
{
    public int PappDlqiId { get; set; }
    public int PappFupId { get; set; }
    public int? TotalScore { get; set; }
    public DateTime? Datecomp { get; set; }
}

public class PappLifestyleSubmitDto
{
    public string? Birthtown { get; set; }
    public string? Birthcountry { get; set; }
    public int? Workstatusid { get; set; }
    public string? Occupation { get; set; }
    public int? Ethnicityid { get; set; }
    public string? Otherethnicity { get; set; }
    public bool? Outdooroccupation { get; set; }
    public bool? Livetropical { get; set; }
    public bool? Eversmoked { get; set; }
    public int? Eversmokednumbercigsperday { get; set; }
    public int? Agestart { get; set; }
    public int? Agestop { get; set; }
    public bool? Currentlysmoke { get; set; }
    public int? Currentlysmokenumbercigsperday { get; set; }
    public int? Drnkbeeravg { get; set; }
    public int? Drnkwineavg { get; set; }
    public int? Drnkspiritsavg { get; set; }
    public bool? Drinkalcohol { get; set; }
    public int? Drnkunitsavg { get; set; }
    public int? Admittedtohospital { get; set; }
    public int? Newdrugs { get; set; }
    public int? Newclinics { get; set; }
    public float? Systolic { get; set; }
    public float? Diastolic { get; set; }
    public float? Height { get; set; }
    public float? Weight { get; set; }
    public float? Waist { get; set; }
    public bool WeightMissing { get; set; }
    public bool WaistMissing { get; set; }
    public bool SmokingMissing { get; set; }
    public bool DrinkingMissing { get; set; }
}

public class PappLifestyleDto : PappLifestyleSubmitDto
{
    public int PappLifestyleId { get; set; }
    public int PappFupId { get; set; }
}

public class PappCageSubmitDto
{
    public bool? Cutdown { get; set; }
    public bool? Annoyed { get; set; }
    public bool? Guilty { get; set; }
    public bool? Earlymorning { get; set; }
}

public class PappCageDto : PappCageSubmitDto
{
    public int PappCageId { get; set; }
    public int PappFupId { get; set; }
}

public class PappEuroqolSubmitDto
{
    /// <summary>1=No problems, 2=Some problems, 3=Extreme problems</summary>
    public int? Mobility { get; set; }
    public int? Selfcare { get; set; }
    public int? Usualacts { get; set; }
    public int? Paindisc { get; set; }
    public int? Anxdepr { get; set; }
    /// <summary>Composite health index (derived from profile).</summary>
    public int? Comphealth { get; set; }
    /// <summary>VAS 0–100.</summary>
    public int? Howyoufeel { get; set; }
}

public class PappEuroqolDto : PappEuroqolSubmitDto
{
    public int PappEuroqolId { get; set; }
    public int PappFupId { get; set; }
}

public class PappHadSubmitDto
{
    // Anxiety items (odd questions)
    public int? Q01tense { get; set; }
    public int? Q03frightened { get; set; }
    public int? Q05worry { get; set; }
    public int? Q07relaxed { get; set; }
    public int? Q09butterflies { get; set; }
    public int? Q11restless { get; set; }
    public int? Q13panic { get; set; }
    // Depression items (even questions)
    public int? Q02enjoy { get; set; }
    public int? Q04laugh { get; set; }
    public int? Q06cheerful { get; set; }
    public int? Q08slowed { get; set; }
    public int? Q10appearence { get; set; }
    public int? Q12enjoyment { get; set; }
    public int? Q14goodbook { get; set; }
}

public class PappHadDto : PappHadSubmitDto
{
    public int PappHadId { get; set; }
    public int PappFupId { get; set; }
    public int? ScoreAnxiety { get; set; }
    /// <summary>0=Normal (0–7), 1=Borderline (8–10), 2=Abnormal (11–21).</summary>
    public int? ResultAnxiety { get; set; }
    public int? ScoreDepression { get; set; }
    public int? ResultDepression { get; set; }
    /// <summary>True once all 14 items answered — triggers GL Assessments invoice count.</summary>
    public bool IsCountable { get; set; }
}

public class PappHaqSubmitDto
{
    public bool? Missingdata { get; set; }
    public string? Missingdatadetails { get; set; }
    // Category 1: Dressing & Grooming
    public int? Dressself { get; set; }
    public int? Shampoo { get; set; }
    // Category 2: Arising
    public int? Standchair { get; set; }
    public int? Bed { get; set; }
    // Category 3: Eating
    public int? Cutmeat { get; set; }
    public int? Liftglass { get; set; }
    public int? Openmilk { get; set; }
    // Category 4: Walking
    public int? Walkflat { get; set; }
    public int? Climbsteps { get; set; }
    // Category 5: Hygiene
    public int? Washdry { get; set; }
    public int? Bath { get; set; }
    public int? Toilet { get; set; }
    // Category 6: Reach
    public int? Reachabove { get; set; }
    public int? Bend { get; set; }
    // Category 7: Grip
    public int? Cardoor { get; set; }
    public int? Openjar { get; set; }
    public int? Turntap { get; set; }
    // Category 8: Activities
    public int? Shop { get; set; }
    public int? Getincar { get; set; }
    public int? Housework { get; set; }
    // Aids & Devices
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
}

public class PappHaqDto : PappHaqSubmitDto
{
    public int PappHaqId { get; set; }
    public int PappFupId { get; set; }
    // Category scores
    public int? Dressgroom { get; set; }
    public int? Rising { get; set; }
    public int? Eating { get; set; }
    public int? Walking { get; set; }
    public int? Hygiene { get; set; }
    public int? Reach { get; set; }
    public int? Gripping { get; set; }
    public int? Errands { get; set; }
    /// <summary>HAQ Disability Index (0.000–3.000).</summary>
    public double? Haqscore { get; set; }
}

public class PappPgaSubmitDto
{
    /// <summary>1=Clear, 2=Almost clear, 3=Mild, 4=Moderate, 5=Severe.</summary>
    public int? Pgascore { get; set; }
}

public class PappPgaDto : PappPgaSubmitDto
{
    public int PappFupId { get; set; }
    public DateTime? DateScored { get; set; }
}

// ── Keep legacy score calculation DTOs for existing unit tests ────────────────
// (These are used by UnitTest1.cs and must not be removed)

/// <summary>Legacy HAQ submit DTO — kept for score calculation unit tests.</summary>
public class HaqSubmitDto
{
    /// <summary>0=Without any difficulty … 3=Unable to do</summary>
    public byte Dressing { get; set; }
    public byte Arising { get; set; }
    public byte Eating { get; set; }
    public byte Walking { get; set; }
    public byte Hygiene { get; set; }
    public byte Reach { get; set; }
    public byte Grip { get; set; }
    public byte Activities { get; set; }
    public bool UsesDressingAids { get; set; }
    public bool UsesArisingAids { get; set; }
    public bool UsesEatingAids { get; set; }
    public bool UsesWalkingAids { get; set; }
    public bool UsesHygieneAids { get; set; }
    public bool UsesReachAids { get; set; }
    public bool UsesGripAids { get; set; }
    public bool UsesActivitiesAids { get; set; }
    public string? Notes { get; set; }
}

/// <summary>Legacy HADS submit DTO — kept for score calculation unit tests.</summary>
public class HadsSubmitDto
{
    public byte A1 { get; set; }
    public byte A2 { get; set; }
    public byte A3 { get; set; }
    public byte A4 { get; set; }
    public byte A5 { get; set; }
    public byte A6 { get; set; }
    public byte A7 { get; set; }
    public byte D1 { get; set; }
    public byte D2 { get; set; }
    public byte D3 { get; set; }
    public byte D4 { get; set; }
    public byte D5 { get; set; }
    public byte D6 { get; set; }
    public byte D7 { get; set; }
    public string? Notes { get; set; }
}

/// <summary>Legacy EuroQol submit DTO — kept for unit tests.</summary>
public class EuroQolSubmitDto
{
    public byte Mobility { get; set; }
    public byte SelfCare { get; set; }
    public byte UsualActivities { get; set; }
    public byte PainDiscomfort { get; set; }
    public byte AnxietyDepression { get; set; }
    public byte VasScore { get; set; }
    public string? Notes { get; set; }
}

