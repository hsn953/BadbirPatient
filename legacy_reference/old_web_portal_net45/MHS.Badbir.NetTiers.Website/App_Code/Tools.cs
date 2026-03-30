using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Badbir.App_Code.nsTools
{
    public enum NotificationType { primary, secondary, success, danger, warning, info, light, dark} // bootstrap alerts

    public class Tools
    {
        /*
         * could be re-written to check the config table in the Badbir database
        public bool IsValueInConfig(string sValue, string ConfigName)
        {
            // get the list of values for the config setting
            ApplicationsConfigValue myConfigValue = MHS.Applications.NetTiers.Data.DataRepository.Provider.ApplicationsConfigValueProvider.GetByConfigName(ConfigName);
            string sValues = myConfigValue.ConfigValue.ToLower();

            // wrap it in \r\n
            sValues = "\r\n" + sValues + "\r\n";

            // wrap it in \r\n
            sValue = "\r\n" + sValue + "\r\n";

            if (sValues.Contains(sValue))
            {
                return true;
            }

            return false;
        }
        */

        public string stripHTMLandWhitespace(string sIn)
        {
            string patternTag = @"<[^>]+>";
            string patternWhitespace = @"\s";
            Regex myRegexTag = new Regex(patternTag);
            Regex myRegexWhitespace = new Regex(patternWhitespace);

            string sOut = sIn;
            sOut = myRegexTag.Replace(sOut, string.Empty);
            sOut = sOut.Replace("&nbsp;",string.Empty);
            sOut = myRegexWhitespace.Replace(sOut, string.Empty);

            return sOut;
        }

        public string GetDatabaseName()
        {
            // get the name of the database that is being used
            string sConn = WebConfigurationManager.ConnectionStrings["badbirnetConnectionString"].ConnectionString;
            int iStartOfDatabaseName = sConn.IndexOf("Initial Catalog=BADBIR_");
            string sDatabase = sConn.Substring(iStartOfDatabaseName + 23);
            int iEndOfDatabaseName = sDatabase.IndexOf(";");
            sDatabase = sDatabase.Substring(0, iEndOfDatabaseName);

            if(string.IsNullOrEmpty(sDatabase)) throw new Exception("Unknow database" + sConn);

            return sDatabase;
        }
    }
}
