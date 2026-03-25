using bbPatientAPI;
using bbPatientApp.ViewModels;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace bbPatientApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {

 

        public AppShell()
        {
            InitializeComponent();


            // Register page routes
            //Routing.RegisterRoute(nameof(Dashboard), typeof(Dashboard));

            Routing.RegisterRoute(nameof(PQ_Dlqi), typeof(PQ_Dlqi));
            Routing.RegisterRoute(nameof(PQ_Pga), typeof(PQ_Pga));
            Routing.RegisterRoute(nameof(PQ_Eq5d), typeof(PQ_Eq5d));
            Routing.RegisterRoute(nameof(PQ_MedProbs), typeof(PQ_MedProbs));
            Routing.RegisterRoute(nameof(PQ_Lifestyle), typeof(PQ_Lifestyle));  
            Routing.RegisterRoute(nameof(PQ_Cage), typeof(PQ_Cage));
            Routing.RegisterRoute(nameof(PQ_Haq), typeof(PQ_Haq));


            Routing.RegisterRoute(nameof(RegisterPage1), typeof(RegisterPage1));
            Routing.RegisterRoute(nameof(RegisterPageStep1), typeof(RegisterPageStep1));
            Routing.RegisterRoute(nameof(RegisterPageStep2), typeof(RegisterPageStep2));
            Routing.RegisterRoute(nameof(RegisterPageStep3), typeof(RegisterPageStep3));
            
        }


        


        private async void OnMenuItemClicked(object sender, EventArgs e)
        {

            //whats that doing?
            var route = $"{nameof(LoginPage)}";
            await Shell.Current.GoToAsync(route);
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            //remove token and islogged

            SecureStorage.Remove("token");
            SecureStorage.Remove("Autologin");
            await Shell.Current.GoToAsync("//LoginPage");

        }

        

    }
}
