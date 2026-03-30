using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class PgaViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }
        public Command CancelQCommand { get; }

        private PatientPgascore pgascore ;

        public PatientPgascore PgaScore { get { return pgascore; } }


        public PgaViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);
            CancelQCommand = new Command(OnCancelClicked);

            pgascore= new PatientPgascore();
            pgascore.Createdbyname = pgascore.Lastupdatedbyname = "Patient via API";
        }

        private async void OnSaveClicked(object obj)
        {
            // Save score
            Client c = new Client();
            try
            {
                var SwagResp = c.DashboardSavePGAScoreAsync(pgascore).Result;
                if (SwagResp.StatusCode == 200)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
                }
            }catch (Exception ex) {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }

        }

        private async void OnCancelClicked(object obj)
        {
            // Confirm user is sure to cancel
            bool answer = await Application.Current.MainPage.DisplayAlert("Exit " + Title, "Are you sure you want to cancel this entry and return to the dashboard? Your answers won't be saved.", "Exit", "Carry on");
            if (answer)
                // Navigate to the dashboard page
                await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
