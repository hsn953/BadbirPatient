using System.ComponentModel;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using bbPatientApp.Views;
using System.Security.Cryptography;
using Xamarin.Essentials;
using Plugin.Fingerprint;
using System.Xml;

namespace bbPatientApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public string VersionText { get; } = $"App Version: v{VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegisterCommand = new Command(OnRegisterClicked);
            SigninUsingSavedCredentials = new Command(OnSigninClicked);
            LogoutSavedCredentials = new Command(OnLogoutClicked);

            //set up values here for visibility? 
            
            var autoLogin = SecureStorage.GetAsync("Autologin").Result;
            var biometricToken = SecureStorage.GetAsync("token").Result;

            if (!String.IsNullOrEmpty(autoLogin) && autoLogin.Equals("1") && !String.IsNullOrEmpty(biometricToken))
            {
                LoginPanelVisible = false;
                BiometricPanelVisible= true;
            }
            else
            {
                LoginPanelVisible= true;
                BiometricPanelVisible= false;
            }
            
        }


        private Boolean _loginPanelVisible;
        public Boolean LoginPanelVisible { 
            get { return _loginPanelVisible; } 
            set { _loginPanelVisible = value; 
                OnPropertyChanged(nameof(LoginPanelVisible)); }     
        }

        private Boolean _biometricPanelVisible;
        public Boolean BiometricPanelVisible { get { return _biometricPanelVisible; } 
            set { _biometricPanelVisible = value; 
                OnPropertyChanged(nameof(BiometricPanelVisible)); } 
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                    ValidateUsername();
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                    ValidatePassword();
                }
            }
        }

        private bool _isUsernameValid;
        public bool IsUsernameValid
        {
            get { return _isUsernameValid; }
            set
            {
                if (_isUsernameValid != value)
                {
                    _isUsernameValid = value;
                    OnPropertyChanged(nameof(IsUsernameValid));
                    ValidateCredentials();
                }
            }
        }

        protected string ErrorMessage = "Password iincomplete";

        private bool _isPasswordValid;
        public bool IsPasswordValid
        {
            get { return _isPasswordValid; }
            set
            {
                if (_isPasswordValid != value)
                {
                    _isPasswordValid = value;
                    OnPropertyChanged(nameof(IsPasswordValid));
                    ValidateCredentials();
                }
            }
        }

        private bool _areCredentialsValid;
        public bool AreCredentialsValid
        {
            get { return _areCredentialsValid; }
            set
            {
                if (_areCredentialsValid != value)
                {
                    _areCredentialsValid = value;
                    OnPropertyChanged(nameof(AreCredentialsValid));
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public ICommand SigninUsingSavedCredentials { get; }

        public ICommand LogoutSavedCredentials { get; }

        private void ValidateUsername()
        {
            // Validate email format
            if (!string.IsNullOrEmpty(Username))
            {
                try
                {
                    var address = new System.Net.Mail.MailAddress(Username);
                    IsUsernameValid = address.Address == Username;
                }
                catch (FormatException)
                {
                    IsUsernameValid = false;

                }
            }
            else
            {
                IsUsernameValid = false;
            }
        }

        private void ValidatePassword()
        {
            // Validate password format
            if (!string.IsNullOrEmpty(Password))
            {
                var hasUpperCase = false;
                var hasDigit = false;
                var hasLowerCase = false;
                foreach (var c in Password)
                {

                    if (char.IsLower(c))
                    {
                        hasLowerCase = true;
                    }

                    if (char.IsUpper(c))
                    {
                        hasUpperCase = true;
                    }
                    if (char.IsDigit(c))
                    {
                        hasDigit = true;
                    }
                }
                IsPasswordValid = Password.Length >= 8 && hasUpperCase && hasDigit && hasLowerCase;
            }
            else
            {
                IsPasswordValid = false;
            }
        }

        private void ValidateCredentials()
        {
            // Check if both username and password are valid
            AreCredentialsValid = IsUsernameValid && IsPasswordValid;
        }

        private async void OnLoginClicked()
        {

            // Check if the username and password are valid
            if (AreCredentialsValid)
            {


                bbPatientAPI.Client client = new bbPatientAPI.Client();


                bbPatientAPI.UserDTO userDTO = new bbPatientAPI.UserDTO();
                userDTO.Email = Username;
                userDTO.Password = Password;

                
                try
                {
                    SecureStorage.Remove("token");
                    SecureStorage.Remove("Autologin");



                    IsBusy= true;
                    var res = await client.Auth2LoginAsync(userDTO);



                    //res.body[token] will have the token to be stored. 
                    await SecureStorage.SetAsync("token", res.BodyProperties["token"]);
                    //save key etc


                    var bioAvailable = await CrossFingerprint.Current.IsAvailableAsync();
                    if (bioAvailable)
                    {
                        var bioRequest = await Application.Current.MainPage.DisplayAlert("Biometric Login", "Would you like to login with biometrics next time?", "Yes", "No");
                        if (bioRequest)
                        {
                            var authResult = await CrossFingerprint.Current.AuthenticateAsync(new Plugin.Fingerprint.Abstractions.AuthenticationRequestConfiguration("Biometric check", "Please confirm your biometrics to save your authentication token."));

                            if (authResult.Authenticated)
                            {
                                await SecureStorage.SetAsync("Autologin", "1");
                                await Application.Current.MainPage.DisplayAlert("Token Saved", "You will be able to login with biometrics next time you launch the app. To remove this, you can log out at any time.", "Ok");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Biometrics Refused", "Unable to confirm biometrics. Login token not saved.", "Ok");

                            }

                        }
                    }

                    try
                    {
                        //var dashboardSwag = client.DashboardGetPatientAppDashboardAuthenticatedAsync().Result;
                        //Dashboard dash = new Dashboard(dashboardSwag);
                        Username = "";
                        Password = "";


                        var nav = $"//Dashboard";
                        await Shell.Current.GoToAsync(nav);


                    }
                    catch (Exception exception)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Unable to obtain your form status from the server", "OK");
                    }
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Unable to validate your username or password", "OK");
                    //have a timeout 

                }
            }
            else
            {
                // Display an error message
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
            }
            IsBusy = false;

        }

        private async void OnRegisterClicked()
        {
            // Create the page
            var nav = $"{nameof(RegisterPage1)}";
            // Handle register button click
            // ...
            await Shell.Current.GoToAsync(nav);
        }


        private async void OnSigninClicked()
        {
            var autoLogin = await SecureStorage.GetAsync("Autologin");
            var biometricToken = await SecureStorage.GetAsync("token");
            //Saved auth
            if (!string.IsNullOrEmpty(autoLogin) && autoLogin.Equals("1"))
            {


                var ability = await CrossFingerprint.Current.IsAvailableAsync(true);
                if (!ability)
                {
                    await Application.Current.MainPage.DisplayAlert("Login using Biometrics", "Error occured trying to access your device's biometrics. Please login using email and password.", "ok");
                    //logout users
                    OnLogoutClicked();
                }
                else
                {
                    var bioCheck = await CrossFingerprint.Current.AuthenticateAsync(new Plugin.Fingerprint.Abstractions.AuthenticationRequestConfiguration("Biometric check", "Please use biometrics to login."));
                    if (bioCheck.Authenticated)
                    {

                        //check if current login is okay. 
                       //think about removing this check at all.
                        if (biometricToken != null)
                        {
                            bbPatientAPI.Client client = new bbPatientAPI.Client();

                            IsBusy=true;
                            var checkSwag = await client.Auth2CheckTokenAsync();

                            //do this in the result?
                            if (checkSwag.StatusCode == 200)
                            {
                                //verified
                                var nav = $"//Dashboard";
                                await Shell.Current.GoToAsync(nav);
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Login using Biometrics", "Authentication Expired. Please login using email and password.", "ok");
                                //logout users
                                OnLogoutClicked();


                            }
                            IsBusy= false;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Login using Biometrics", "Authentication Token Not Found. Please login using email and password.", "ok");
                            //logout users
                            OnLogoutClicked();

                        }



                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Login using Biometrics", "Authentication Failed. Please login using email and password.", "ok");
                        //logout users
                        OnLogoutClicked();

                    }
                }
            }
        }

        private async void OnLogoutClicked()
        {

            //remove token and islogged

            SecureStorage.Remove("token");
            SecureStorage.Remove("Autologin");

            LoginPanelVisible = true;
            BiometricPanelVisible = false;

            //assuming the page will trigger OnAppearing again. The panel visibliyt inst bound but can be. 
            // Create the page

            //var nav = $"//{nameof(LoginPage)}";
            // Handle register button click
            // ...
            //await Shell.Current.GoToAsync(nav);
        }



    }


}