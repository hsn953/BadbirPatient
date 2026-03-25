namespace BADBIR.Shared.Constants;

/// <summary>
/// Application-wide constants shared across all projects.
/// </summary>
public static class AppConstants
{
    public const string AppName = "BADBIR Patient Application";
    public const string ApiVersion = "v1";

    public static class ApiRoutes
    {
        public const string Auth = "/api/auth";
        public const string Patients = "/api/patients";
        public const string Forms = "/api/forms";
    }

    public static class FormTypes
    {
        public const string EuroQol = "EuroQol";
        public const string HAQ = "HAQ";
        public const string HADS = "HADS";
    }
}
