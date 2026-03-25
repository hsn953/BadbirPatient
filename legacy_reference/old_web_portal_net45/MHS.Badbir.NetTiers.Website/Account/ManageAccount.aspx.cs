using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using Badbir.App_Code.nsTools;
using System.Text.RegularExpressions;


public partial class ManageAccount:System.Web.UI.Page
{
    protected string auth8PublicKey = "jalsdkjflkwejflkqwejf";
    protected string auth8PrivateKey = "alksjdas";
    protected string localUserID = "";




        

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MemberData.SelectParameters.Clear();
            MemberData.SelectParameters.Add(new Parameter("UserName", System.Data.DbType.String, User.Identity.Name));

            AdditionalUserData.Parameters.Clear();
            AdditionalUserData.Parameters.Add("userid", Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            LoginLogDS.Parameters.Clear();
            LoginLogDS.Parameters.Add("BadbirUserId", TypeCode.Int32, Session["UserBADBIRuserid"].ToString());

            if (((MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail)AdditionalUserData.GetCurrentEntity()).Require2Fa)
            {
                twoFA.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["showOTT"] != null && Session["showOTT"].Equals(1))
        {
            Session.Remove("showOTT");
            showOTT();
        }
        else
            codeLiteral.Visible = false;

        if (Session["showNumbers"] != null && Session["showNumbers"].Equals(1))
        {
            Session.Remove("showNumbers");
            showNumbers();
        }
        else
            numbersLiteral.Visible = false;

    }

    protected void deauthenticate2FA(object sender, EventArgs e)
    {
                //RemoveAuthController

        if (!twoFA.Visible)
            return;

        try
        {

            localUserID = Session["UserBADBIRuserid"].ToString();

            string url = "https://authsys.azurewebsites.net/api/RemoveAuth?" +
                                  "publickey=" + auth8PublicKey +
                                  "&privatekey=" + auth8PrivateKey +
                                  "&cUserID=" + localUserID;

            System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            newReq.Timeout = 10000;
            newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["OneTimeMessageText"] = "All existing authentications have been revoked.";
                Session["OneTimeMessageType"] = NotificationType.info;
                var reader = new System.IO.StreamReader(resp.GetResponseStream(), true);
                string responseText = reader.ReadToEnd();



            }
            else
            {
                Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 911.";
                Session["OneTimeMessageType"] = NotificationType.danger;
            }

        }
        catch
        {
            Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 912.";
            Session["OneTimeMessageType"] = NotificationType.danger;

        }


        Response.Redirect(Request.Url.ToString());

    }



    protected void removeSelectedNumber(object sender, EventArgs e)
    {

        
        try
        {

            localUserID = Session["UserBADBIRuserid"].ToString();

            string url = "https://authsys.azurewebsites.net/api/UserNumbers?" +
                                  "publickey=" + auth8PublicKey +
                                  "&privatekey=" + auth8PrivateKey +
                                  "&cUserID=" + localUserID +
                                  "&numberID=" + ((GridView)sender).SelectedDataKey.Value;


            System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            newReq.Timeout = 10000;
            newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["OneTimeMessageText"] = "Number marked as invalid on authentication server.";
                Session["OneTimeMessageType"] = NotificationType.info;
                var reader = new System.IO.StreamReader(resp.GetResponseStream(), true);
                string responseText = reader.ReadToEnd();
                Session["2faNumbersString"] = responseText;
                Session["showNumbers"] = 1;



            }
            else
            {
                Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 921.";
                Session["OneTimeMessageType"] = NotificationType.danger;
            }

        }
        catch
        {
            Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 922.";
            Session["OneTimeMessageType"] = NotificationType.danger;

        }


        Response.Redirect(Request.Url.ToString());




    }

    protected void addNumberToList(object sender, EventArgs e)
    {
        while (TextNewNumber.Text.StartsWith("0"))
        { TextNewNumber.Text = TextNewNumber.Text.Substring(1); }

        if (TextNewNumber.Text.Length < 9)
        {
            Session["OneTimeMessageText"] = "The number doesn't seem to be valid. Please contact BADBIR with Error Code 932 if error persists.";
            Session["OneTimeMessageType"] = NotificationType.danger;
            Response.Redirect(Request.Url.ToString());
            //error, cant save invalid number
            return;
        }
        string longNumber = DDcountry.SelectedValue + TextNewNumber.Text;


        try
        {

            localUserID = Session["UserBADBIRuserid"].ToString();

            string url = "https://authsys.azurewebsites.net/api/UserNumbers?" +
                                  "publickey=" + auth8PublicKey +
                                  "&privatekey=" + auth8PrivateKey +
                                  "&cUserID=" + localUserID+
                                  "&commType="+ DDCommType.SelectedValue+
                                  "&longNewNumber="+longNumber;


            System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            newReq.Timeout = 10000;
            newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["OneTimeMessageText"] = "Number added on authentication server and will be verified after first use.";
                Session["OneTimeMessageType"] = NotificationType.info;
                var reader = new System.IO.StreamReader(resp.GetResponseStream(), true);
                string responseText = reader.ReadToEnd();
                Session["2faNumbersString"] = responseText;
                Session["showNumbers"] = 1;



            }
            else
            {
                Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 921.";
                Session["OneTimeMessageType"] = NotificationType.danger;
            }

        }
        catch
        {
            Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 922.";
            Session["OneTimeMessageType"] = NotificationType.danger;

        }


        Response.Redirect(Request.Url.ToString());



    }

    protected void getNumbersList(object sender, EventArgs e)
    {
        try
        {

            localUserID = Session["UserBADBIRuserid"].ToString();

            string url = "https://authsys.azurewebsites.net/api/UserNumbers?" +
                                  "publickey=" + auth8PublicKey +
                                  "&privatekey=" + auth8PrivateKey +
                                  "&cUserID=" + localUserID;

            System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            newReq.Timeout = 10000;
            newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["OneTimeMessageText"] = "List of numbers obtained from authentications server.";
                Session["OneTimeMessageType"] = NotificationType.info;
                var reader = new System.IO.StreamReader(resp.GetResponseStream(), true);
                string responseText = reader.ReadToEnd();
                Session["2faNumbersString"] = responseText;
                Session["showNumbers"] = 1;



            }
            else
            {
                Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 921.";
                Session["OneTimeMessageType"] = NotificationType.danger;
            }

        }
        catch
        {
            Session["OneTimeMessageText"] = "Unable to contact authentication server. Please contact BADBIR with Error Code 922.";
            Session["OneTimeMessageType"] = NotificationType.danger;

        }


        Response.Redirect(Request.Url.ToString());

    }

    public class numberRow
    {

        public numberRow(int ID, string number, string isVerified, string commMedium)
        {
            
         this.ID = ID;
         this.number = number;
         this.isVerified = Convert.ToBoolean(isVerified);
         this.commMedium = commMedium;

        }


        public int ID{get;set;}
        public string number{get;set;}
        public bool isVerified { get; set; }
        public string commMedium{get;set;}

    }

    //show numbers
        protected void showNumbers()
            {

        //follow the format. minutes to live, n codes, codes.
        string[] responses = Session["2faNumbersString"].ToString().Split(',');
        
    
        int numberCount = (Convert.ToInt32(Convert.ToInt32(Regex.Match(responses[0], @"\d+").ToString())));

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (numberCount == 0)
        {
            sb.Append("<b>No valid saved numbers found. Please add a number to use 2FA</b>");
        }
        else
        {
            //PARSE the response and show numbers with link on each number to invalidate it.

            List<numberRow> numberList = new List<numberRow>();
            for (int i = 1; i < numberCount * 4; )
            {
                numberList.Add(new numberRow(Convert.ToInt32(responses[i++]), responses[i++], responses[i++], responses[i++]));
            }
            numberGridView.DataSource = numberList;
            numberGridView.DataBind();


        }


        //numbersLiteral.InnerHtml = sb.ToString();




        

    }    
    



    protected void getOTT(object sender, EventArgs e)
    {
        if (!twoFA.Visible)
            return;

        try
        {

            localUserID = Session["UserBADBIRuserid"].ToString();

            string url = "https://authsys.azurewebsites.net/api/NewOTT?" +
                                  "publickey=" + auth8PublicKey +
                                  "&privatekey=" + auth8PrivateKey +
                                  "&cUserID=" + localUserID;

            System.Net.HttpWebRequest newReq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            newReq.Timeout = 10000;
            newReq.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)newReq.GetResponse();


            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Session["OneTimeMessageText"] = "New One Time Tokens obtained successfully.";
                Session["OneTimeMessageType"] = NotificationType.info;
                var reader = new System.IO.StreamReader(resp.GetResponseStream(), true);
                string responseText = reader.ReadToEnd();
                Session["ottString"] = responseText;
                Session["showOTT"]=1;

                

            }
            else
            {
                Session["OneTimeMessageText"] = "Unable to obtain One Time Tokens for this account. Please contact BADBIR with Error Code 901.";
                Session["OneTimeMessageType"] = NotificationType.danger;
            }

        }
        catch
        {
            Session["OneTimeMessageText"] = "Unable to obtain One Time Tokens for this account. Error2, Please contact BADBIR with Error Code 902.";
            Session["OneTimeMessageType"] = NotificationType.danger;

        }


        Response.Redirect(Request.Url.ToString());


    }





    protected void showOTT()
    {

        //follow the format. minutes to live, n codes, codes.
        string[] responses = Session["ottString"].ToString().Split(',');
        DateTime expiryTime = DateTime.Now.AddMinutes(Convert.ToInt32(Regex.Match(responses[0], @"\d+").ToString()));
        int codeCount = (Convert.ToInt32(responses[1]));

        System.Text.StringBuilder sb = new System.Text.StringBuilder();


        sb.Append("<b>One Time Tokens</b><br/>Refreshing the page will hide these codes.<br/>Generated: ").Append(DateTime.Now.ToUniversalTime());
        sb.Append("<br/>Expiry: ").Append(expiryTime.ToUniversalTime());
        for (int i = 0; i < codeCount; ++i)
        {
            int nextCode = 0;
            Int32.TryParse(Regex.Match(responses[i + 2], @"\d+").Value, out nextCode);
            sb.Append("<br/>Code # ").Append((i + 1).ToString()).Append(" - ").Append(nextCode);
        }
        sb.Append("<br/><br/>").Append("<button style=\"font-family:monospace \" onclick=\"PrintElem('ctl00_MainContent_codeLiteral')\">Print</button>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
        sb.Append("<button style=\"font-family:monospace \" onclick=\"downloadInnerHtml('badbirOTTCodes.htm', 'ctl00_MainContent_codeLiteral', 'text')\">Save As HTML File</button>");




        codeLiteral.InnerHtml = sb.ToString();



    }
    protected void toggle2FA(object sender, EventArgs e) {
        //not implemented yet - should this be allwoed to user?
    }
}
