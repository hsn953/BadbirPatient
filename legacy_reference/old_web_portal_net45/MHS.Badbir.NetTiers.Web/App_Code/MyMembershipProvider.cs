using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;

namespace MHS.Badbir.NetTiers.Web
{

    /// <summary>
    /// Summary description for MyMembershipProvider
    /// </summary>
    /// 
    public class MyMembershipProvider : System.Web.Security.SqlMembershipProvider
    {
        public MyMembershipProvider()
            : base()
        {
        }

        public override string GeneratePassword()
        {

            string myPasswordStrengthRegularExpression = Membership.PasswordStrengthRegularExpression;
            string sPassword = base.GeneratePassword();

            while (!Regex.IsMatch(sPassword, myPasswordStrengthRegularExpression))
            {
                sPassword = base.GeneratePassword();

            }

            return sPassword;


        }

    }
}