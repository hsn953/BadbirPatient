using System;
using System.Web;


public partial class anon_CallBackService : System.Web.UI.Page
{


    protected void checkLoginStatus()
    {
        try
        {
            if (Session["LastPageLoadTime"].ToString().Length > 0)
            {
                DateTime lastpageloadtime = new DateTime(long.Parse(Session["LastPageLoadTime"].ToString()));

                Response.Write(lastpageloadtime.Ticks.ToString());
            }
            else
            {
                Response.Write("0");
            }
        }
        catch //logged out with no session value
        {
            Response.Write("0");
        }
    }




    protected void renewSession()
    {
        try
        {
            if (Session["LastPageLoadTime"].ToString().Length > 0)
            {
                Session["LastPageLoadTime"] = (DateTime.Now.Hour * 3600) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
                Response.Write(Session["LastPageLoadTime"]);
            }
        }
        catch //logged out with no session value
        {
            Response.Write("0");
        }
    }
   
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ClearHeaders();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));


        switch (Request.QueryString["act"])
        {
            case "1":
                checkLoginStatus();
                break;
            case "2":
                renewSession();
                break;
            default:
                break;
        }


    }
}
