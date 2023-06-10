using Fitty.Models;
using Fitty.Views;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Muscle), nameof(Muscle))]
    class ExerciseViewModel : BaseViewModel
    {
        string _muscle;
        public Command<Exercise> ItemTapped { get; }

        public ExerciseViewModel() 
        {
            ItemTapped = new Command<Exercise>(OnItemSelected);
        }

        public string Muscle
        {
            get => _muscle; 
            set
            {
                Title = value.ToUpper();
                SetProperty(ref _muscle, value);
                GetExercisesByMuscle(Muscle);
            }
        }
        List<Exercise> _muscleExercises;
        public List<Exercise> MuscleExercises
        {
            get => _muscleExercises;
            set
            {
                SetProperty(ref _muscleExercises, value);
            }
        }

        void GetExercisesByMuscle(string muscleGroup)
        {
           MuscleExercises = GetExercises(muscleGroup);
        }
        public List<Exercise> GetExercises(string muscleGroup)
        {
            Debug.WriteLine(muscleGroup);
            List<Exercise> exerciseList = new List<Exercise>();
            foreach(var exercise in MusclesViewModel.DataSource.exercises)
            {
                if (exercise.Muscle == muscleGroup)
                {
                    exerciseList.Add(exercise);
                }
            }
            return exerciseList;
        }
        private async void OnItemSelected(Exercise item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ExerciseDetailViewModel.Name)}={item.Name}");
        }
    }
}
