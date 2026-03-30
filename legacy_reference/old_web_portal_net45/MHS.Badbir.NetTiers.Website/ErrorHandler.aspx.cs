using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Badbir.App_Code.nsTools;

public partial class anon_ErrorHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string CurrentError = Request.QueryString["ErrorType"];
            if (string.IsNullOrEmpty(CurrentError)) CurrentError = Context.Items["ErrorType"].ToString();
            if (string.IsNullOrEmpty(CurrentError)) CurrentError = "Unknown";


            if (CurrentError.StartsWith("bbError"))
            {
                // error has been thrown deliberately 
                string sError = HttpContext.GetGlobalResourceObject("ErrorMessages", CurrentError).ToString();
                if (!string.IsNullOrEmpty(sError))
                {
                    // there's an error message
                    ((MasterPages_bbDefault)Page.Master).WriteFormNotification(sError, NotificationType.danger);
                    return;
                }
            }

            switch (CurrentError)
            {
                case "404":
                    CheckForKnown404s();
                    ((MasterPages_bbDefault)Page.Master).WriteFormNotification("Sorry - the page that you were looking for could not be found.", NotificationType.danger);
                    break;
                case "LostContext":
                    ((MasterPages_bbDefault)Page.Master).WriteFormNotification("Sorry - you have been logged out. Please try <a href=\"/applications/\">logging in again</a>.", NotificationType.danger);
                    break;
                default:

                    ((MasterPages_bbDefault)Page.Master).WriteFormNotification("Sorry - an error has occurred. Our administrators have been emailed and we hope to fix the problem as soon as possible.", NotificationType.danger);
                break;
            }
        }
        catch(Exception )
        {
            ((MasterPages_bbDefault)Page.Master).WriteFormNotification("Sorry - an error has occurred. Our administrators have been emailed and we hope to fix the problem as soon as possible.", NotificationType.danger);
        }
    }

    private void CheckForKnown404s()
    {
        /*
        try
        {

            string sURL = Request.QueryString["url"];
            if (string.IsNullOrEmpty(sURL)) return;

            if (sURL.IndexOf("/applications/staffprofile") > 0)
            {
                Response.Redirect("/applications/staff/profile/");
                Response.End();
            }

            if (sURL.IndexOf("applications%2fstaffprofile") > 0)
            {
                Response.Redirect("/applications/staff/profile/");
                Response.End();
            }
        }
        catch { }
        */
    }
}
