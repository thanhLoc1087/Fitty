using Fitty.Services;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    class HomeViewModel : BaseViewModel
    {
        string _name;
        static public DataSource DataSource { get; set; }

        public string Name { get => _name; set
            {
                SetProperty(ref _name, value);
            }
        }
        public Command HydrationCommand { get; set; }
        public Command CalculatorCommand { get; set; }
        public Command ExercisesCommand { get; set; }
        public HomeViewModel()
        {
            DataSource = new DataSource();
            HydrationCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(HydrationAlarm)}");
            });
            CalculatorCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(CalculatorPage)}");
            });
            ExercisesCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(MusclesPage)}");
            });
        }
    }
}
