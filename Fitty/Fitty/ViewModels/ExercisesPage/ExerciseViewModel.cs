using Fitty.Models;
using Fitty.Views;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Muscle), nameof(Muscle))]
    class ExerciseViewModel : BaseViewModel
    {
        string _muscle;
        public Command<ExerciseDisplay> ItemTapped { get; }
        public Command<ExerciseDisplay> ItemBookmarked { get; }

        public ExerciseViewModel() 
        {
            ItemTapped = new Command<ExerciseDisplay>(OnItemSelected);
            ItemBookmarked = new Command<ExerciseDisplay>(OnItemBookmarked);
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
        List<ExerciseDisplay> _muscleExercises;
        public List<ExerciseDisplay> MuscleExercises
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
        public List<ExerciseDisplay> GetExercises(string muscleGroup)
        {
            List<ExerciseDisplay> exerciseList = new List<ExerciseDisplay>();
            foreach(var exercise in HomeViewModel.DataSource.exercises)
            {
                if (exercise.Muscle == muscleGroup)
                {
                    Debug.WriteLine(exercise.IsBookmarked);
                    exerciseList.Add(new ExerciseDisplay(exercise));
                }
            }
            return exerciseList;
        }
        private async void OnItemSelected(ExerciseDisplay item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ExerciseDetailPage)}?{nameof(ExerciseDetailViewModel.Name)}={item.Value.Name}");
        }
        private async void OnItemBookmarked(ExerciseDisplay item)
        {
            if (item == null)
                return;
            await HandleBookmark(item);
        }
        async Task HandleBookmark(ExerciseDisplay item)
        {
            item.Value.IsBookmarked = !item.Value.IsBookmarked;
            await ExerciseService.UpdateExercise(item.Value);
        }
    }
}
