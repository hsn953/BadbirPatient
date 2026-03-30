using bbPatientAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace bbPatientApp.Models
{
    public class DashModel : INotifyPropertyChanged
    {
        
        private String dlqiLabelText;
        public String DlqiLabelText
        {
            get => dlqiLabelText;
            set
            {
                dlqiLabelText = value;
                OnPropertyChanged(nameof(DlqiLabelText));
            }
        }

        private String pgaLabelText;
        public String PgaLabelText
        {
            get => pgaLabelText;
            set
            {
                pgaLabelText = value;
                OnPropertyChanged(nameof(PgaLabelText));
            }
        }

        private String eqLabelText;
        public String EqLabelText
        {
            get => eqLabelText;
            set
            {
                eqLabelText = value;
                OnPropertyChanged(nameof(EqLabelText));
            }
        }

        private String medLabelText;
        public String MedLabelText
        {
            get => medLabelText;
            set
            {
                medLabelText = value;
                OnPropertyChanged(nameof(MedLabelText));
            }
        }

        private String lifestyleLabelText;
        public String LifestyleLabelText
        {
            get => lifestyleLabelText;
            set
            {
                lifestyleLabelText = value;
                OnPropertyChanged(nameof(LifestyleLabelText));
            }
        }

        private String cageLabelText;
        public String CageLabelText
        {
            get => cageLabelText;
            set
            {
                cageLabelText = value;
                OnPropertyChanged(nameof(CageLabelText));
            }
        }

        private String haqLabelText;
        public String HaqLabelText
        {
            get => haqLabelText;
            set
            {
                haqLabelText = value;
                OnPropertyChanged(nameof(HaqLabelText));
            }
        }

        private Boolean dlqiButtonEnabled = true;
        public Boolean DlqiButtonEnabled
        {
            get => dlqiButtonEnabled;
            set
            {
                dlqiButtonEnabled = value;
                OnPropertyChanged(nameof(DlqiButtonEnabled));
            }
        }

        private Boolean pgaButtonEnabled = true;
        public Boolean PgaButtonEnabled
        {
            get => pgaButtonEnabled;
            set
            {
                pgaButtonEnabled = value;
                OnPropertyChanged(nameof(PgaButtonEnabled));
            }
        }

        private Boolean eqButtonEnabled = true;
        public Boolean EqButtonEnabled
        {
            get => eqButtonEnabled;
            set
            {
                eqButtonEnabled = value;
                OnPropertyChanged(nameof(EqButtonEnabled));
            }
        }

        private Boolean medButtonEnabled = true;
        public Boolean MedButtonEnabled
        {
            get => medButtonEnabled;
            set
            {
                medButtonEnabled = value;
                OnPropertyChanged(nameof(MedButtonEnabled));
            }
        }

        private Boolean lifestyleButtonEnabled = true;
        public Boolean LifestyleButtonEnabled
        {
            get => lifestyleButtonEnabled;
            set
            {
                lifestyleButtonEnabled = value;
                OnPropertyChanged(nameof(LifestyleButtonEnabled));
            }
        }

        private Boolean cageButtonEnabled = true;
        public Boolean CageButtonEnabled
        {
            get => cageButtonEnabled;
            set
            {
                cageButtonEnabled = value;
                OnPropertyChanged(nameof(CageButtonEnabled));
            }
        }

        private Boolean haqButtonEnabled = true;
        public Boolean HaqButtonEnabled
        {
            get => haqButtonEnabled;
            set
            {
                haqButtonEnabled = value;
                OnPropertyChanged(nameof(HaqButtonEnabled));
            }
        }


        private Boolean dlqiVisible = true;
        public Boolean DlqiVisible
        {
            get => dlqiVisible;
            set
            {
                dlqiVisible = value;
                OnPropertyChanged(nameof(DlqiVisible));
            }
        }

        private Boolean pgaVisible = true;
        public Boolean PgaVisible
        {
            get => pgaVisible;
            set
            {
                pgaVisible = value;
                OnPropertyChanged(nameof(PgaVisible));
            }
        }

        private Boolean eqVisible = true;
        public Boolean EqVisible
        {
            get => eqVisible;
            set
            {
                eqVisible = value;
                OnPropertyChanged(nameof(EqVisible));
            }
        }

        private Boolean medProbVisible = true;
        public Boolean MedProbVisible
        {
            get => medProbVisible;
            set
            {
                medProbVisible = value;
                OnPropertyChanged(nameof(MedProbVisible));
            }
        }

        private Boolean lifestyleVisible = true;
        public Boolean LifestyleVisible
        {
            get => lifestyleVisible;
            set
            {
                lifestyleVisible = value;
                OnPropertyChanged(nameof(LifestyleVisible));
            }
        }

        private Boolean cageVisible = true;
        public Boolean CageVisible
        {
            get => cageVisible;
            set
            {
                cageVisible = value;
                OnPropertyChanged(nameof(CageVisible));
            }
        }

        private Boolean haqVisible = true;
        public Boolean HaqVisible
        {
            get => haqVisible;
            set
            {
                haqVisible = value;
                OnPropertyChanged(nameof(HaqVisible));
            }
        }

        //Flag if set based on psoriasis diagnosis.
        private Boolean haqNotApplicableByDiagnosis = false;
        public Boolean HaqNotApplicableByDiagnosis
        {
            get => haqNotApplicableByDiagnosis;
            set
            {
                haqNotApplicableByDiagnosis = value;
                OnPropertyChanged(nameof(HaqNotApplicableByDiagnosis));
            }
        }

        public void ParseData(SwaggerResponse DashSwag)
        {
            //reset to force reloading?
            //_dashSwag = null;

            //obtain data from swaggerResponse and set in the strings


            //LabelDashboardInfo = "Your data will be saved in Follow up " + DashSwag.BodyProperties["nextFupNumber"];


            DlqiVisible = DashSwag.BodyJObject["dlqi"].Value<bool>("allowed");
            if (DlqiVisible)
            {   
                DlqiButtonEnabled = DashSwag.BodyJObject["dlqi"].Value<bool>("availableToFill");
                DlqiLabelText = DlqiButtonEnabled ? "DLQI Form is available" : "DLQI Form Complete!";
            }
            else
            {
                DlqiButtonEnabled = false;
            }


            PgaVisible = DashSwag.BodyJObject["pgaScore"].Value<bool>("allowed");
            if (PgaVisible)
            {
                PgaButtonEnabled = DashSwag.BodyJObject["pgaScore"].Value<bool>("availableToFill");
                PgaLabelText = PgaButtonEnabled ? "PGA Form is available" : "PGA Form Complete!";
            }
            else
            {
                PgaButtonEnabled = false;
            }


            EqVisible = DashSwag.BodyJObject["euroQOL"].Value<bool>("allowed");
            if (EqVisible)
            {
                EqButtonEnabled = DashSwag.BodyJObject["euroQOL"].Value<bool>("availableToFill");
                EqLabelText = EqButtonEnabled ? "EQ-5D Form is available" : "EQ-5D Form Complete!";
            }
            else
            {
                EqButtonEnabled = false;
            }


            MedProbVisible = DashSwag.BodyJObject["medicalProblems"].Value<bool>("allowed");
            if (MedProbVisible)
            {
                MedButtonEnabled = DashSwag.BodyJObject["medicalProblems"].Value<bool>("availableToFill");
                MedLabelText = MedButtonEnabled ? "Medical Problems Form is available" : "Medical Problems Form Complete!";
            }
            else
            {
                MedButtonEnabled = false;
            }


            LifestyleVisible = DashSwag.BodyJObject["lifestyle"].Value<bool>("allowed");
            if (LifestyleVisible)
            {
                LifestyleButtonEnabled = DashSwag.BodyJObject["lifestyle"].Value<bool>("availableToFill");
                LifestyleLabelText = LifestyleButtonEnabled ? "Lifestyle Factors Form is available" : "Lifestyle Factors Form Complete!";
            }
            else
            {
                LifestyleButtonEnabled = false;
            }


            CageVisible = DashSwag.BodyJObject["cage"].Value<bool>("allowed");
            if (CageVisible)
            {
                CageButtonEnabled = DashSwag.BodyJObject["cage"].Value<bool>("availableToFill");
                CageLabelText = CageButtonEnabled?"CAGE Form is available": "CAGE Form Complete!";
            }
            else
            {
                CageButtonEnabled = false;
            }

            //Temporarily forcing true as applicable for all patients. This will be updated soon when API is picking it from the system. 
            //TODO:extra flag used temporarily because current flag is set to False in the API Hard coded. New flag doesnt exist so defaults to false.
            //When API code is changed to send the correct Allowed value, this bit will be changed back to how other flags are being used. 
            bool HaqNotApplicableByDiagnosis = DashSwag.BodyJObject["haq"].Value<bool>("haqNotAvailableByDiagnosis");
            if (HaqNotApplicableByDiagnosis) HaqVisible = false;
            else
            {
                HaqVisible = true;// DashSwag.BodyJObject["haq"].Value<bool>("allowed");
                if (HaqVisible)
                {
                    HaqButtonEnabled = DashSwag.BodyJObject["haq"].Value<bool>("availableToFill");
                    HaqLabelText = HaqButtonEnabled ? "HAQ Form is available" : "HAQ Form Complete!";
                }
                else
                {
                    HaqButtonEnabled = false;
                }
            }
        }

        //property change handlers. 
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)=>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));




    }
}
