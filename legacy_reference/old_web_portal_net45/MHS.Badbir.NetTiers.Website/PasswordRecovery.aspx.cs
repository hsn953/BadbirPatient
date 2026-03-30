using System;
using System.Web.UI.WebControls;
using MHS.Badbir.NetTiers.MailListManager;
using Badbir.App_Code.nsTools;

public partial class PasswordRecovery : System.Web.UI.Page
{
    private string CRLF = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        /*
        if (IsPostBack && !ReCaptcha.SkipCaptcha)
        {
            try
            {
                //response checking
                var encodedResponse = Request.Form["g-Recaptcha-Response"];
                var isCaptchaValid = ReCaptcha.Validate(encodedResponse);

                if (!isCaptchaValid)
                {
                    // E.g. Return to view or set an error message to visible
                    //check captcha verification 
                    Session["OneTimeMessageText"] = "Captcha verification failed";
                    Session["OneTimeMessageType"] = NotificationType.danger;

                    //check session here
                    Response.Redirect(Request.Url.ToString());
                    Response.End();
                }
            }
            catch
            { //captha checking failed

            }

        }

                */


    }



    // Set the field label background color if the user name is not found.
    protected void PasswordRecovery1_UserLookupError(object sender, System.EventArgs e)
    {
        PasswordRecovery1.LabelStyle.ForeColor = System.Drawing.Color.Red;

        
    }

    // Reset the field label background color.
    protected void PasswordRecovery1_Load(object sender, System.EventArgs e)
    {
        
        PasswordRecovery1.LabelStyle.ForeColor = System.Drawing.Color.Black;
    }

    protected void PasswordRecovery1_OnSendingMail(object sender, MailMessageEventArgs e)
    {
        // create a new password recovery email and send that, rather than use e.Message - just to be sure of the settings that are being used to send the message.

        string errorMsg;
        MailListManager.sendEmailToAddressee(e.Message.To.ToString(), "Your BADBIR Account", e.Message.Body, out errorMsg);

        


        e.Cancel = true;
    }
}
