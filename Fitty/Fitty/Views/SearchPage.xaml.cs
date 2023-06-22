using Fitty.Models;
using Fitty.Services;
using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        SearchViewModel viewmodel;
        string searchInput;
		public SearchPage ()
		{
			InitializeComponent ();
			BindingContext = viewmodel = new SearchViewModel();
		}

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExerciseListView.BatchBegin();
            searchInput = e.NewTextValue.Trim().ToLower();
            ApplyFilter(searchInput);
            ExerciseListView.BatchCommit();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ExerciseListView.BatchBegin();
            var data = await ExerciseService.GetExercises();
            if (viewmodel.Exercises == null)
			{
                viewmodel.Exercises = new List<ExerciseDisplay>();
			}
                viewmodel.Exercises.Clear();
			data.ForEach(exercise => 
			{
                viewmodel.Exercises.Add(new ExerciseDisplay(exercise));
			});
            ApplyFilter(searchInput);
            ExerciseListView.BatchCommit();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ExerciseListView.BatchBegin();
            viewmodel.FilterBookmarked = !viewmodel.FilterBookmarked;
            if (viewmodel.FilterBookmarked)
            {
                var data = await ExerciseService.GetBookmarkedExercises();
                    viewmodel.Exercises.Clear();
                data.ForEach(exercise =>
                {
                    viewmodel.Exercises.Add(new ExerciseDisplay(exercise));
                });
            }
            else
            {
                var data = await ExerciseService.GetExercises();
                    viewmodel.Exercises.Clear();
                data.ForEach(exercise =>
                {
                    viewmodel.Exercises.Add(new ExerciseDisplay(exercise));
                });
            }
            ApplyFilter(searchInput);
            ExerciseListView.BatchCommit();
        }
        void ApplyFilter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                viewmodel.FilteredExercises = new ObservableCollection<ExerciseDisplay>(viewmodel.Exercises);
            }
            else
            {
                viewmodel.FilteredExercises = new ObservableCollection<ExerciseDisplay>(
                        viewmodel.Exercises.Where(exercise =>
                        exercise.Value.Name.ToLower().Contains(filter) ||
                        exercise.Value.Muscle.ToLower().Contains(filter) ||
                        exercise.Value.Difficulty.ToLower().Contains(filter) ||
                        exercise.Value.Type.ToLower().Contains(filter) ||
                        exercise.Value.Equipment.ToLower().Contains(filter))
                    );
            }
        }
    }
}