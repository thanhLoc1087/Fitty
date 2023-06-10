using Fitty.Models;
using Fitty.Services;
using Fitty.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    internal class MusclesViewModel : BaseViewModel
    {
        public Command<MuscleGroup> ItemTapped { get; }
        static public DataSource DataSource { get; set; }

        public MusclesViewModel()
        {
            ItemTapped = new Command<MuscleGroup>(OnItemSelected);
            DataSource = new DataSource();
        }

        private async void OnItemSelected(MuscleGroup item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ExercisePage)}?{nameof(ExerciseViewModel.Muscle)}={item.Enum.ToString()}");
        }
        public List<MuscleGroup> MuscleGroups { get; } = new List<MuscleGroup>
        {
            new MuscleGroup("Abdominals", MuscleGroupEnum.abdominals, "abdominals_muscle.jpg"),
            new MuscleGroup("Biceps", MuscleGroupEnum.biceps, "biceps_muscle.jpg"),
            new MuscleGroup("Calves", MuscleGroupEnum.calves, "calves_muscle.jpg"),
            new MuscleGroup("Chest", MuscleGroupEnum.chest, "chest_muscle.jpg"),
            new MuscleGroup("Forearms", MuscleGroupEnum.forearms, "forearms_muscle.jpg"),
            new MuscleGroup("Glutes", MuscleGroupEnum.glutes, "glutes_muscle.jpg"),
            new MuscleGroup("Hamstrings", MuscleGroupEnum.hamstrings, "hamstrings_muscle.jpg"),
            new MuscleGroup("Lats", MuscleGroupEnum.lats, "lats_muscle.jpg"),
            new MuscleGroup("Lower Back", MuscleGroupEnum.lower_back, "abdominals_muscle.jpg"),
            new MuscleGroup("Middle Back", MuscleGroupEnum.middle_back, "middle_back_muscle.jpg"),
            new MuscleGroup("Neck", MuscleGroupEnum.neck, "neck_muscle.jpg"),
            new MuscleGroup("Quadriceps", MuscleGroupEnum.quadriceps, "quadriceps_muscle.jpg"),
            new MuscleGroup("Traps", MuscleGroupEnum.traps, "traps_muscle.jpg"),
            new MuscleGroup("Tricep", MuscleGroupEnum.tricep, "triceps_muscle.jpg"),
        };
    }
}
