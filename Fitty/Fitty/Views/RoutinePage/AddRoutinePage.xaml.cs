using Fitty.Models;
using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views.RoutinePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddRoutinePage : ContentPage
    {
        public AddRoutinePage()
        {
            InitializeComponent();
            BindingContext = RoutinesViewModel.addRoutineViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var _container = BindingContext as AddRoutineViewModel;
            //RoutineDetailsList.ItemsSource = _container.Routine.Details;
            Debug.WriteLine("on appearing");
            Debug.WriteLine(_container.Routine.Details.Count());
        }
    }
}