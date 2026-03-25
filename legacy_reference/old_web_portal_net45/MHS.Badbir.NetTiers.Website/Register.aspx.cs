using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.MailListManager;
using Badbir.App_Code.nsTools;
using System.Text.RegularExpressions;

public partial class Register : System.Web.UI.Page
{
    private string CRLF = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();
    private string PassPrefix = "A@_";
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void createPatientAccount(object sender, EventArgs e)
    {
        String UserName = NHSNumber.Text;
        String Password = PassPrefix + TbDOB.Text;




        //check if patient with these details exists?
        MHS.Badbir.NetTiers.Data.bbCrypto c = new MHS.Badbir.NetTiers.Data.bbCrypto();

        TList<BbPatient> pat = MHS.Badbir.NetTiers.Data.DataRepository.BbPatientProvider.Find("pnhs='" + c.encrypt(NHSNumber.Text) + "'");
        BbPatient thisPatient;
        if (pat.Count == 1)
        {
            if (pat[0].Dateofbirth.Value.ToString("yyyy-MM-dd") == TbDOB.Text) //format to be consisent as it becomes encrypted pwd
            {
                thisPatient = pat[0];
                c.DecryptPatient(ref thisPatient);

                MembershipCreateStatus cs = new MembershipCreateStatus();
                Membership.CreateUser(UserName, Password, "noEmailRequired" + UserName + "@badbir.org", "No Question Required", "No Answer Required", true, out cs);

                //get data from patient to populate in additionaluserdetails. 







                MembershipUser User = Membership.GetUser(NHSNumber.Text);
                // 2019D-0015 - User registration process - user is auto approved at this point so that they can login to the Registration holding page
                User.IsApproved = true;
                Membership.UpdateUser(User);



                Roles.AddUserToRole(User.UserName, "patient");

                // create a empty BbAdditionalUserDetail entity
                MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail eAdditionalUserDetail = new BbAdditionalUserDetail();

                // set the values
                eAdditionalUserDetail.Userid = (Guid)User.ProviderUserKey;
                eAdditionalUserDetail.Title = thisPatient.Title;
                eAdditionalUserDetail.FName = thisPatient.Forenames;
                eAdditionalUserDetail.LName = thisPatient.Surname;
                eAdditionalUserDetail.Position = "Patient";
                eAdditionalUserDetail.Phone = "NA";
                eAdditionalUserDetail.Hospital = "NA";


                // set audit data
                // don't know the ID of the user yet so set to 0
                eAdditionalUserDetail.Createdbyid = 0;
                eAdditionalUserDetail.Createdbyname = eAdditionalUserDetail.FName + " " + eAdditionalUserDetail.LName;
                eAdditionalUserDetail.Lastupdatedbyid = 0;
                eAdditionalUserDetail.Lastupdatedbyname = eAdditionalUserDetail.FName + " " + eAdditionalUserDetail.LName;


                // insert the BbAdditionalUserDetail entity
                MHS.Badbir.NetTiers.Data.DataRepository.BbAdditionalUserDetailProvider.Insert(eAdditionalUserDetail);

                // the entity will have been updated with an auto number for BadbiRuserid so use that to set the created and updated by IDs
                eAdditionalUserDetail.Createdbyid = eAdditionalUserDetail.BadbiRuserid;
                eAdditionalUserDetail.Lastupdatedbyid = eAdditionalUserDetail.BadbiRuserid;
                MHS.Badbir.NetTiers.Data.DataRepository.BbAdditionalUserDetailProvider.Update(eAdditionalUserDetail);


                // Prepare email string for notification & email
                string sBody1 = "New patient account registered by " + eAdditionalUserDetail.Title + " " + eAdditionalUserDetail.FName + " " + eAdditionalUserDetail.LName + " with NHS Number " + NHSNumber.Text;


                MHS.Badbir.NetTiers.Notification.generateNotification(1, 8, null, null, eAdditionalUserDetail.BadbiRuserid, MHS.Badbir.NetTiers.Notification.defaultPriority, sBody1);

                Session["OneTimeMessageText"] = "Account activated. Please continue to log in ";

                Session["OneTimeMessageType"] = NotificationType.info;


                Response.Redirect("~/Default.aspx");

            }
            else
            {
                //dob doesnt match
                Master.WriteFormNotification("Unable to match details with an existing BADBIR patient. Please contact BADBIR if you require help.", NotificationType.danger);
            }
        }
        else
        {
            Master.WriteFormNotification("Unable to find details with for existing BADBIR patient. Please contact BADBIR if you require help.", NotificationType.danger);
            return;//not made
        }



    }
}
