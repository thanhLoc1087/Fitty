using Fitty.Models;
using Fitty.Views;
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
        private int id;
        private string name;
        private int totalDuration;
        private int totalExercises;
        private int numberOfSet;
        private ObservableCollection<RoutineDetail> details;
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
                var item = await RoutineService.GetRoutine(itemId);
                Name = item.Name;
                TotalDuration = item.TotalDuration;
                TotalExercises = item.TotalExercises;
                NumberOfSet = item.NumberOfSet;
                Details = new ObservableCollection<RoutineDetail>(item.Details);
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
    }
}
