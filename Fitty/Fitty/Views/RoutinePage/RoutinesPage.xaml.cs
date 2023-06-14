using Fitty.Models;
using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutinesPage : ContentPage
    {
        public RoutinesPage()
        {
            InitializeComponent();
            BindingContext = new RoutinesViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var _container = BindingContext as RoutinesViewModel;

            var data = await RoutineService.GetRoutines();
            _container.Routines = data;
        }
    }
}