using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class LifestyleViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }
        public Command CancelQCommand { get; }

        private PatientLifestyle lifestyleDAO;

        public PatientLifestyle LifestyleDAO { get { return lifestyleDAO; } }

        public LifestyleViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);

            lifestyleDAO = new PatientLifestyle();
            lifestyleDAO.Createdbyname = lifestyleDAO.Lastupdatedbyname = "Patient via API";
        }

        private async void OnSaveClicked(object obj)
        {
            // Always return to the dashboard to show completed questionnaire
            //Save
            Client c = new Client();

            try
            {
                var SwagResp = c.DashboardSaveLifestyleAsync(lifestyleDAO).Result;
                if (SwagResp.StatusCode == 200)
                    await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }

        }

    }
}
