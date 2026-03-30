using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace bbPatientAPI
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class SwaggerResponse
    {
        public int StatusCode { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public System.Collections.Generic.Dictionary<string, string> BodyProperties { get;  }

        public Newtonsoft.Json.Linq.JObject BodyJObject { get; }

        public SwaggerResponse(int statusCode, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Net.Http.HttpResponseMessage response_)
        {
            StatusCode = statusCode;
            Headers = headers;

            
            try
            {
                //process json from response and save it in body in a more accessible way
                var responseText = response_.Content.ReadAsStringAsync().Result;

                //Create a newtonsoft object. 
                BodyJObject = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseText);
                BodyProperties = new Dictionary<string, string>();

                // Loop through each property in the parsed object
                foreach (JProperty property in BodyJObject.Properties())
                {
                    // Add the property name and value to the dictionary
                    BodyProperties.Add(property.Name, property.Value.ToString());
                }
                
            }
            catch (Newtonsoft.Json.JsonException exception)
            {
                var message = "Could not deserialize the response body string.";
                throw new ApiException(message, (int)response_.StatusCode, "no response text", headers, exception);
            }

        }
    }

}
