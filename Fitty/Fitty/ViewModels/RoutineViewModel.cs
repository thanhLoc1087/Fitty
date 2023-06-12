using Fitty.Models;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    internal class RoutineViewModel:BaseViewModel
    {
        List<Routine> _routines;
        public List<Routine> Routines { get { return _routines; } set
            {
                SetProperty(ref _routines, value);
            }
        }
        Command AddCommand { get; set; }
        RoutineViewModel() {
            AddCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(AddExercisePage)}");
            });
        }
    }
}
