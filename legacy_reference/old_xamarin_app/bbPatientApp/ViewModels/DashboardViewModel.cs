using bbPatientAPI;
using bbPatientApp.Models;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace bbPatientApp.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public Command DlqiCommand { get; }
        public Command PgaCommand { get; }
        public Command Eq5dCommand { get; }
        public Command MedprobsCommand { get; }
        public Command LifestyleCommand { get; }
        public Command CageCommand { get; }
        public Command HaqCommand { get; }


        //to be moved in dashmodel? 
        public String LabelDashboardInfo { get { return "Your data will be saved in Follow up " + DashSwag.BodyProperties["nextFupNumber"]; } }
        public Int32 FupNo { get { return Int32.Parse(DashSwag.BodyProperties["nextFupNumber"]); } }

        public DashModel Dash { get; } = new DashModel();



        protected SwaggerResponse _dashSwag;
        public SwaggerResponse DashSwag
        {
            get {
                if (_dashSwag != null)
                {
                    return _dashSwag;
                }
                else
                {
                    //load sw.
                    try
                    {
                        var token = Xamarin.Essentials.SecureStorage.GetAsync("token").Result;
                        if(token != null)
                        {
                            //restructure awaiting
                            bbPatientAPI.Client client = new bbPatientAPI.Client();
                            _dashSwag = client.DashboardGetPatientAppDashboardAuthenticatedAsync().Result;


                            if (_dashSwag.StatusCode== 200) { 
                                //trigger property change 
                                Dash.ParseData(_dashSwag);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _dashSwag = null;
                        //unable to load SW
                    }
                    return _dashSwag;
                }
            
            }
            set
            {
                _dashSwag = value;
            }
        }

        public DashboardViewModel()
        {
            Title = "BADBIR Patient Dashboard";

            DlqiCommand = new Command(OnDlqiClicked);
            PgaCommand = new Command(OnPgaClicked);
            Eq5dCommand = new Command(OnEq5dClicked);
            MedprobsCommand = new Command(OnMedprobsClicked);
            LifestyleCommand = new Command(OnLifestyleClicked);
            CageCommand = new Command(OnCageClicked);
            HaqCommand = new Command(OnHaqClicked);


        }


        public async Task LoadData()
        {
            _dashSwag = null;

            //trigger the getter to load and parse http resp.
            var SC = DashSwag.StatusCode;


            //if(DashSwag.StatusCode== 200)
            //{

                //Dash.LoadData(DashSwag);

            //}
        }



        private async void OnDlqiClicked(object obj)
        {

            var nav = $"{nameof(PQ_Dlqi)}";
            await Shell.Current.GoToAsync(nav);
        }

        private async void OnPgaClicked(object obj)
        {

            var nav = $"{nameof(PQ_Pga)}";
            await Shell.Current.GoToAsync(nav);

        }

        private async void OnEq5dClicked(object obj)
        {
            var nav = $"{nameof(PQ_Eq5d)}";
            await Shell.Current.GoToAsync(nav);
        }

        private async void OnHaqClicked(object obj)
        {
            var nav = $"{nameof(PQ_Haq)}";
            await Shell.Current.GoToAsync(nav);
        }

        private async void OnCageClicked(object obj)
        {
            // Create the page
            var nav = $"{nameof(PQ_Cage)}";
            await Shell.Current.GoToAsync(nav);
        }

        private async void OnMedprobsClicked(object obj)
        {


            var nav = $"{nameof(PQ_MedProbs)}";
            await Shell.Current.GoToAsync(nav);
        }

        private async void OnLifestyleClicked(object obj)
        {
            // Create the page
            var nav = $"{nameof(PQ_Lifestyle)}";
            await Shell.Current.GoToAsync(nav);
        }

    }
}
