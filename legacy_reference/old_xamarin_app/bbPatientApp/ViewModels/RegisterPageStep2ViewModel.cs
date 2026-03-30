using System.ComponentModel;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using bbPatientApp.Views;
using System.Security.Cryptography;
using Xamarin.Essentials;

namespace bbPatientApp.ViewModels
{
    public class RegisterPageStep2ViewModel : BaseViewModel
    {
        public RegisterPageStep2ViewModel()
        {
           // LoginCommand = new Command(OnLoginClicked);
          //  RegisterCommand = new Command(OnRegisterClicked);

              
            
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

        protected string ErrorMessage = "";



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

        //public ICommand LoginCommand { get; }
       // public ICommand RegisterCommand { get; }

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


        private void ValidateCredentials()
        {
            // Check if both username and password are valid
            AreCredentialsValid = IsUsernameValid ;
        }

        private async void OnLoginClicked()
        {
            
            // Check if the username and password are valid
            //if (AreCredentialsValid)
            
        }

 



        public new event PropertyChangedEventHandler PropertyChanged;

        private new void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}