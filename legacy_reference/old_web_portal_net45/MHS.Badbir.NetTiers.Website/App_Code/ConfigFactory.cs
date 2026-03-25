using System;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using Badbir.App_Code.nsTools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHS.Badbir.NetTiers
{
    /// <summary>
    /// Summary description for MailListManager
    /// Manage text strings in constants. will load from database too
    /// 
    /// </summary>


    /*
     *  how to use?
     * MHS.Badbir.NetTiers.ConfigFactory.buildText("account_ErrNoUser", new string[]{LoginControl.UserName});
     * 
     * * 
     **/


    public static class ConfigFactory
    {
        public static bool isInitalised = false;
        public static bool isLoading = false;
        public static string splitter = "[@]";

        public static Dictionary<String, String> textDictionary;
        public static Dictionary<String, Int32> intDictionary;
        public static Dictionary<String, Double> floatDictionary;

        public static void initConfigFactory()
        {

            //set loading to true so other methdos are made to wait for loading to finish?
            isLoading = true;

            //renew objects, old objects will get GC'ed
            textDictionary = new Dictionary<string, string>();
            intDictionary = new Dictionary<string, int>();
            floatDictionary = new Dictionary<string, double>();

            // Reworked foreach loops on 21/05/2020 to try-catch within the loop, so we can capture which key is causing an issue

            // --Text--
            foreach (MHS.Badbir.NetTiers.Entities.BbConfigFactory thisConfig in MHS.Badbir.NetTiers.Data.DataRepository.BbConfigFactoryProvider.GetByTypeIdInuse(1, true))
            {
                try
                {
                    textDictionary.Add(thisConfig.Configname, thisConfig.TextVal);
                }
                catch (Exception ex)
                {
                    // Error adding this config - send email
                    string errorDetails = "Config name: " + (thisConfig.Configname ?? "[Config name is null]") + MailListManager.MailListManager.CRLF + "Value: " + (thisConfig.TextVal ?? "[Config value is null]");
                    string errormsg;
                    MailListManager.MailListManager.sendEmailToList(MailListManager.MailListManager.ML_AdminErrors, "URGENT: Config Factory not loaded ", "Error loading Text dictionary. Config details: " +
                        errorDetails + MailListManager.MailListManager.CRLF + "Exception details: " + ex.Message, out errormsg);
                }
            }

            // --Int--
            foreach (MHS.Badbir.NetTiers.Entities.BbConfigFactory thisConfig in MHS.Badbir.NetTiers.Data.DataRepository.BbConfigFactoryProvider.GetByTypeIdInuse(2, true))
            {
                try
                {
                    intDictionary.Add(thisConfig.Configname, thisConfig.IntVal.Value);
                }
                catch (Exception ex)
                {
                    // Error adding this config - send email
                    string errorDetails = "Config name: " + (thisConfig.Configname ?? "[Config name is null]") + MailListManager.MailListManager.CRLF + "Value: " + (thisConfig.IntVal.HasValue ? thisConfig.IntVal.Value.ToString() : "[Config value is null]");
                    string errormsg;
                    MailListManager.MailListManager.sendEmailToList(MailListManager.MailListManager.ML_AdminErrors, "URGENT: Config Factory not loaded ", "Error loading Int dictionary. Config details: " +
                        errorDetails + MailListManager.MailListManager.CRLF + "Exception details: " + ex.Message, out errormsg);
                }
            }

            // --Float--
            foreach (MHS.Badbir.NetTiers.Entities.BbConfigFactory thisConfig in MHS.Badbir.NetTiers.Data.DataRepository.BbConfigFactoryProvider.GetByTypeIdInuse(3, true))
            {
                try
                {
                    floatDictionary.Add(thisConfig.Configname, thisConfig.FloatVal.Value);
                }
                catch (Exception ex)
                {
                    // Error adding this config - send email
                    string errorDetails = "Config name: " + (thisConfig.Configname ?? "[Config name is null]") + MailListManager.MailListManager.CRLF + "Value: " + (thisConfig.FloatVal.HasValue ? thisConfig.FloatVal.Value.ToString() : "[Config value is null]");
                    string errormsg;
                    MailListManager.MailListManager.sendEmailToList(MailListManager.MailListManager.ML_AdminErrors, "URGENT: Config Factory not loaded ", "Error loading Float dictionary. Config details: " + 
                        errorDetails + MailListManager.MailListManager.CRLF + "Exception details: " + ex.Message, out errormsg);
                }
            }

            // Update flag - config factory is now initialised
            isLoading = false;//loading completed this lock is lifted now
            isInitalised = true;            
        }

        public static String getText(string textName)
        {
            // Initialise, if not already
            if (!isInitalised)
            {
                //for five seconds, wait for loading to complete
                for( int i = 0; i < 5; ++i)
                    if(isLoading)//Check every second if loading is complete
                        Task.Delay(1000);


                //if its not loading and not initialised (loading failed somewhere), then call the loading function. else return 
                if (!isLoading  && !isInitalised)
                    initConfigFactory();
                else
                    return "String Loading Timed Out";
            }

            // Try getting the text from the Dictionary - but catch error if not
            try
            {
                return textDictionary[textName];
            }
            catch (KeyNotFoundException ex)
            {
                return "Config text key not found.";
            }
        }

        public static String buildText(string textName, String[] fillers)
        {
            // Initialise, if not already
            if (!isInitalised)
            {
                //for five seconds, wait for loading to complete
                for (int i = 0; i < 5; ++i)
                    if (isLoading)//Check every second if loading is complete
                        Task.Delay(1000);


                //if its not loading and not initialised (loading failed somewhere), then call the loading function. else return 
                if (!isLoading && !isInitalised)
                    initConfigFactory();
                else
                    return "String Loading Timed Out";
            }

            try
            {
                String[] splits = textDictionary[textName].Split(new string[] { splitter }, StringSplitOptions.None);

            if (splits.Length - 1 > fillers.Length)
            {
                String emailError;
                    //notify admin about an error
                MailListManager.MailListManager.sendEmailToList(MailListManager.MailListManager.ML_AdminErrors, "Config Factory Error", "error in buildText finding " + textName + " with " + fillers.Length.ToString() + " fillers.", out emailError);
                return "[ConfigFactoryError: Not enough fillers to build string]";
            }
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append(splits[i]);
            for (++i; i < splits.Length; ++i)
                sb.Append(fillers[i - 1]).Append(splits[i]);


            return sb.ToString();
            }
            catch (KeyNotFoundException ex)
            {
                // Updated 21/05/2020 to show the textName
                return "Config text key '" + textName + "' not found. Error details: " + ex.Message;
            }

        }


        public static Int32 getInt(string intName)
        {
            // Initialise, if not already
            if (!isInitalised)
            {
                //for five seconds, wait for loading to complete
                for (int i = 0; i < 5; ++i)
                    if (isLoading)//Check every second if loading is complete
                        Task.Delay(1000);


                //if its not loading and not initialised (loading failed somewhere), then call the loading function. else return 
                if (!isLoading && !isInitalised)
                    initConfigFactory();
                else
                    return 0; //? should this be an exception instead?
            }

            return intDictionary[intName];

        }

        public static Double getFloat(string floatName)
        {
            // Initialise, if not already
            if (!isInitalised)
            {
                //for five seconds, wait for loading to complete
                for (int i = 0; i < 5; ++i)
                    if (isLoading)//Check every second if loading is complete
                        Task.Delay(1000);


                //if its not loading and not initialised (loading failed somewhere), then call the loading function. else return 
                if (!isLoading && !isInitalised)
                    initConfigFactory();
                else
                    return 0;//should this be an exception instead? 
            }

            return floatDictionary[floatName];

        }

        //Checking for loading can be made into a generic function that returns a bool to indicate the the getfunction 


    }
}