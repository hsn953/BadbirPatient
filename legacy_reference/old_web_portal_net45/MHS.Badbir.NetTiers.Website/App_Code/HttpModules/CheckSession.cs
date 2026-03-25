using System;
using System.Web;
using System.Web.Security;

namespace Badbir.App_Code.nsCheckSession
{
    /// <summary>
    /// Summary description for CheckSession
    /// </summary>
    public class CheckSession : IHttpModule
    {
        public CheckSession()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // Classes that inherit IHttpModule 
        // must implement the Init and Dispose methods.
        public void Init(HttpApplication app)
        {

            app.AcquireRequestState += new EventHandler(CheckSession_AcquireRequestState);
            //app.PostAcquireRequestState += new EventHandler(app_PostAcquireRequestState);
        }

        public void Dispose()
        {
            // Add code to clean up the
            // instance variables of a module.
        }

        public bool IsUserLoggedIn(HttpContext ctx)
        {
            bool bIsUserLoggedIn = false;

            // check if the user has a context
            if (null == ctx) return false;  // not logged in

            // check if the request is authenticated
            if (!ctx.Request.IsAuthenticated) return false;     // not logged in

            string UserFullname = string.Empty;

            // user is logged in and authenticated
            // check if they have a UserPartyNumber session variable - they will unless the session had been killed
            try
            {
                // check if the session variable exists
                if (null != ctx.Session["UserFullname"])
                {
                    // it exists, so check that it is not empty
                    UserFullname = ctx.Session["UserFullname"].ToString();
                    if (string.IsNullOrEmpty(UserFullname)) return false;

                    bIsUserLoggedIn = true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return bIsUserLoggedIn;
        }

        // Define a custom AcquireRequestState event handler.
        public void CheckSession_AcquireRequestState(object o, EventArgs ea)
        {
            HttpApplication httpApp = (HttpApplication)o;
            HttpContext ctx = HttpContext.Current;

            // some pages don't need to be logged in to use, so ignore those
            string sFolder = httpApp.Request.AppRelativeCurrentExecutionFilePath.ToLower();

            if (!sFolder.Contains(".aspx")) { return; }

            if (sFolder.StartsWith("~/default.aspx")) { return; }
            if (sFolder.StartsWith("~/login.aspx")) { return; }
            if (sFolder.StartsWith("~/passwordrecovery.aspx")) { return; }
            if (sFolder.StartsWith("~/register.aspx")) { return; }
            if (sFolder.StartsWith("~/patientlogin.aspx")) { return; }
            if (sFolder.StartsWith("~/checkifloggedin.aspx")) { return; }
            if (sFolder.StartsWith("~/contact.aspx")) { return; }
            if (sFolder.StartsWith("~/errorhandler.aspx")) { return; }
            if (sFolder.StartsWith("~/anon/")) { return; }
            if (sFolder.StartsWith("~/webresource.axd")) { return; }
            if (sFolder.StartsWith("~/scriptresource.axd")) { return; }

            // page requires authentication
            try
            {
                if (IsUserLoggedIn(ctx)) { return; }

                ctx.Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                ctx.Response.End();
            }
            catch { }
        }
    }
}