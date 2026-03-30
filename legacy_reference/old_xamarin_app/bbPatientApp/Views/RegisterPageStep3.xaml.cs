using bbPatientAPI;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPageStep3 : ContentPage
	{
        private readonly int reqID;
        private readonly string reqPassCode;
        private readonly string emailAddress;

        public RegisterPageStep3 ()
		{
			InitializeComponent ();
		}
		public RegisterPageStep3(int reqID, string reqPassCode, string emailAddress)
		{

			InitializeComponent ();
            this.reqID = reqID;
            this.reqPassCode = reqPassCode;
            this.emailAddress = emailAddress;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {


            if (Entry_Password.Text.Length < 8)
            {
                ErrorLabel.Text = "Password too short";
                return;
            }else if (! Entry_Password.Text.Any(x => char.IsUpper(x)))
            {
                ErrorLabel.Text = "No upper case letter found in password";
                return;
            }else if(! Entry_Password.Text.Any (x => char.IsLower(x)))
            {
                ErrorLabel.Text = "No lower case letter found in password";
                return;
            }else if (!Entry_Password.Text.Any(x => char.IsNumber(x)))
            {
                ErrorLabel.Text = "No number found in password";
                return;
            }else if(!Entry_Password.Text.Any(x => (x == '@' || x == '!' || x == '$' || x=='£' || x=='%' || x=='*'     )))
            {
                ErrorLabel.Text = "No special character found";
                return;
            }


            RegisterStep3DTO dto = new RegisterStep3DTO();
            dto.HoldingID = reqID;
            dto.HoldingPassCode = reqPassCode;
            dto.Password = Entry_Password.Text;
            //send password

            Client client = new Client();
            try
            {

                //check password validation

                var swagResult= await client.Auth2Registerstep3Async(dto);


                //var swagResult = client.Auth2Registerstep3Async(dto).Result;
                if (swagResult.StatusCode == 200)
                {
                    //account created - for the time being, redirect to login page?

                    //TODO: CODE REUSED FROM LOGIN PAGE Needs remodeling
                    //res.body[token] will have the token to be stored. 
                    //save key etc
                    //TODO COMPLETED
                    await SecureStorage.SetAsync("token", swagResult.BodyProperties["token"]);

                    await DisplayAlert("Account Registered", "Your account is now active. Continuing to dashboard", "OK");

                    
                    var bioAvailable = await CrossFingerprint.Current.IsAvailableAsync();
                    if (bioAvailable)
                    {
                        var bioRequest = await DisplayAlert("Biometric Login", "Would you like to login with biometrics next time?", "Yes", "No");
                        if (bioRequest)
                        {
                            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new Plugin.Fingerprint.Abstractions.AuthenticationRequestConfiguration("Biometric check", "Please confirm your biometrics to save your authentication token."));

                            if (authResult.Authenticated)
                            {
                                await SecureStorage.SetAsync("Autologin", "1");
                                await DisplayAlert("Token Saved", "You will be able to login with biometrics next time you launch the app. To remove this, you can log out at any time.", "Ok");
                            }
                            else
                            {
                                await DisplayAlert("Biometrics Refused", "Unable to confirm biometrics. Login token not saved.", "Ok");
                            }

                        }
                    }

                    try
                    {
                        var nav = $"//Dashboard";
                        await Shell.Current.GoToAsync(nav);
                    }
                    catch (Exception exception)
                    {
                        await DisplayAlert("Error", "Unable to create your account (possible duplicate) - Please contact BADBIR for help", "OK");
                    }


                    //recieve data,
                    //keep user logged in as response will have a jwt

                    //that page automatically redirects to dashboard if user is logged in. 
                    //sign user in automatically and send to login page where they will be asked if they want to stay logged in using biometrics. 

                }
                else
                {
                    Entry_Password.Focus();
                    ErrorLabel.Text = "Password not valid. Please try again with a different password";

                }
            }
            catch (Exception eee)
            {
                Entry_Password.Focus();
                ErrorLabel.Text = "Its likely that you already had an account with us. Please contact BADBIR for further details.";

            }


        }
    }
}