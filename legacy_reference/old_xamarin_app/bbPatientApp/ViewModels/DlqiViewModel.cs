using bbPatientAPI;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class DlqiViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }

        private PatientDlqi dlqiDAO;

        public PatientDlqi DLQIDAO { get { return dlqiDAO; }}


            
        public DlqiViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);
            dlqiDAO = new PatientDlqi();
            dlqiDAO.Createdbyname = dlqiDAO.Lastupdatedbyname = "Patient via API";

        }

        private async void OnSaveClicked(object obj)
        {
            // Calculate score & save Q
            calculateTotal();
            // Always return to the dashboard to show completed questionnaire
            //Save
            Client c = new Client();

            try
            {
                var SwagResp = c.DashboardSaveDLQIAsync(dlqiDAO).Result;
                if(SwagResp.StatusCode==200)
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Application.Current.MainPage.DisplayAlert("Score Saved", $"DLQI Score: {dlqiDAO.TotalScore}", "Ok");
            }
            catch (Exception ex) 
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }

        }


        private void calculateTotal()
        {
            dlqiDAO.TotalScore = 0;
            if(dlqiDAO.ItchsoreScore.HasValue &&  dlqiDAO.ItchsoreScore<3 ) dlqiDAO.TotalScore += 3 - dlqiDAO.ItchsoreScore;
            if (dlqiDAO.EmbscScore.HasValue && dlqiDAO.EmbscScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.EmbscScore;
            if (dlqiDAO.ShophgScore.HasValue && dlqiDAO.ShophgScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.ShophgScore;
            if (dlqiDAO.ClothesScore.HasValue && dlqiDAO.ClothesScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.ClothesScore;
            if (dlqiDAO.SocleisScore.HasValue && dlqiDAO.SocleisScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.SocleisScore;
            if (dlqiDAO.SportScore.HasValue && dlqiDAO.SportScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.SportScore;
            if (dlqiDAO.WorkstudScore.HasValue && dlqiDAO.WorkstudScore == 5) dlqiDAO.TotalScore += 3 ;
            else if(dlqiDAO.WorkstudScore.HasValue && dlqiDAO.WorkstudScore==6 && dlqiDAO.WorkstudnoScore.HasValue ) dlqiDAO.TotalScore += 3 - dlqiDAO.WorkstudnoScore;
            if (dlqiDAO.PartcrfScore.HasValue && dlqiDAO.PartcrfScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.PartcrfScore;
            if (dlqiDAO.SexdifScore.HasValue && dlqiDAO.SexdifScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.SexdifScore;
            if (dlqiDAO.TreatmentScore.HasValue && dlqiDAO.TreatmentScore < 3) dlqiDAO.TotalScore += 3 - dlqiDAO.TreatmentScore;


        }

        
    }
}
