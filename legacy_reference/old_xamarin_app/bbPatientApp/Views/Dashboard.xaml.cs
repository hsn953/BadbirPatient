using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bbPatientApp.ViewModels;
using Xamarin.Forms.Shapes;
using bbPatientAPI;
using Xamarin.Essentials;

namespace bbPatientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        //DashboardViewModel _viewModel;

        public Dashboard()
        {
            InitializeComponent();

            //BindingContext = _viewModel = new DashboardViewModel();
            // Allows us to use binding Commands

        }

        //not a
        private async void DashboardPage_Appearing(object sender, EventArgs e)
        {
            //Shell.Current.IsBusy = true;

            DashboardViewModel _viewModel = BindingContext as DashboardViewModel;
            if(_viewModel != null) { 

                //force refresh
                await _viewModel.LoadData();

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Internal Error 1", "Something unexpected happened. Please contact BADBIR if problem persists..", "ok");
            }

            //Shell.Current.IsBusy = false;

        }


    }
}