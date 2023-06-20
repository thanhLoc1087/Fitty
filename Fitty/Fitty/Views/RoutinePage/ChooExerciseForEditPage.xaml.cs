using Fitty.Models;
using Fitty.ViewModels;
using System;
using System.Collections.Generic;
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
        EditRoutineViewModel _viewmodel;
        public ChooExerciseForEditPage()
        {
            InitializeComponent();
            BindingContext = _viewmodel = RoutinesViewModel.editRoutineViewModel;
        }
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ExerciseListView.BatchBegin();
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ExerciseListView.ItemsSource = _viewmodel.Exercises;
            }
            else
            {
                var searchInput = e.NewTextValue.Trim().ToLower();

                ExerciseListView.ItemsSource = _viewmodel.Exercises.Where(exercise =>
                    exercise.Name.ToLower().Contains(searchInput) ||
                    exercise.Muscle.ToLower().Contains(searchInput) ||
                    exercise.Difficulty.ToLower().Contains(searchInput) ||
                    exercise.Type.ToLower().Contains(searchInput) ||
                    exercise.Equipment.ToLower().Contains(searchInput)
                );
            }
            ExerciseListView.BatchCommit();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var data = await ExerciseService.GetExercises();
            _viewmodel.Exercises = data;
            ExerciseListView.ItemsSource = _viewmodel.Exercises;
        }
    }
}