using System;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using Badbir.App_Code.nsTools;
using System.Collections.Generic;
using System.Linq;

namespace MHS.Badbir.NetTiers.MailListManager
{
    /// <summary>
    /// Summary description for MailListManager
    /// Manage automated emails sent from the system in specific areas. Control given to users to subscribe or unsubscribe to email lists on the go.
    /// 
    /// </summary>

    /*
     *  how to use?
     *                 string mailerrormsg;
                //send message
                if (MailListManager.sendEmailToList(MailListManager.ML_BioDrugModification, "Biologic Drug Changes", sBody, out mailerrormsg) > 0)
                   Success scenario
                else
                   Failure Scenario
     * 
     **/

    public static class MailListManager
    {
        public static SmtpClient singletonSMTPClient;

        public static short ML_AdminErrors = 1;
        public static short ML_DeathInFutureFup = 2;
        public static short ML_PatStatusChange = 3;
        public static short ML_BioDrugModification = 4;

        public static short ML_ConsentFormUpload = 10;
        public static short ML_UserDisablingSummary = 11;
        public static short ML_ESIFiled = 12;
        public static short ML_RituxGolimAlert = 13;
        public static short ML_NewUserAlert = 14;
        public static short ML_PatientDemographicsChange = 15;

        private static string fromString = MHS.Badbir.NetTiers.ConfigFactory.getText("Email_FromAddress");
        public static string CRLF = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();

        private static Dictionary<string,DateTime> recentRegEmails = new Dictionary<string, DateTime>();
        /*
      * Takes in an integer as mailing list id. Use predifined local static ints to select appropriate mailing list
      * 
      **/

        public static SmtpClient getSMTPClient(){
            if (singletonSMTPClient == null)
            {
                singletonSMTPClient = new SmtpClient();
            }

            return singletonSMTPClient;
        }

        public static int? sendEmailToList(int mailListID, string subject, string body, out string errorMessage)
        {
            // send an email to inform administrators of the new account
            
            MHS.Badbir.NetTiers.Entities.BbMailingLists thisML = MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListsProvider.GetByBbMlid(mailListID);
            StringBuilder toAddressCommaSeparated = new StringBuilder();

            
            foreach (MHS.Badbir.NetTiers.Entities.BbMailingListSubscriptions targetAddress in MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListSubscriptionsProvider.GetByBbMlid(mailListID))
            {
                toAddressCommaSeparated.Append(targetAddress.EmailAddress).Append(",");
            }

            toAddressCommaSeparated.Remove(toAddressCommaSeparated.Length - 1,1);

            int? returnval = sendEmailToAddressee(toAddressCommaSeparated.ToString(), subject, body, out errorMessage,null,thisML.MlName);
            if (returnval.Value == 1)
            {
                thisML.TotalEmailsSent++;
                thisML.LastEmailSentOn = DateTime.Now;
                MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListsProvider.Update(thisML);
            }
            return returnval;
        }

        public static int? sendEmailToAddresseeCCToList(string toAddressCommaSeparated, string subject, string body, out string errorMessage, int CCMailListID)
        {
            MHS.Badbir.NetTiers.Entities.BbMailingLists thisML = MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListsProvider.GetByBbMlid(CCMailListID);
            StringBuilder CCAddressCommaSeparated = new StringBuilder();


            foreach (MHS.Badbir.NetTiers.Entities.BbMailingListSubscriptions targetAddress in MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListSubscriptionsProvider.GetByBbMlid(CCMailListID))
            {
                CCAddressCommaSeparated.Append(targetAddress.EmailAddress).Append(",");
            }

            CCAddressCommaSeparated.Remove(CCAddressCommaSeparated.Length - 1,1);

            int? returnval = sendEmailToAddressee(toAddressCommaSeparated.ToString(), subject, body, out errorMessage, CCAddressCommaSeparated.ToString(),thisML.MlName+"(CC)");
            if (returnval.Value == 1)
            {
                thisML.TotalEmailsSent++;
                thisML.LastEmailSentOn = DateTime.Now;
                MHS.Badbir.NetTiers.Data.DataRepository.BbMailingListsProvider.Update(thisML);
            }
            return returnval;
        }

        public static int? sendEmailToAddressee(string toAddressCommaSeparated, string subject, string body, out string errorMessage)
        {
            return sendEmailToAddressee(toAddressCommaSeparated, subject, body, out errorMessage, null,null);
        }
                
        /*
         * generic function to send emails used by all sources including the above function.
         * 
         * */
        public static int? sendEmailToAddressee(string toAddressCommaSeparated, string subject, string body, out string errorMessage, string ccAddressCommaSeparated, string optionalListNameForDisclaimer)
        {
            try
            {
                // get the name of the database that is being used
                Tools myTools = new Tools();
                string sDatabase = myTools.GetDatabaseName();
                //send email

                // send an email to inform administrators of the new account

                // Create a new email message
                MailMessage myMessage = new MailMessage();
                myMessage.From = new System.Net.Mail.MailAddress(fromString);
                myMessage.Sender = new System.Net.Mail.MailAddress(fromString);

                // Add 'to' addresses
                myMessage.To.Add(toAddressCommaSeparated);
                //myMessage.IsBodyHtml = true;

                // If there are CC addresses, add them to the CC field
                if (null != ccAddressCommaSeparated && ccAddressCommaSeparated.Length > 5)
                    myMessage.CC.Add(toAddressCommaSeparated);
                
                // Get the subject prefix from ConfigFactory, and add the subject
                myMessage.Subject = ConfigFactory.getText("Email_SubjectPrefix") + subject;
                myMessage.IsBodyHtml = false;
                if (body != null)
                    myMessage.Body = body;
                else
                    myMessage.Body = "";

                // Add the disclaimer to the end of the email - include the mailing list name, if available
                if (null != optionalListNameForDisclaimer && optionalListNameForDisclaimer.Length > 0)
                {
                    myMessage.Body += ConfigFactory.buildText("Email_MailListTargetFooter", new string[] { optionalListNameForDisclaimer });
                }
                else
                {
                    myMessage.Body += ConfigFactory.getText("Email_GeneralTargetFooter");
                }

                // Add the disclaimer footer
                myMessage.Body += ConfigFactory.getText("Email_DisclaimerFooter") + fromString;

                /* // Old disclaimer details
                myMessage.Body += CRLF+CRLF+CRLF+CRLF+"----------------------------------"+CRLF;
                myMessage.Body += "This email is private and confidential. ";
                if (null != optionalListNameForDisclaimer && optionalListNameForDisclaimer.Length > 0)
                    myMessage.Body += CRLF + "It is sent using the BADBIR Database mailing lists to the subscribers of the mailing list :" + optionalListNameForDisclaimer;
                else
                    myMessage.Body += CRLF + "It is sent using the BADBIR Database";
                myMessage.Body += CRLF+"If you are not the intended recipient of this email, please notify us and remove it from your system. ";
                myMessage.Body += CRLF+"The response to this email will reach a share mailbox for all BADBIR staff (badbir@manchester.ac.uk).";
                */

                getSMTPClient().Send(myMessage);
                
                errorMessage = "[no error]";
                return 1;
            }
            catch (Exception e)
            {
                errorMessage = e.ToString();
                //errorMessage += "<br/>"+e.StackTrace;
                //errorMessage += "<br/>" + e.InnerException.ToString();

                return -1;
            }
        }

        public static int? sendAccountRegVerificationEmail(string toAddress)
        {
            // First, remove all entries older than 5 minutes, from the email list
            foreach(KeyValuePair<string,DateTime> item in recentRegEmails.Where(kvp => kvp.Value.AddMinutes(5) < DateTime.Now).ToList())
            {
                recentRegEmails.Remove(item.Key);
            }

            // Now check if email is in recent list
            if(recentRegEmails.ContainsKey(toAddress))
            {
                // Recent email already sent - return error 2
                return 2;
            } else
            {
                // No recent email - send verification code to this email address

                // Generate a hash code from the email address
                string hashCode = Math.Abs(toAddress.GetHashCode() % 10000).ToString().PadLeft(4,'0');

                // Set the email parameters
                string subject = "BADBIR Account Registration - please confirm your email address";
                string body = "Please enter the following code onto the BADBIR registration page to confirm your email address: " + CRLF + CRLF + hashCode + CRLF + CRLF + 
                    "If you are not registering for a BADBIR Account, please ignore this email. " + CRLF + CRLF;
                string errorMessage = "";

                // Add the email address to the recent list
                recentRegEmails.Add(toAddress,DateTime.Now);

                // Send the email
                return sendEmailToAddressee(toAddress, subject, body, out errorMessage, null, null);
            }
        }
    }
}