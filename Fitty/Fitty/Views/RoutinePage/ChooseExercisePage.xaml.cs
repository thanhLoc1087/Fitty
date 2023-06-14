using Fitty.Models;
using Fitty.ViewModels;
using Fitty.Views.RoutinePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseExercisePage : ContentPage
    {
        public ChooseExercisePage()
        {
            InitializeComponent();
            BindingContext = RoutinesViewModel.addRoutineViewModel;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as AddRoutineViewModel;
            ExerciseListView.BatchBegin();
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                ExerciseListView.ItemsSource = _container.Exercises;
            }
            else
            {
                var searchInput = e.NewTextValue.Trim().ToLower();

                ExerciseListView.ItemsSource = _container.Exercises.Where(exercise =>
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
            var _container = BindingContext as AddRoutineViewModel;

            var data = await ExerciseService.GetExercises();
            _container.Exercises = data;
            ExerciseListView.ItemsSource = _container.Exercises;
        }
    }
}