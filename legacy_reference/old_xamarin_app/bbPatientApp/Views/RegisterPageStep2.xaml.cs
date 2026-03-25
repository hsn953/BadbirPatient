using bbPatientAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPageStep2 : ContentPage
    {

        protected int reqID;
        protected string reqPassCode;

        private string _emailAddress;

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }


        public RegisterPageStep2()
        {
            InitializeComponent();
        }
        public RegisterPageStep2(int reqID, string reqPassCode)
        {
            InitializeComponent();

            this.reqID= reqID;  
            this.reqPassCode= reqPassCode;


        }

        private async void SendCodeButton_Clicked(object sender, EventArgs e)
        {
            if (reqID > 0 && reqPassCode != null)
            {
                if (Entry_Email.Text.Length > 0)
                {
                    RegisterStep2DTO dto = new RegisterStep2DTO();
                    dto.EmailAddress = Entry_Email.Text;
                    dto.HoldingID = reqID;
                    dto.HoldingPassCode = reqPassCode;
                    Client client = new Client();
                    try
                    {
                        var swagResult = await client.Auth2Registerstep2Async(dto);
                        if (swagResult.StatusCode == 200)
                        {
                            EmailLabel.Text = $"Verification code has been sent to {EmailAddress}. Please check your email and enter the code below to verify email. It may take a few minutes to arrive but it is valid till midnight.";
                            //result successful
                            SendCodeStack.IsVisible = false;
                            CodeSentStack.IsVisible = true;
                            Entry_Email.IsEnabled = false;
                        }
                    }
                    catch
                    {
                        await DisplayAlert("Error while sending code", "Unable to send a code to this email address", "OK");
                    }

                }
                else
                {
                    Entry_Email.Text = "";
                    Entry_Email.Focus();
                }
            }
            else
            {
                await Shell.Current.Navigation.PopAsync();
                await DisplayAlert("Personal Information Not Verified", "Please start at step 1 to verify personal information.", "OK");
            }

        }

        private async void VerifyCodeButton_Clicked(object sender, EventArgs e)
        {
            if (reqID >0 && reqPassCode != null)
            {
                if (Entry_Email.Text.Length > 0 && Entry_ValidationCode.Text.Length==6)
                {
                    RegisterStep2DTO dto = new RegisterStep2DTO();
                    dto.EmailAddress = Entry_Email.Text;
                    dto.HoldingID = reqID;
                    dto.HoldingPassCode = reqPassCode;
                    dto.EmailVerificationCode = Entry_ValidationCode.Text;
                    Client client = new Client();
                    try
                    {
                        var swagResult = await client.Auth2Registerstep2confirmAsync(dto);
                        if (swagResult.StatusCode == 200)
                        {
                            EmailLabel.Text = $"Verification code has been sent to {dto.EmailAddress}. Please check your email and enter the code below to verify email. It may take a few minutes to arrive but it is valid till midnight.";
                            //result successful
                            SendCodeStack.IsVisible = false;
                            CodeSentStack.IsVisible= false;
                            EmailVerifiedStack.IsVisible = true;
                            Entry_Email.IsEnabled = false;
                            Entry_ValidationCode.IsEnabled = false;
                        }
                    }
                    catch
                    {
                        Entry_ValidationCode.Focus();
                        await DisplayAlert("Code not verified", "Unable to verify this code", "OK");
                    }

                }
                else
                {
                    Entry_ValidationCode.Focus();
                    await DisplayAlert("6 digit code needed", "Please provide a six digit code", "OK");
                }
            }
            else
            {
                await Shell.Current.Navigation.PopAsync();
                await DisplayAlert("Personal Information Not Verified", "Please start at step 1 to verify personal information.", "OK");
            }


        }


        private async void Proceed_Clicked(object sender, EventArgs e)
        {


            await Shell.Current.Navigation.PushAsync(new RegisterPageStep3(reqID, reqPassCode, EmailAddress));
        }
    }
}