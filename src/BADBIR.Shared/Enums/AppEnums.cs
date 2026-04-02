namespace BADBIR.Shared.Enums;

/// <summary>
/// Identifies the type of patient-reported outcome (PRO) form.
/// </summary>
public enum FormType
{
    Lifestyle,
    Pga,
    Cage,
    Dlqi,
    EuroQol,
    Hads,
    Haq,
    Sapasi
}

/// <summary>
/// General application roles used for authorization.
/// </summary>
public enum UserRole
{
    Patient,
    Clinician,
    Administrator
}

/// <summary>
/// Patient account registration / verification status.
/// </summary>
public enum RegistrationStatus : byte
{
    /// <summary>Account verified and active (identity matched OR clinician-confirmed).</summary>
    Active = 0,

    /// <summary>
    /// Account created in holding state — identity not yet confirmed by a clinician.
    /// Data will be permanently deleted after HoldingExpiry if not confirmed.
    /// </summary>
    Holding = 1,

    /// <summary>Invitation-based account, active immediately (no holding period).</summary>
    InviteActive = 2
}

/// <summary>
/// Patient's self-reported inflammatory arthritis diagnosis.
/// Determines whether the HAQ form is included in their sequence.
/// </summary>
public enum SelfReportedDiagnosis : byte
{
    /// <summary>Patient confirmed they have a rheumatologist diagnosis of IA/PsA.</summary>
    Yes = 0,

    /// <summary>Patient confirmed they do NOT have an IA/PsA diagnosis.</summary>
    No = 1,

    /// <summary>
    /// Patient is unsure — HAQ is included at baseline (inclusive default);
    /// clinician overrides after promotion.
    /// </summary>
    NotSure = 2
}

/// <summary>
/// Type of informed consent signature provided.
/// </summary>
public enum SignatureType : byte
{
    /// <summary>Typed name + checkbox confirmation.</summary>
    Electronic = 0,

    /// <summary>Hand-drawn signature captured on canvas.</summary>
    Drawn = 1
}

/// <summary>
/// Completion status of a single form within a follow-up period.
/// </summary>
public enum FormStatusEnum : byte
{
    NotStarted = 0,
    Completed  = 1,
    Skipped    = 2
}
