using bbPatientAPI;
using bbPatientApp.Models;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace bbPatientApp.ViewModels
{
    public class Eq5dViewModel : BaseViewModel
    {
        public Command SaveQCommand { get; }

        public ObservableCollection<Eq5dQuestion> EQ5DQuestions { get; set; }


        private PatientEuroQol euroQol;
        
        public Eq5dViewModel()
        {
            SaveQCommand = new Command(OnSaveClicked);
            euroQol = new PatientEuroQol();
            euroQol.Createdbyname = euroQol.Lastupdatedbyname = "Patient via API";

            PopulateQuestions();
 
        }

        protected void PopulateQuestions()
        {
            EQ5DQuestions = new ObservableCollection<Eq5dQuestion>
            {
                new Eq5dQuestion
                {
                    TopLabel = "Please select the ONE box that best desribes your health TODAY",
                    QuestionText = "1. MOBILITY",
                    QuestionType = "Standard",
                    RbGroupName="RB1",
                    Answer0 = "I have no problems in walking about",
                    Answer1 = "I have some problems in walking about",
                    Answer2 = "I am confined to bed"
                },
                new Eq5dQuestion
                {
                    TopLabel = "Please select the ONE box that best desribes your health TODAY",
                    QuestionText = "2. SELF-CARE",
                    QuestionType = "Standard",
                    RbGroupName="RB2",

                    Answer0 = "I have no problems with self care",
                    Answer1 = "I have some problems with washing or dressing myself",
                    Answer2 = "I am unable to wash or dress myself"
                },
                new Eq5dQuestion
                {
                    TopLabel = "Please select the ONE box that best desribes your health TODAY",
                    QuestionText = "3. USUAL ACTIVITIES (e.g. work, study, housework or family/leisure activities)",
                    QuestionType = "Standard",
                    RbGroupName="RB3",
                    Answer0 = "I have no problems performing my usual activities",
                    Answer1 = "I have some problems performing my usual activities",
                    Answer2 = "I am unable to perform my usual activities"
                },
                new Eq5dQuestion
                {
                    TopLabel = "Please select the ONE box that best desribes your health TODAY",
                    QuestionText = "4. PAIN/DISCOMFORT",
                    QuestionType = "Standard",
                    RbGroupName="RB4",
                    Answer0 = "I have no pain or discomfort",
                    Answer1 = "I have moderate pain or discomfort",
                    Answer2 = "I have extreme pain or discomfort"
                },
                new Eq5dQuestion
                {
                    TopLabel = "Please select the ONE box that best desribes your health TODAY",
                    QuestionText = "5. ANXIETY/DEPRESSION",
                    QuestionType = "Standard",
                    RbGroupName="RB5",
                    Answer0 = "I am not anxious or depressed",
                    Answer1 = "I am moderately anxious or depressed",
                    Answer2 = "I am extremely anxious or depressed"
                },
                new Eq5dQuestion
                {
                    TopLabel = "We would like to know how good or bad your health is TODAY",
                    QuestionText = "Fixed text can be added to the template. ",
                    QuestionType = "Scale"
                },
                new Eq5dQuestion
                {
                    TopLabel = "Additional question related to your health",
                    QuestionText = "Compared with my general level of health over the past 12 months, my health state today is:",
                    QuestionType = "Standard",
                    RbGroupName="RB7",
                    Answer0 = "Better",
                    Answer1 = "Much the same",
                    Answer2 = "Worse"
                }
            };
        }


        private void FillDAO()
        {
            euroQol.Mobility = EQ5DQuestions[0].AnswerValue;
            euroQol.Selfcare = EQ5DQuestions[1].AnswerValue;
            euroQol.Usualacts = EQ5DQuestions[2].AnswerValue;
            euroQol.Paindisc = EQ5DQuestions[3].AnswerValue;    
            euroQol.Anxdepr = EQ5DQuestions[4].AnswerValue;
            euroQol.Howyoufeel = EQ5DQuestions[5].AnswerValue;
            euroQol.Comphealth = EQ5DQuestions[6].AnswerValue;
        }

        private async void OnSaveClicked(object obj)
        {           // Save score

            FillDAO();

            Client c = new Client();
            try
            {
                var SwagResp = c.DashboardSaveEuroqolAsync (euroQol).Result;
                if (SwagResp.StatusCode == 200)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to save. Please try again later", "Ok");
            }
        }


    }


}
