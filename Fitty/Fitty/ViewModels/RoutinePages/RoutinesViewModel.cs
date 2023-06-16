using Fitty.Models;
using Fitty.Views;
using Fitty.Views.RoutinePage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    internal class RoutinesViewModel : BaseViewModel
    {
        public static AddRoutineViewModel addRoutineViewModel;
        public Command Refresh { get; set; }
        public Command<Routine> ItemTapped { get; }
        public Command<Routine> DeleteRoutine { get; }
        public Command<Routine> EditRoutine { get; }

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
            ItemTapped = new Command<Routine>(OnItemSelected);
            DeleteRoutine = new Command<Routine>(HandleDeleteRoutine);
            EditRoutine = new Command<Routine>(HandleEditRoutine);
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
        private async void OnItemSelected(Routine item)
        {
            if (item == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RoutineDetailPage)}?{nameof(RoutineDetailViewModel.Id)}={item.Id}");
        }
        private async void HandleDeleteRoutine(Routine item)
        {
            Debug.WriteLine("made it to delete");
            if (item == null)
                return;
            item.Details.ForEach(async d =>
            {
                await RoutineDetailService.RemoveRoutineDetail(d.Id);
            });
            await RoutineService.RemoveRoutine(item.Id);
            Routines = await RoutineService.GetRoutines();
            Debug.WriteLine("deleted");
        }
        private async void HandleEditRoutine(Routine item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RoutineDetailPage)}?{nameof(RoutineDetailViewModel.Id)}={item.Id}");
        }
    }
}
