using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage1 : ContentPage
	{
		public RegisterPage1 ()
		{
			Title = "Activate account";
			InitializeComponent ();
		}

        private async void RegisterStep1Button_Clicked(object sender, EventArgs e)
        {
			
			await Shell.Current.GoToAsync($"{nameof(RegisterPageStep1)}");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PrivacyStack.IsVisible = true;

        }
    }
}