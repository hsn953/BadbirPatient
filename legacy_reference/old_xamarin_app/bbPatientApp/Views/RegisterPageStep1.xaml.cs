using bbPatientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPageStep1 : ContentPage
    {
        public RegisterPageStep1()
        {
            InitializeComponent();
            //min age = 5 years
            DateOfBirth.MaximumDate= DateTime.Now.AddYears(-5);
            DateOfBirth.MinimumDate = DateTime.Now.AddYears(-99);
            



        }

        private async void RegisterStep2Button_Clicked(object sender, EventArgs e)
        {
            //check info, if found, save ID etc and go to step 2

            bbPatientAPI.Client client= new bbPatientAPI.Client();

            RegisterStep1DTO step1DTO = new RegisterStep1DTO();

            step1DTO.FirstInitial = FirstInitial.Text;
            step1DTO.LastInitial = LastInitial.Text;
            step1DTO.Dob = DateOfBirth.Date; 
            
            if(rb1.IsChecked) step1DTO.WhichID = 3; 
            else if(rb2.IsChecked) step1DTO.WhichID= 2; 
            else if(rb3.IsChecked)  step1DTO.WhichID= 1;

            step1DTO.BadbirStudyNo = step1DTO.Chi = step1DTO.NhsNo = PatientNumber.Text;
            try { 
            var swagReg1 = await client.Auth2Registerstep1Async(step1DTO);

                if(swagReg1.StatusCode == 200)
                {
                    //obtain values, set them in secure storage
                    var reqID = Int32.Parse(swagReg1.BodyProperties["requestID"]);
                    var reqPassCode = swagReg1.BodyProperties["passcode"];


                    //"requestID": 13,
                    //"passcode": "04896039",
                    //"message": "details matched."

                    await Shell.Current.Navigation.PushAsync(new RegisterPageStep2(reqID, reqPassCode));

                }
                else
                {
                    await DisplayAlert("Details do not match", "The details you have entered do not match with what we have for you. Please try again or contact BADBIR for help with registration.", "OK");
                }
            }
            catch (Exception ecx)
            {
                await DisplayAlert("Details do not match", "The details you have entered do not match with what we have for you. Please try again or contact BADBIR for help with registration.", "OK");

            }
        }
    }
}