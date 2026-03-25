using bbPatientApp.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Resources;
namespace bbPatientApp
{
    public partial class App : Application
    {

        public App()
        {
            
            


            
            InitializeComponent();
            object apiKey;
            if (Resources.TryGetValue("SFAPIKey", out apiKey))
            {

                //Register Syncfusion license
                Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(apiKey.ToString());

            }

            MainPage = new AppShell();
        }


        /*protected override async void OnStart()
        {
            //nothing needs doing onstart as first page is loginpage based on shell content and thats the page that checks logged in status. 


        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }*/
    }
}
