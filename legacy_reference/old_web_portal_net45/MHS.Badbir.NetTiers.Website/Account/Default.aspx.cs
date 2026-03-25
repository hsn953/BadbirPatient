

using System;
using Badbir.App_Code.nsTools;
public partial class mfaDefault : System.Web.UI.Page
{
    protected string auth8PublicKey = "jalsdkjflkwejflkqwejf";
    protected string auth8PrivateKey = "alksjdas";
    protected string localUserID = "";



    protected void Page_Load(object sender, EventArgs e)
    {


        localUserID = Session["UserBADBIRuserid"].ToString();

        //local
        //Session.Remove("need2fa");
        //Response.Redirect(Session["forwardLink"].ToString());
        //return;
        //*/

        if (Request.QueryString.Get("AuthSysActionCode") != null)
        {
            //response came from authsys , do something on it, reverify or proceed if you trust. 
            switch (Request.QueryString.Get("AuthSysActionCode"))
            {
                case "2"://existing auth
                case "3"://new auth
                    var authID = Request.QueryString.Get("authID");

                    string url = "https://authsys.azurewebsites.net/api/VerifyAuth?" +
                        "publickey=" + auth8PublicKey +
                        "&privatekey=" + auth8PrivateKey +
                        "&cUserID=" + localUserID +
                        "&authid=" + authID +
                        "&method2=" + 1;

                    System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    newReq.Timeout = 10000;
                    newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                    
                    
                    //string urllocal = "http://localhost:58327/api/VerifyAuth?";
                    System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();




                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Session["OneTimeMessageText"] = "Authentication Successful.";
                        Session["OneTimeMessageType"] = NotificationType.info;
                        Session.Remove("need2fa");
                        Response.Redirect(Session["forwardLink"].ToString());
                    }
                    else
                    {
                        Session["OneTimeMessageText"] = "Two factor authentication failed. Please try again or contact BADBIR";
                        Session["OneTimeMessageType"] = NotificationType.danger;
                        Response.Redirect("~/Default.aspx?Detail='Auth Not provided correctly'");

                    }

                    

                    break;
                case "1":
                    //error occured so not doing validation. can use this if allowed
                    Session["OneTimeMessageText"] = "Authentication allowed with errors.";
                    Session["OneTimeMessageType"] = NotificationType.danger;
                    Session.Remove("need2fa");
                    Response.Redirect(Session["forwardLink"].ToString());
                    break;
                default:
                    //Do Not Authenticate
                    Session["OneTimeMessageText"] = "Two factor authentication failed. Please try again or contact BADBIR";
                    Session["OneTimeMessageType"] = NotificationType.danger;
                    Response.Redirect(("~/Default.aspx?Detail='Auth Not provided correctly'"));

                    break;
            }
            Session["OneTimeMessageText"] = "Two factor authentication failed.";
            Session["OneTimeMessageType"] = NotificationType.danger;
            Response.Redirect("~/Default.aspx?detail=dontknow");
        }








    }

    protected void Proceed(object sender, EventArgs e)
    {
        Session.Remove("need2fa");
        Response.Redirect(Session["forwardLink"].ToString());
    }




    






}
