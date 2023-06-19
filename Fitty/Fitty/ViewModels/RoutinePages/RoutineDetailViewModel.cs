using Fitty.Models;
using Fitty.Views;
using Fitty.Views.RoutinePage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class RoutineDetailViewModel:BaseViewModel
    {
        public RoutineDetailViewModel() {
            StartCommand = new Command<int>(OnItemSelected);
            EditCommand = new Command<int>(HandleEditRoutine);

            Refresh = new Command(() =>
            {
                IsRefreshing = true;
                IsBusy = true;
                try
                {
                    var temp = Details;
                    Details.Clear();
                    Details = temp;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Failed to Load Routine");
                }
                IsRefreshing = false;
                IsBusy = false;
            });
        }
        Routine Routine { get; set; }
        private int id;
        private string name;
        private int totalDuration;
        private int totalExercises;
        private int numberOfSet;
        private ObservableCollection<RoutineDetail> details;
        public Command<int> EditCommand { get; set; }
        public bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing; set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        public Command Refresh { get; set; }
        public Command<int> StartCommand { get; }

        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                LoadItemId(value);
            }
        }
        public string Name
        { get => name; set { SetProperty(ref name, value); } }
        public int TotalDuration
        { get => totalDuration; set { SetProperty(ref totalDuration, value); } }
        public int TotalExercises
        { get => totalExercises; set { SetProperty(ref totalExercises, value); } }

        public int NumberOfSet
        { get => numberOfSet; set { SetProperty(ref numberOfSet, value); } }
        public ObservableCollection<RoutineDetail> Details
        { get => details; set { SetProperty(ref details, value); } }


        public async void LoadItemId(int itemId)
        {
            try
            {
                Routine = await RoutineService.GetRoutine(itemId);
                Reset();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Routine");
            }
        }
        private async void OnItemSelected(int itemId)
        {
            if (itemId <= 0)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RoutineExePage)}?{nameof(RoutineExeViewModel.Id)}={itemId}");
        }
        public void Reset()
        {
            Name = Routine.Name;
            TotalDuration = Routine.TotalDuration;
            TotalExercises = Routine.TotalExercises;
            NumberOfSet = Routine.NumberOfSet;
            Details = new ObservableCollection<RoutineDetail>(Routine.Details);
        }
        private async void HandleEditRoutine(int itemID)
        {
            if (itemID <= 0)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(AddRoutinePage)}?{nameof(AddRoutineViewModel.Id)}={itemID}");
        }
    }
}
