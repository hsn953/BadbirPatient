using bbPatientApp.ViewModels;
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
    public partial class PQ_Cage : ContentPage
    {
        //CageViewModel _viewModel;
        public PQ_Cage()
        {
            InitializeComponent();

            // Allows us to use binding Commands
            //BindingContext = _viewModel = new CageViewModel();
        }

    
    }
}