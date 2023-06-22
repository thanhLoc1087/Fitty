using Fitty.Models;
using Fitty.Services;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<ExerciseDisplay> _exercises;
        public List<ExerciseDisplay> Exercises
        {
            get => _exercises;
            set
            {
                SetProperty(ref _exercises, value);
            } 
        }
        ObservableCollection<ExerciseDisplay> _filteredExercises;
        public ObservableCollection<ExerciseDisplay> FilteredExercises
        {
            get => _filteredExercises;
            set
            {
                SetProperty(ref _filteredExercises, value);
            } 
        }

        string bookmarkImage;
        public string BookmarkImage
        {
            get => bookmarkImage;
            set { SetProperty(ref bookmarkImage, value);}
        }

        bool filterBookmarked;
        public bool FilterBookmarked
        {
            get => filterBookmarked;
            set
            {
                SetProperty(ref filterBookmarked, value);
                if (value)
                {
                    BookmarkImage = "bookmark.png";
                } else
                {
                    BookmarkImage = "ribbon.png";
                }
            }
        }

        public SearchViewModel() 
        {
            BookmarkImage = "ribbon.png";
            ItemTapped = new Command<Exercise>(OnItemSelected);
            Refresh = new Command(async () =>
            {
                IsRefreshing = true;
                IsBusy = true;
                var data = await ExerciseService.GetExercises();
                if (FilteredExercises == null)
                {
                    FilteredExercises = new ObservableCollection<ExerciseDisplay>();
                }
                FilteredExercises.Clear();
                data.ForEach(exercise =>
                {
                    FilteredExercises.Add(new ExerciseDisplay(exercise));
                });
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
