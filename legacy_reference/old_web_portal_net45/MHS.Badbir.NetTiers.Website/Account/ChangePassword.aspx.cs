using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Badbir.App_Code.nsTools;
using System.Text.RegularExpressions;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ChangePassword1.ChangingPassword += new LoginCancelEventHandler(this.ChangePassword1_ChangingPassword);
    }

    protected void ChangePassword1_ChangingPassword(Object sender, LoginCancelEventArgs e)
    {
        if (ChangePassword1.CurrentPassword.Equals(ChangePassword1.NewPassword))
        {
            Master.WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Account_PasswordsMustDiffer"), NotificationType.danger);
            e.Cancel = true;
            return;
        }

        if (!ChangePassword1.ConfirmNewPassword.Equals(ChangePassword1.NewPassword))
        {
            Master.WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Account_PasswordsMustDiffer"), NotificationType.danger);
            e.Cancel = true;
            return;
        }

        if (!Membership.ValidateUser(ChangePassword1.UserName, ChangePassword1.CurrentPassword))
        {
            Master.WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Account_OldPasswordIncorrect"), NotificationType.danger);
            e.Cancel = true;
            return;
        }

        string myPasswordStrengthRegularExpression = Membership.PasswordStrengthRegularExpression;

        // check the new password against the regular expression in the membership provider in the web.config
        if (!Regex.IsMatch(ChangePassword1.NewPassword, myPasswordStrengthRegularExpression))
        {
            Master.WriteFormNotification(MHS.Badbir.NetTiers.ConfigFactory.getText("Account_PasswordCriteriaNotMet"), NotificationType.danger);
            e.Cancel = true;
            return;
        }
    }
}
