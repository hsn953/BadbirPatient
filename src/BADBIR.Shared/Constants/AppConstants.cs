namespace BADBIR.Shared.Constants;

/// <summary>
/// Application-wide constants shared across all projects.
/// </summary>
public static class AppConstants
{
    public const string AppName = "BADBIR Patient Application";
    public const string ApiVersion = "v1";

    /// <summary>Current consent form version. Bump this to trigger re-consent.</summary>
    public const string ConsentFormVersion = "1.0";

    /// <summary>Number of days before a holding account is permanently deleted.</summary>
    public const int HoldingAccountExpiryDays = 14;

    public static class ApiRoutes
    {
        public const string Auth          = "/api/auth";
        public const string Patients      = "/api/patients";
        public const string Forms         = "/api/forms";
        public const string Consent       = "/api/consent";
    }

    public static class FormTypes
    {
        public const string Lifestyle = "Lifestyle";
        public const string Pga       = "PGA";
        public const string Cage      = "CAGE";
        public const string Dlqi      = "DLQI";
        public const string EuroQol   = "EuroQol";
        public const string Hads      = "HADS";
        public const string Haq       = "HAQ";
        public const string Sapasi    = "SAPASI";
    }

    /// <summary>Clinical centre names used during registration.</summary>
    public static readonly IReadOnlyList<string> ClinicalCentres =
    [
        "Aberdeen Royal Infirmary",
        "Addenbrooke's Hospital (Cambridge)",
        "Bristol Royal Infirmary",
        "Chelsea and Westminster Hospital",
        "Dundee Ninewells Hospital",
        "Edinburgh Royal Infirmary",
        "Glasgow Queen Elizabeth University Hospital",
        "Guy's and St Thomas' NHS Foundation Trust",
        "King's College Hospital",
        "Leeds Teaching Hospitals",
        "Leicester Royal Infirmary",
        "Manchester Royal Infirmary",
        "Newcastle Royal Victoria Infirmary",
        "Ninewells Hospital Dundee",
        "Queen's Medical Centre Nottingham",
        "Royal Free Hospital London",
        "Royal London Hospital",
        "Royal Victoria Hospital Belfast",
        "Sheffield Teaching Hospitals",
        "Southampton General Hospital",
        "St George's Hospital London",
        "University Hospital Birmingham",
        "University Hospital of Wales Cardiff",
        "Other"
    ];
}
