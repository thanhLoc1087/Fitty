using Fitty.Models;
using Fitty.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class SearchViewModel : BaseViewModel
    {
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
        public ExerciseAPIService exerciseAPIService;

        public SearchViewModel() 
        {
            exerciseAPIService = new ExerciseAPIService();
            FilteredExercises = exerciseAPIService.ReadJsonFile();
            Refresh = new Command(async () =>
            {
                IsRefreshing = true;
                IsBusy = true;
                FilteredExercises.AddRange(await ExerciseService.GetExercises());
                IsRefreshing = false;
            });
            IsBusy = false;
        }
    }
}
