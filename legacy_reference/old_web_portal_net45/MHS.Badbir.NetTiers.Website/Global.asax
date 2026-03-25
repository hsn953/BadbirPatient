<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Configuration" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Globalization" %>
<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

        CustomErrorsSection currentCustomErrors = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");

        if (currentCustomErrors.Mode == CustomErrorsMode.Off) return;

        try
        {
            // get the current error
            HttpContext ctx = System.Web.HttpContext.Current;
            //handling 404 as session is null when 404. No need to send 404 to error handler as that has a master page looking for session. 
            Exception objErr = ctx.Server.GetLastError().GetBaseException();
            ctx.Server.ClearError();

            if (null == ctx.Session || (objErr.StackTrace.ToString().IndexOf("System.Web.UI.TemplateParser.GetParserCacheItem()") > 0) || (objErr.StackTrace.ToString().IndexOf("at System.Web.UI.Util.CheckVirtualFileExists(VirtualPath virtualPath)") > 0)) 
            {
                string sURL = ctx.Request.Url.ToString();
                if (!string.IsNullOrEmpty(sURL)) sURL = System.Web.HttpUtility.UrlEncode(sURL);
                ctx.Items["url"] = sURL;
                ctx.Items["ErrorType"] = "404";
               

                Response.Redirect("~/Error404.html");
                Response.End();
                return;
            }
            
            
            // put error message into a simple and a more descriptive string
            string CurrentError = objErr.Message.ToString();
            string FullCurrentError = "\n\n*************************\nError caught in BADBIR Application" +
                        "\n\nTimestamp: " + DateTime.Now +
                        "\n\nURL: " + ctx.Request.Url.ToString() +
                        "\n\nError Message: " + objErr.Message.ToString() +
                        "\n\nStack Trace: " + objErr.StackTrace.ToString() +
                        "\n\n--End of stack trace--\n";
            string FullCurrentErrorFormatted = FormatError(ctx, CurrentError, FullCurrentError);

            if (CurrentError.StartsWith("bbError"))
            {
                // error has been thrown deliberately 
                string sError = HttpContext.GetGlobalResourceObject("ErrorMessages", CurrentError).ToString();
                if (!string.IsNullOrEmpty(sError))
                {
                    // there's an error message
                    
                    ctx.Items["ErrorType"] = CurrentError;
                    Server.Transfer("~/errorhandler.aspx", true);

                    return;
                }
            }

           
        }
        catch(Exception )
        {
            string errornotifications = "";
            try
            {
                HttpContext ctx = System.Web.HttpContext.Current;
                errornotifications = "IP address: " + ctx.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            catch
            {
                errornotifications = "Context was NULL";
            }

            Response.Redirect("~/Default.aspx");
            Response.End();
        }

        Server.Transfer("~/errorhandler.aspx", true);
    }

    private string FormatError(HttpContext ctx, string CurrentError, string FullCurrentError)
    {
        string sFormattedError = string.Empty;
        string CRLF = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();

        try
        {

            if (CurrentError.IndexOf("The timeout period elapsed prior to obtaining a connection from the pool") > 0)
            {
                sFormattedError = "Badbir TimeOut: " + ctx.Request.Url.ToString();
            }
            else
            {
                if (CurrentError.IndexOf("The timeout period elapsed prior to completion of the operation or the server is not responding") > 0)
                {
                    sFormattedError = "Badbir TimeOut: " + ctx.Request.Url.ToString();
                }
                else
                {
                    sFormattedError = "Badbir Error in: " + ctx.Request.Url.ToString();
                }
            }
            sFormattedError = FullCurrentError + CRLF;
            try { sFormattedError += CRLF + "User: " + ctx.User.Identity.Name + CRLF; }
            catch { }

            try { sFormattedError += CRLF + "IP address: " + ctx.Request.ServerVariables["REMOTE_ADDR"].ToString() + CRLF; }
            catch { }

            // 28/05/2020 add another few line breaks after IP address
            sFormattedError += "\n\n\n";

        }
        catch (Exception exc)
        {
            sFormattedError = "Error in FormatError: " + exc.Message + "\n" + exc.StackTrace;
        }

        return sFormattedError;
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
       
            

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        
        
        //save this in database:
        /* Last time a web page was opened:
         * 		Convert.ToInt32(Session["LastPageLoadTime"]) /3600	16	int
		(Convert.ToInt32(Session["LastPageLoadTime"]) %3600)/60	5	int
		(Convert.ToInt32(Session["LastPageLoadTime"]) %60)	48	int

         * Session expiry time? Datetime.now
         * Login details:
         * +		Session["SessionStart"]	{24/03/2016 16:05:40}	object {System.DateTime}
		Session["UserBADBIRuserid"]	347	object {int}
		Session["UserFullname"]	"Hassan Ali"	object {string}

         * 
         * */
        try
        {
            if (null != Session && Session.Count > 0 && null != Session.SessionID)
            {
                if (null != Session["LoginRowID"])
                {
                    MHS.Badbir.NetTiers.Entities.BbLoginLog log = MHS.Badbir.NetTiers.Data.DataRepository.BbLoginLogProvider.GetByRowId(Convert.ToInt32(Session["LoginRowID"]));
                    log.LogoutTime = DateTime.Now;
                    log.IsOnline = false;
                    log.LastReqTime = DateTime.Today.AddHours(Convert.ToInt32(Session["LastPageLoadTime"]) / 3600).AddMinutes(Convert.ToInt32(Session["LastPageLoadTime"]) % 3600 / 60).AddSeconds(Convert.ToInt32(Session["LastPageLoadTime"]) % 60);
                    MHS.Badbir.NetTiers.Data.DataRepository.BbLoginLogProvider.Save(log);
                }
            }
        }catch(Exception ){}

    }
    
    
    
       
</script>
