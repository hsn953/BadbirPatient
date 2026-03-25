using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Badbir.App_Code.nsTools;

public partial class MasterPages_bbDefault : System.Web.UI.MasterPage
{
    #region Public Properties

    // sets the h1 element
 
    #endregion Public Properties

    #region Private Properties

    private string _FormTitle = null;
    
    #endregion Private Properties

    #region Public Methods

    public void WriteFormNotification(string sMessage, NotificationType ntType)
    {
        this.dFormNotification.InnerHtml = sMessage;
        this.dFormNotification.Attributes.Add("class", "text-center alert alert-" + ntType);
        this.dFormNotification.Visible = true;
    }



    #endregion Public Methods


    protected void Page_init(object sender, EventArgs e)
    {
        try
        {
            
            
            if (Session.Contents.Count == 0)
            {

                if (Session.IsNewSession && Request.UrlReferrer.Segments.Length > 2 &&
                        !(Request.UrlReferrer.Segments.Length == 3 && Request.UrlReferrer.LocalPath.ToUpper().EndsWith("/DEFAULT.ASPX")))
                {

                    WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Login_LogoutMessage"), NotificationType.info);
                    Session.Abandon();
                    return;
                }
            }
            else if(Session["need2fa"] != null){
                WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Login_TwoFactorRequired"),NotificationType.danger);

                Session.Abandon();
                return;

            }
        }
        catch (NullReferenceException )
        {
            
            //WriteFormNotification("Please log in again", NotificationType.information);
            Session.Abandon();
            return;
            //do nothing as the location is empty (default)
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        Session["LastPageLoadTime"] = (DateTime.Now.Hour *3600) + (DateTime.Now.Minute *60) + DateTime.Now.Second; 

        // Added to get around the code block issue
        // http://leedumond.com/blog/the-controls-collection-cannot-be-modified-because-the-control-contains-code-blocks/
        Page.Header.DataBind();

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));




        try
        {

            if (!string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                string sUserFullname = "";
                try
                {
                    if (!string.IsNullOrEmpty(Session["UserFullname"].ToString())) sUserFullname = Session["UserFullname"].ToString();
                }
                catch { }

                //sWelcome.InnerHtml = sUserFullname + " (" + HttpContext.Current.User.Identity.Name + ")";
                
            }
        }
        catch { }

        // a OneTime message may have been written to the user's session
        // if so, get the message and type, write it to the notification section of the page and then null the session variables
        try
        {
            string sOneTimeMessageText = Session["OneTimeMessageText"].ToString();

            NotificationType myNotificationType = NotificationType.info;
            try
            {
                myNotificationType = (NotificationType)Session["OneTimeMessageType"];
            }
            catch { }

            if (!string.IsNullOrEmpty(sOneTimeMessageText))
            {
                Session["OneTimeMessageText"] = null;
                Session["OneTimeMessageType"] = null;

                if (Page.Master is MasterPages_bbDefault || Page.Master.Master is MasterPages_bbDefault)
                {
                    WriteFormNotification(sOneTimeMessageText, myNotificationType);
                }
            }
        }
        catch { }




    }

    protected void LoginStatus_OnLoggingOut(object sender, LoginCancelEventArgs e)
    {
        Session.Abandon();
    }


}
