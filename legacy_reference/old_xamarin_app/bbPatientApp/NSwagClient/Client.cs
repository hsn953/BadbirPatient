using Newtonsoft.Json;

namespace bbPatientAPI
{
    public partial class Client
    {
        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            // Force all date values to be serialized as yyyy-MM-dd
            settings.DateFormatString = "yyyy-MM-dd";
        }
    }
}
