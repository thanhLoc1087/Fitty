using Fitty.Models;
using Fitty.Services;
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
    public partial class HomePage : ContentPage
    {
        HomeViewModel _viewmodel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = new HomeViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ExerciseAPIService.ReadJsonFile();

            var data = await ExerciseService.GetExercises();
            HomeViewModel.DataSource.exercises = data;
            _viewmodel.Name = UserLocal.GetUserFromFile().Name;
        }
    }
}