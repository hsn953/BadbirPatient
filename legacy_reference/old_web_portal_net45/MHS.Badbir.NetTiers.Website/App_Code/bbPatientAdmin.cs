using System;
using System.Web;
using MHS.Badbir.NetTiers.Entities;


namespace MHS.Badbir.NetTiers.bbPatientAdmin
{
    /// <summary>
    /// Summary description for bbPatientAdmin
    /// </summary>
    public class bbPatientAdmin
    {
        public bbPatientAdmin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool IsFollowUpEditable(int Fupid)
        {
            BbPatientCohortTracking myBbPatientCohortTracking = MHS.Badbir.NetTiers.Data.DataRepository.BbPatientCohortTrackingProvider.GetByFupId(Fupid);
            if (null != myBbPatientCohortTracking)
            {
                DateTime? EditWindowFrom = myBbPatientCohortTracking.EditWindowFrom;
                DateTime? EditWindowTo = myBbPatientCohortTracking.EditWindowTo;
                DateTime? Duedate = myBbPatientCohortTracking.Duedate;

                // check the start date of the window
                DateTime StartDate;
                if (!(null != EditWindowFrom) && !(null != Duedate)) return false; // no date to use as a start date
                if (null != EditWindowFrom)
                    StartDate = (DateTime)EditWindowFrom;
                else
                    StartDate = ((DateTime)Duedate).AddDays(-30);

                if (StartDate > DateTime.Now) return false;

                // check the end date of the window
                DateTime EndDate;
                if (!(null != EditWindowFrom) && !(null != EditWindowTo) && !(null != Duedate)) return false; // no date to use as an end date
                if (null != EditWindowTo)
                    EndDate = (DateTime)EditWindowTo;
                else if (null != EditWindowFrom)
                    EndDate = ((DateTime)EditWindowFrom).AddDays(21);
                else
                    EndDate = ((DateTime)Duedate).AddDays(21);

                if (EndDate < DateTime.Now) return false;

                // start and end dates are ok, so must be in the edit window
                return true;
            }

            return false;
        }

        public void TriggerWindowOpen(int FupId)
        {
            MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking PCT = MHS.Badbir.NetTiers.Data.DataRepository.BbPatientCohortTrackingProvider.GetByFupId(FupId);

            if (null == PCT.EditWindowFrom)
            {
                PCT.EditWindowFrom = DateTime.Now;
                PCT.Dateentered = DateTime.Now;
                PCT.Fupstatus = 2;  // 'In Edit Window'
                //opening edit window means creating fup
                PCT.Createdbyid = Convert.ToInt32(HttpContext.Current.Session["UserBADBIRuserid"]);
                PCT.Createdbyname = HttpContext.Current.Session["UserFullname"].ToString();
                PCT.Createddate = DateTime.Now;

                MHS.Badbir.NetTiers.Data.DataRepository.BbPatientCohortTrackingProvider.Update(PCT);
            }



            
        }



        public bool DoesUserHaveAccessToPatient(int PatientId)
        {
            try
            {
                int UserBADBIRuserid = Convert.ToInt32(HttpContext.Current.Session["UserBADBIRuserid"]);
                bool? bClinicianHasAccessToPatient = false;
                MHS.Badbir.NetTiers.Data.DataRepository.BbPatientProvider.Patient_Check_Access_forClinician(PatientId, UserBADBIRuserid, ref bClinicianHasAccessToPatient);
                if (true == bClinicianHasAccessToPatient) return true;
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}