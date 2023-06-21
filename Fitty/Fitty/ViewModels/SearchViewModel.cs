using Fitty.Models;
using Fitty.Services;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class SearchViewModel : BaseViewModel
    {
        public Command<Exercise> ItemTapped { get; }
        public bool _isRefreshing;
        public bool IsRefreshing { get => _isRefreshing; set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        public Command Refresh { get; set; }
        List<Exercise> _filteredExercises;
        public List<Exercise> FilteredExercises
        {
            get => _filteredExercises;
            set
            {
                SetProperty(ref _filteredExercises, value);
            } 
        }

        public SearchViewModel() 
        {
            ItemTapped = new Command<Exercise>(OnItemSelected);
            Refresh = new Command(async () =>
            {
                IsRefreshing = true;
                IsBusy = true;
                FilteredExercises = await ExerciseService.GetExercises();
                IsRefreshing = false;
            });
            IsBusy = false;
        }

        private async void OnItemSelected(Exercise item)
        {
            if (item == null)
                return;
            if (item.UserCreated)
            {
                await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ExerciseDetailViewModel.Id)}={item.Id}");
            } else { 
                await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ExerciseDetailViewModel.Name)}={item.Name}");
            }
        }
    }
}
