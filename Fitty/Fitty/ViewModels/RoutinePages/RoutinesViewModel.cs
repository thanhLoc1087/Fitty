using Fitty.Models;
using Fitty.Views;
using Fitty.Views.RoutinePage;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    internal class RoutinesViewModel : BaseViewModel
    {
        public static AddRoutineViewModel addRoutineViewModel;
        public Command Refresh { get; set; }

        public bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing; set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        List<Routine> _routines;
        public List<Routine> Routines
        {
            get { return _routines; }
            set
            {
                SetProperty(ref _routines, value);
            }
        }
        public Command AddCommand { get; set; }
        public RoutinesViewModel()
        {
            addRoutineViewModel = new AddRoutineViewModel();
            AddCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(AddRoutinePage)}");
            });
            Refresh = new Command(async () =>
            {
                IsRefreshing = true;
                IsBusy = true;
                Routines = await RoutineService.GetRoutines();
                IsRefreshing = false;
            });
        }
    }
}
