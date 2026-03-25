using bbPatientApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bbPatientApp.Models;
using Syncfusion.SfRangeSlider.XForms;

namespace bbPatientApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PQ_Eq5d : ContentPage
    {

        public PQ_Eq5d()
        {
            InitializeComponent();
            
            //BindingContext = _viewModel = new DashboardViewModel();
            // Allows us to use binding Commands

        }

        private void rangeSlider_ValueChanging(object sender, Syncfusion.SfRangeSlider.XForms.ValueEventArgs e)
        {
            SfRangeSlider slider = sender as SfRangeSlider;
            Label sliderValue = slider.Parent.FindByName("SliderValue") as Label;
            sliderValue.Text = $"How do you feel today:{Math.Round(e.Value, 0).ToString()}";
        }

    }
}