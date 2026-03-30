using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Entities;
using Badbir.App_Code.nsTools;

public partial class MasterPages_bbStaticNoMenu : System.Web.UI.MasterPage
{
    #region Private Properties
    private int? currentPatientId;
    private int? currentChid;
    private int? currentFupId;
    private bool? isBaseline;
    private string _PatientFormTitle = null;
    #endregion Private Properties

    #region Public Properties

    public BbPatient thisPatient
    {
        get
        {
            bbCrypto myCrypto = new bbCrypto();
            BbPatient p = MHS.Badbir.NetTiers.Data.DataRepository.BbPatientProvider.GetByPatientid(CurrentPatientId);
            myCrypto.DecryptPatient(ref p);
            return p;
        }
    }

    // sets the h1 element
    public int CurrentPatientId
    {
        get
        {
            // if there's a private value, use that
            if (null != currentPatientId)
                return Convert.ToInt32(currentPatientId);

            string sCurrentPatientId = string.Empty;    // Request.QueryString["Patientid"];
            if (!string.IsNullOrEmpty(sCurrentPatientId))
            {
                int iCurrentPatientId = Convert.ToInt32(sCurrentPatientId);
                HttpContext.Current.Session["PatientId"] = iCurrentPatientId;
                return iCurrentPatientId;
            }
            else
            {
                sCurrentPatientId = HttpContext.Current.Session["PatientId"].ToString();
                int iCurrentPatientId = Convert.ToInt32(sCurrentPatientId);
                return iCurrentPatientId;
            }
        }
        //set
        //{
        //    HttpContext.Current.Session["PatientId"] = CurrentPatientId;
        //}
    }

    public int CurrentChid
    {
        get
        {
            // if there's a private value, use that
            if (null != currentChid)
                return Convert.ToInt32(currentChid);

            // a CurrentPatientId is needed to be able to get the CurrentChid
            int iCurrentPatientId = this.CurrentPatientId;  // an error will be thrown if there isn't one

            string sCurrentChid = string.Empty;
            sCurrentChid = HttpContext.Current.Session["Chid"].ToString();
            return Convert.ToInt32(sCurrentChid);
        }
        //set
        //{
        //    HttpContext.Current.Session["Chid"] = value;
        //}
    }

    public int CurrentFupId
    {
        get
        {
            // if there's a private value, use that
            if (null != currentFupId)
                return Convert.ToInt32(currentFupId);

            // a CurrentChid is needed to be able to get the CurrentFupId
            int iCurrentChid = this.CurrentChid;  // an error will be thrown if there isn't one

            string sCurrentFupId = string.Empty;
            sCurrentFupId = HttpContext.Current.Session["FupId"].ToString();
            return Convert.ToInt32(sCurrentFupId);
        }
    }

    public bool IsBaseline
    {
        get
        {
            // if there's a private value, use that
            if (null != isBaseline)
                return Convert.ToBoolean(isBaseline);

            string sBaseline = string.Empty;    // Request.QueryString["baseline"];
            if ("true" == sBaseline)
                return true;

            return false;
        }
    }
    
    // sets the h2 element
    public string PatientFormTitle
    {
        get { return this.hPatientFormTitle.InnerHtml; }
        set
        {
            _PatientFormTitle = value;
            if (_PatientFormTitle != null)
            {
                // first choice (can set it to an empty string)
                this.hPatientFormTitle.InnerHtml = _PatientFormTitle;
            }
        }
    }

    #endregion Public Properties

    #region Public Methods

    public void WritePatientFormNotification(string sMessage, NotificationType ntType)
    {
        this.dPatientFormNotification.InnerHtml = sMessage;
        this.dPatientFormNotification.Attributes.Add("class", "message-" + ntType);
        this.dPatientFormNotification.Visible = true;
    }

    public void WritePatientFormTitle(string sPatientFormTitle)
    {
        this.PatientFormTitle = sPatientFormTitle;
    }

    #endregion Public Methods



    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
