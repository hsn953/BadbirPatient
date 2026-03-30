using System.ComponentModel.DataAnnotations;

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

    /// <summary>Itchy/sore/painful — 0=Not at all, 1=A little, 2=A lot, 3=Very much.</summary>
    [Range(0, 3)] public int? ItchsoreScore { get; set; }

    /// <summary>Embarrassed/self-conscious — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? EmbscScore { get; set; }

    /// <summary>Shopping/home — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? ShophgScore { get; set; }

    /// <summary>Clothes — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? ClothesScore { get; set; }

    /// <summary>Social/leisure — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? SocleisScore { get; set; }

    /// <summary>Sport — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? SportScore { get; set; }

    /// <summary>Work/study prevented — 0=No (sub-question follows), 1=Yes (prevented).</summary>
    [Range(0, 1)] public int? WorkstudScore { get; set; }

    /// <summary>Work/study problem (sub-question, shown when <see cref="WorkstudScore"/> = 0 / "No") — 0=Not at all, 1=A little, 2=A lot.</summary>
    [Range(0, 2)] public int? WorkstudnoScore { get; set; }

    /// <summary>Partner/family problems — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? PartcrfScore { get; set; }

    /// <summary>Sexual difficulties — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? SexdifScore { get; set; }

    /// <summary>Treatment problem — 0=Not at all … 3=Very much.</summary>
    [Range(0, 3)] public int? TreatmentScore { get; set; }

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips Q7 (work/study question N/A).
    /// </summary>
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

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips or declines this section.
    /// All four question fields will be stored as null.
    /// </summary>
    public bool SkipForm { get; set; }
}

public class PappCageDto : PappCageSubmitDto
{
    public int PappCageId { get; set; }
    public int PappFupId { get; set; }
}

public class PappEuroqolSubmitDto
{
    /// <summary>Mobility — 1=No problems, 2=Some problems, 3=Extreme problems.</summary>
    [Range(1, 3)] public int? Mobility { get; set; }

    /// <summary>Self-care — 1=No problems, 2=Some problems, 3=Extreme problems.</summary>
    [Range(1, 3)] public int? Selfcare { get; set; }

    /// <summary>Usual activities — 1=No problems, 2=Some problems, 3=Extreme problems.</summary>
    [Range(1, 3)] public int? Usualacts { get; set; }

    /// <summary>Pain/Discomfort — 1=No pain, 2=Moderate, 3=Extreme.</summary>
    [Range(1, 3)] public int? Paindisc { get; set; }

    /// <summary>Anxiety/Depression — 1=Not, 2=Moderately, 3=Extremely.</summary>
    [Range(1, 3)] public int? Anxdepr { get; set; }

    /// <summary>Comparative health vs past 12 months — 1=Better, 2=Same, 3=Worse.</summary>
    [Range(1, 3)] public int? Comphealth { get; set; }

    /// <summary>VAS "How you feel today" (0–100).</summary>
    [Range(0, 100)] public int? Howyoufeel { get; set; }

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips or declines this form.
    /// All scored fields will be stored as null.
    /// </summary>
    public bool SkipForm { get; set; }
}

public class PappEuroqolDto : PappEuroqolSubmitDto
{
    public int PappEuroqolId { get; set; }
    public int PappFupId { get; set; }
}

public class PappHadSubmitDto
{
    // ── Anxiety items (odd questions) — each 0–3 ────────────────────────────
    [Range(0, 3)] public int? Q01tense { get; set; }
    [Range(0, 3)] public int? Q03frightened { get; set; }
    [Range(0, 3)] public int? Q05worry { get; set; }
    [Range(0, 3)] public int? Q07relaxed { get; set; }
    [Range(0, 3)] public int? Q09butterflies { get; set; }
    [Range(0, 3)] public int? Q11restless { get; set; }
    [Range(0, 3)] public int? Q13panic { get; set; }

    // ── Depression items (even questions) — each 0–3 ────────────────────────
    [Range(0, 3)] public int? Q02enjoy { get; set; }
    [Range(0, 3)] public int? Q04laugh { get; set; }
    [Range(0, 3)] public int? Q06cheerful { get; set; }
    [Range(0, 3)] public int? Q08slowed { get; set; }
    [Range(0, 3)] public int? Q10appearence { get; set; }
    [Range(0, 3)] public int? Q12enjoyment { get; set; }
    [Range(0, 3)] public int? Q14goodbook { get; set; }

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips or declines this form.
    /// All question fields will be stored as null.
    /// </summary>
    public bool SkipForm { get; set; }
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
    /// <summary>Set to <c>true</c> if clinical data is missing — requires <see cref="Missingdatadetails"/>.</summary>
    public bool? Missingdata { get; set; }
    public string? Missingdatadetails { get; set; }

    // Category 1: Dressing & Grooming — each 0=Without difficulty … 3=Unable
    [Range(0, 3)] public int? Dressself { get; set; }
    [Range(0, 3)] public int? Shampoo { get; set; }

    // ── Category 2: Arising ──────────────────────────────────────────────────
    [Range(0, 3)] public int? Standchair { get; set; }
    [Range(0, 3)] public int? Bed { get; set; }

    // ── Category 3: Eating ───────────────────────────────────────────────────
    [Range(0, 3)] public int? Cutmeat { get; set; }
    [Range(0, 3)] public int? Liftglass { get; set; }
    [Range(0, 3)] public int? Openmilk { get; set; }

    // ── Category 4: Walking ──────────────────────────────────────────────────
    [Range(0, 3)] public int? Walkflat { get; set; }
    [Range(0, 3)] public int? Climbsteps { get; set; }

    // ── Category 5: Hygiene ──────────────────────────────────────────────────
    [Range(0, 3)] public int? Washdry { get; set; }
    [Range(0, 3)] public int? Bath { get; set; }
    [Range(0, 3)] public int? Toilet { get; set; }

    // ── Category 6: Reach ────────────────────────────────────────────────────
    [Range(0, 3)] public int? Reachabove { get; set; }
    [Range(0, 3)] public int? Bend { get; set; }

    // ── Category 7: Grip ─────────────────────────────────────────────────────
    [Range(0, 3)] public int? Cardoor { get; set; }
    [Range(0, 3)] public int? Openjar { get; set; }
    [Range(0, 3)] public int? Turntap { get; set; }

    // ── Category 8: Activities ───────────────────────────────────────────────
    [Range(0, 3)] public int? Shop { get; set; }
    [Range(0, 3)] public int? Getincar { get; set; }
    [Range(0, 3)] public int? Housework { get; set; }

    // Aids & Devices (0=None, 1=Used)
    [Range(0, 1)] public int? Cane { get; set; }
    [Range(0, 1)] public int? Crutches { get; set; }
    [Range(0, 1)] public int? Walker { get; set; }
    [Range(0, 1)] public int? Wheelchair { get; set; }
    [Range(0, 1)] public int? Specialutensils { get; set; }
    [Range(0, 1)] public int? Specialchair { get; set; }
    [Range(0, 1)] public int? Dressing { get; set; }
    public string? Dressingdetails { get; set; }
    [Range(0, 1)] public int? Loolift { get; set; }
    [Range(0, 1)] public int? Bathseat { get; set; }
    [Range(0, 1)] public int? Bathrail { get; set; }
    [Range(0, 1)] public int? Longreach { get; set; }
    [Range(0, 1)] public int? Jaropener { get; set; }
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
    /// <summary>Patient-reported global assessment — 1=Clear, 2=Almost clear, 3=Mild, 4=Moderate, 5=Severe.</summary>
    [Range(1, 5)] public int? Pgascore { get; set; }

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips or declines this form.
    /// </summary>
    public bool SkipForm { get; set; }
}

public class PappPgaDto : PappPgaSubmitDto
{
    public int PappFupId { get; set; }
    public DateTime? DateScored { get; set; }
}

// ── SAPASI ────────────────────────────────────────────────────────────────────

/// <summary>
/// SAPASI (Self-Administered PASI) submission DTO.
///
/// Each body region requires:
/// <list type="bullet">
///   <item>Coverage band: 0=None, 1=&lt;10%, 2=10–30%, 3=30–50%, 4=&gt;50%.</item>
///   <item>Erythema (redness): 0=None … 4=Very marked.</item>
///   <item>Induration (thickness): 0=None … 4=Very marked.</item>
///   <item>Desquamation (scaliness): 0=None … 4=Very marked.</item>
/// </list>
///
/// All fields are nullable — patients may leave any region blank.
/// </summary>
public class PappSapasiSubmitDto
{
    // ── Head (weight = 0.1) ──────────────────────────────────────────────────
    /// <summary>Coverage band 0–4.</summary>
    [Range(0, 4)] public int? HeadCoverage { get; set; }
    [Range(0, 4)] public int? HeadErythema { get; set; }
    [Range(0, 4)] public int? HeadInduration { get; set; }
    [Range(0, 4)] public int? HeadDesquamation { get; set; }

    // ── Trunk (weight = 0.3) ─────────────────────────────────────────────────
    [Range(0, 4)] public int? TrunkCoverage { get; set; }
    [Range(0, 4)] public int? TrunkErythema { get; set; }
    [Range(0, 4)] public int? TrunkInduration { get; set; }
    [Range(0, 4)] public int? TrunkDesquamation { get; set; }

    // ── Upper Limbs (weight = 0.2) ───────────────────────────────────────────
    [Range(0, 4)] public int? UpperLimbsCoverage { get; set; }
    [Range(0, 4)] public int? UpperLimbsErythema { get; set; }
    [Range(0, 4)] public int? UpperLimbsInduration { get; set; }
    [Range(0, 4)] public int? UpperLimbsDesquamation { get; set; }

    // ── Lower Limbs (weight = 0.4) ───────────────────────────────────────────
    [Range(0, 4)] public int? LowerLimbsCoverage { get; set; }
    [Range(0, 4)] public int? LowerLimbsErythema { get; set; }
    [Range(0, 4)] public int? LowerLimbsInduration { get; set; }
    [Range(0, 4)] public int? LowerLimbsDesquamation { get; set; }

    /// <summary>
    /// Set to <c>true</c> if the patient explicitly skips or declines this form.
    /// </summary>
    public bool SkipForm { get; set; }
}

/// <summary>SAPASI read DTO — includes the server-calculated total score.</summary>
public class PappSapasiDto : PappSapasiSubmitDto
{
    public int PappSapasiId { get; set; }
    public int PappFupId { get; set; }

    /// <summary>
    /// SAPASI total score = Σ(Severity × Coverage × RegionWeight) across 4 regions.
    /// Range: 0–48 for coverage bands 0–4 and severity items 0–4.
    /// </summary>
    public float? SapasiScore { get; set; }

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

