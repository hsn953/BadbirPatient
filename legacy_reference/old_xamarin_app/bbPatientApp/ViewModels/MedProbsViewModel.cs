using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class MedProbsViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }

        private PatientMedProblemFup medprobDAO;

        public PatientMedProblemFup MedProbDAO { get { return medprobDAO; } }

        public MedProbsViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);
            medprobDAO = new PatientMedProblemFup();
            medprobDAO.Createdbyname = medprobDAO.Lastupdatedbyname = "Patient via API";
        }

        private async void OnSaveClicked(object obj)
        {
            // Save score
            Client c = new Client();


            try
            {
                var SwagResp = c.DashboardSaveMedProblemAsync(medprobDAO).Result;
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
