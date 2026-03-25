namespace BADBIR.Shared.DTOs;

/// <summary>Patient profile summary DTO (safe for API responses).</summary>
public class PatientDto
{
    public int PatientId { get; set; }
    public string NhsNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Ethnicity { get; set; }
}

/// <summary>Diagnosis entry DTO.</summary>
public class PatientDiagnosisDto
{
    public int DiagnosisId { get; set; }
    public string DiagnosisCode { get; set; } = string.Empty;
    public string DiagnosisName { get; set; } = string.Empty;
    public DateOnly? DiagnosedDate { get; set; }
    public bool IsActive { get; set; }
}

// ── EuroQol ──────────────────────────────────────────────────────────────────

/// <summary>Payload for submitting an EQ-5D-5L form.</summary>
public class EuroQolSubmitDto
{
    /// <summary>1=No problems … 5=Extreme problems</summary>
    public byte Mobility { get; set; }
    public byte SelfCare { get; set; }
    public byte UsualActivities { get; set; }
    public byte PainDiscomfort { get; set; }
    public byte AnxietyDepression { get; set; }
    /// <summary>0–100 VAS</summary>
    public byte VasScore { get; set; }
    public string? Notes { get; set; }
}

/// <summary>EuroQol submission read DTO.</summary>
public class EuroQolSubmissionDto : EuroQolSubmitDto
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public decimal? IndexValue { get; set; }
}

// ── HAQ ──────────────────────────────────────────────────────────────────────

/// <summary>Payload for submitting an HAQ-DI form.</summary>
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

/// <summary>HAQ submission read DTO.</summary>
public class HaqSubmissionDto : HaqSubmitDto
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public decimal HaqDiScore { get; set; }
}

// ── HADS ─────────────────────────────────────────────────────────────────────

/// <summary>Payload for submitting a HADS form (14 items, each 0–3).</summary>
public class HadsSubmitDto
{
    // Anxiety items
    public byte A1 { get; set; }
    public byte A2 { get; set; }
    public byte A3 { get; set; }
    public byte A4 { get; set; }
    public byte A5 { get; set; }
    public byte A6 { get; set; }
    public byte A7 { get; set; }

    // Depression items
    public byte D1 { get; set; }
    public byte D2 { get; set; }
    public byte D3 { get; set; }
    public byte D4 { get; set; }
    public byte D5 { get; set; }
    public byte D6 { get; set; }
    public byte D7 { get; set; }

    public string? Notes { get; set; }
}

/// <summary>HADS submission read DTO (includes computed scores).</summary>
public class HadsSubmissionDto : HadsSubmitDto
{
    public int SubmissionId { get; set; }
    public int PatientId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public byte AnxietyScore { get; set; }
    public byte DepressionScore { get; set; }
}
