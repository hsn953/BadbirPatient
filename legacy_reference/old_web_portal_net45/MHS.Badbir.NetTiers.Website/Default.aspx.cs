using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Entities;
using System.Text.RegularExpressions;
using Badbir.App_Code.nsTools;

public partial class _Default : System.Web.UI.Page
{
    /**
     * 
     * 
     * 
     */
    protected void Page_Init(object sender, EventArgs e)
    {
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack) Session.Abandon();

        try
        {
            if (Request.QueryString["timeout"].ToString().Equals("true"))
            {
                Session.Abandon();
                Response.Redirect(Request.Url.ToString().Substring(0, Request.Url.ToString().Length - Request.Url.Query.Length));
            }
        }
        catch
        {
            //session timeout not sent by javascript
        }
    }

    protected void LoginControl_OnPatientLoggedIn(object sender, System.EventArgs e)
    {
        //load patient details here:
        string ApplicationName1 = Membership.ApplicationName;
        string ApplicationName2 = Membership.Provider.ApplicationName;
        string ApplicationName3 = Membership.Provider.Name;
        int ProvidersCount = Membership.Providers.Count;

        string sUsername = ((TextBox)((Login)sender).FindControl("UserName")).Text;
        MembershipUser myObject = Membership.GetUser(sUsername);
        Guid UserID = (Guid)myObject.ProviderUserKey;

        BbAdditionalUserDetail eAdditionalUserDetail = MHS.Badbir.NetTiers.Data.DataRepository.BbAdditionalUserDetailProvider.GetByUserid(UserID);

        Session["UserBADBIRuserid"] = eAdditionalUserDetail.BadbiRuserid;
        Session["UserFullname"] = eAdditionalUserDetail.FName + " " + eAdditionalUserDetail.LName;
        Session["SessionStart"] = DateTime.Now;
        Session["timestamp"] = DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy");

        string sPassword = LoginControl.Password;
        string myPasswordStrengthRegularExpression = Membership.PasswordStrengthRegularExpression;

        BbLoginLog loginLogItem = new BbLoginLog();
        loginLogItem.SessionId = Session.SessionID;
        loginLogItem.BadbirUserId = eAdditionalUserDetail.BadbiRuserid;
        loginLogItem.LoginTime = DateTime.Now;
        loginLogItem.IsOnline = true;
        loginLogItem.UserAgent = Request.ServerVariables.Get("HTTP_USER_AGENT");
        loginLogItem.Ip = Request.ServerVariables.Get("REMOTE_HOST");
        DataRepository.BbLoginLogProvider.Insert(loginLogItem);
        Session["LoginRowID"] = loginLogItem.RowId;


        if (Roles.IsUserInRole(sUsername, "Patient"))
        {
            Session["OneTimeMessageText"] = "Patient Login Successful";

            Session["OneTimeMessageType"] = NotificationType.info;

        }
        else
        {
            //Session["OneTimeMessageText"] = MHS.Badbir.NetTiers.ConfigFactory.getText("Login_UserHasNoCompany");
            Session["OneTimeMessageText"] = "User is not a patient. Please use the main login site.";

            Session["OneTimeMessageType"] = NotificationType.danger; 
            return;

        }
        return;   
    }

    protected void LoginControl_OnLoginError(object sender, System.EventArgs e)
    {
        LoginControl.FailureText = "";
        LoginControl.CssClass = "alert-danger";
        string sMessage;

        // There was a problem logging in the user
        // See if this user exists in the database
        MembershipUser userInfo = Membership.GetUser(LoginControl.UserName);
        if ((userInfo == null))
        {
            sMessage = MHS.Badbir.NetTiers.ConfigFactory.buildText("Login_UserDoesNotExist", new string[] { LoginControl.UserName });
        }
        else if (!userInfo.IsApproved)
        {
            sMessage = MHS.Badbir.NetTiers.ConfigFactory.getText("Login_UserNotApproved");
        }
        else if (userInfo.IsLockedOut)
        {
            if (userInfo.LastLockoutDate.Date.Equals(DateTime.Now.Date)) {
                // Display the normal lockout message - pass in null string for lockout date
                sMessage = MHS.Badbir.NetTiers.ConfigFactory.buildText("Login_UserLocked", new string[] {""});
            } else {
                // Message with lockout since date
                sMessage = MHS.Badbir.NetTiers.ConfigFactory.buildText("Login_UserLocked", new string[] { " since " + userInfo.LastLockoutDate.ToString("dd MMMM yyyy") });
            }
        }
        else
        {
            sMessage = MHS.Badbir.NetTiers.ConfigFactory.getText("Login_PasswordInvalid");
        }
        Master.WriteFormNotification(sMessage, Badbir.App_Code.nsTools.NotificationType.danger);
    }





}
