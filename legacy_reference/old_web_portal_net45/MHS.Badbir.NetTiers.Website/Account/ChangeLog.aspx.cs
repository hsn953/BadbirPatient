using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_ChangeLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Show the appropriate div
        if(User.IsInRole("Admin"))
        {
            // Show both for admins
            divAdmins.Visible = true;
            divClinicians.Visible = true;
        } else
        {
            divClinicians.Visible = true;
        }

        // Set database revision title text
        lblDatabaseRevision.Text = MHS.Badbir.NetTiers.ConfigFactory.getText("Database_CurrentIteration");
    }
}