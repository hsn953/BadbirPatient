using bbPatientAPI;
using bbPatientApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbPatientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PQ_Dlqi : ContentPage
    {
       // DlqiViewModel _viewModel;


        public PQ_Dlqi()
        {
            InitializeComponent();


            // Allows us to use binding Commands
         //   BindingContext = _viewModel = new DlqiViewModel();

        }


        private void Q7RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //toggle visibility of q7a based on this.
            RadioButton rb = sender as RadioButton;
            if(rb.Value.Equals("10") || rb.Value.Equals("5"))
            {
                RBGQA7.IsVisible = false;
            }
            else
            {
                RBGQA7.IsVisible=true;
            }
        }
    }
}