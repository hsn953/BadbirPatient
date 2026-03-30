using bbPatientApp.ViewModels;
using bbPatientApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp.Views
{
    public partial class PQ_Pga : ContentPage
    {
        PgaViewModel _viewModel;
        public PQ_Pga()
        {
            InitializeComponent();

            // Allows us to use binding Commands
            BindingContext = _viewModel = new PgaViewModel();
        }

        private async void Button_CancelAndReturn_Click(object sender, EventArgs e)
        {
            // Confirm user is sure to cancel
            bool answer = await DisplayAlert("Exit questionnaire", "Are you sure you want to cancel this entry and return to the dashboard? Your answers won't be saved.", "Exit", "Carry on");
            if (answer)
                // Navigate to the dashboard page
                await Navigation.PopModalAsync();
        }
    }
}