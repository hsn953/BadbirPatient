using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class HaqViewModel : BaseViewModel
    {


        private PatientHaq haqDAO;

        public PatientHaq HAQDAO { get { return haqDAO; } }

        public Command SaveQCommand { get; }
        
        public HaqViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);

            haqDAO = new PatientHaq();
            haqDAO.Createdbyname = haqDAO.Lastupdatedbyname = "Patient via API";
        }

        private async void OnSaveClicked(object obj)
        {
            // Calculate score & save Q
            //Save
            Client c = new Client();


            try
            {
                var SwagResp = c.DashboardSaveHAQAsync(haqDAO).Result;
                if (SwagResp.StatusCode == 200)
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }
        }



    }
}
