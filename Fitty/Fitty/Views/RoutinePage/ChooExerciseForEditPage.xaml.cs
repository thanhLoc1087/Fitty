using Fitty.Models;
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

namespace Fitty.Views.RoutinePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooExerciseForEditPage : ContentPage
    {
        List<ExerciseDisplay> exerciseSource;
        string searchInput;
        public ChooExerciseForEditPage()
        {
            InitializeComponent();
            var _viewmodel = BindingContext = RoutinesViewModel.editRoutineViewModel;
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
            var _container = BindingContext as EditRoutineViewModel;

            var data = await ExerciseService.GetExercises();
            if (exerciseSource == null)
            {
                exerciseSource = new List<ExerciseDisplay>();
            }
            exerciseSource.Clear();
            if (_container.FilterBookmarked)
            {
                data = data.Where(exercise => exercise.IsBookmarked).ToList();
            }
            data.ForEach(exercise =>
            {
                exerciseSource.Add(new ExerciseDisplay(exercise));
            });
            ApplyFilter(searchInput);
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var _container = BindingContext as EditRoutineViewModel;
            ExerciseListView.BatchBegin();
            _container.FilterBookmarked = !_container.FilterBookmarked;
            if (_container.FilterBookmarked)
            {
                Debug.WriteLine("filter");
                var data = await ExerciseService.GetBookmarkedExercises();
                exerciseSource.Clear();
                data.ForEach(exercise =>
                {
                    exerciseSource.Add(new ExerciseDisplay(exercise));
                });
            }
            else
            {
                var data = await ExerciseService.GetExercises();
                exerciseSource.Clear();
                data.ForEach(exercise =>
                {
                    exerciseSource.Add(new ExerciseDisplay(exercise));
                });
            }
            Debug.WriteLine(_container.Exercises.Count);
            ApplyFilter(searchInput);
            ExerciseListView.BatchCommit();
        }

        void ApplyFilter(string filter)
        {
            var _container = BindingContext as EditRoutineViewModel;
            if (string.IsNullOrEmpty(filter))
            {
                _container.Exercises = new ObservableCollection<ExerciseDisplay>(exerciseSource);
            }
            else
            {
                _container.Exercises = new ObservableCollection<ExerciseDisplay>(
                        exerciseSource.Where(exercise =>
                        exercise.Value.Name.ToLower().Contains(filter) ||
                        exercise.Value.Muscle.ToLower().Contains(searchInput) ||
                        exercise.Value.Difficulty.ToLower().Contains(filter) ||
                        exercise.Value.Type.ToLower().Contains(filter) ||
                        exercise.Value.Equipment.ToLower().Contains(filter))
                    );
            }
        }
    }
}