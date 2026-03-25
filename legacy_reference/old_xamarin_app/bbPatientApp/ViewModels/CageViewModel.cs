using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class CageViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }

        private PatientCage cageDAO;

        public PatientCage CageDAO { get { return cageDAO; } }
        
        public CageViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);
            cageDAO = new PatientCage();
            cageDAO.Createdbyname = cageDAO.Lastupdatedbyname = "Patient via API";
            
        }

        private async void OnSaveClicked(object obj)
        {
       
            //Save
            Client c = new Client();

            try
            {
                var SwagResp = c.DashboardSaveCageAsync(cageDAO).Result;
                if (SwagResp.StatusCode == 200)
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }

            //refresh dashboard 

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
