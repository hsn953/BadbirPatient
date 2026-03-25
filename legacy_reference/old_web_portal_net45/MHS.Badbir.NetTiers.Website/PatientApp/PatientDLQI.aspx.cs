using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Badbir.App_Code.nsTools;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Web.UI;


public partial class PatientApp_PatientDLQI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormUtil.SetDefaultMode(FormView1, "FormID");

    }


    protected void FormView1_ItemCreated(Object sender, EventArgs e)
    {

        // Iterate through the items in the Values collection
        // and verify that the user entered a value for each 
        // text box displayed in the insert item template. Cancel
        // the insert operation if the user left a text box empty.
        if (FormView1.CurrentMode == FormViewMode.Insert)
        {
            
            /*TextBox tFupId = (TextBox)FormView1.Row.FindControl("dataPappFupId");
            if (tFupId != null)
            {
               // ((TextBox)FormView1.Row.FindControl("dataPappFupId")).Text = "328857"; //temporary no selection of fup
            }*/
        }

    }





    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        //redirect etc
    }
}