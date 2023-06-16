using Fitty.Models;
using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutineDetailPage : ContentPage
    {
        RoutineDetailViewModel _viewmodel;
        public RoutineDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new RoutineDetailViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            _viewmodel.Reset();
        }

    }
}