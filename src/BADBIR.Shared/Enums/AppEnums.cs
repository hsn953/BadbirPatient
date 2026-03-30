namespace BADBIR.Shared.Enums;

/// <summary>
/// Identifies the type of patient-reported outcome (PRO) form.
/// </summary>
public enum FormType
{
    EuroQol,
    HAQ,
    HADS
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
