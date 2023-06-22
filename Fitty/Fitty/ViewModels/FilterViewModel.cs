using Fitty.Models;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class FilterViewModel : BaseViewModel
    {

        List<Exercise> _filteredExercises;
        public List<Exercise> FilteredExercises { 
            get => _filteredExercises; 
            set
            {
                SetProperty(ref _filteredExercises, value);
            }
        }
        String _selectedMuscle;
        public String SelectedMuscle{ 
            get => _selectedMuscle; 
            set
            {
                SetProperty(ref _selectedMuscle, value);
                LoadExerciseList();
            }
        }
        String _selectedLevel;
        public String SelectedLevel
        { 
            get => _selectedLevel; 
            set
            {
                SetProperty(ref _selectedLevel, value);
                LoadExerciseList();
            }
        }
        String _selectedEquipment;
        public String SelectedEquipment
        { 
            get => _selectedEquipment; 
            set
            {
                SetProperty(ref _selectedEquipment, value);
                LoadExerciseList();
            }
        }
        public List<String> Muscles { get; set; }
        public List<String> Levels { get; set; }
        public List<String> Equipments { get; set; }
        public Command EraseMuscleFilter { get; set; }

        public Command<Exercise> ItemTapped { get; }

        public FilterViewModel() {
            ItemTapped = new Command<Exercise>(OnItemSelected);
            FilteredExercises = new List<Exercise>();
            FilteredExercises.AddRange(HomeViewModel.DataSource.exercises);
            EraseMuscleFilter = new Command(() => {
                SelectedMuscle = null;
            });
            Muscles = new List<string>
            {
                "--All--",
                "Abdominals",
                "Biceps",
                "Calves",
                "Chest",
                "Forearms",
                "Glutes",
                "Hamstrings",
                "Lats",
                "Lower Back",
                "Middle Back",
                "Neck",
                "Quadriceps",
                "Traps",
                "Tricep",
            };
            Levels = new List<string>
            {
                "--All--",
                "Beginner",
                "Intermediate",
                "Expert",
            };
            Equipments = new List<string>
            {
                "--All--",
                "Body weight",
                "Cable",
                "Barbel",
                "Dumbbell",
                "Kettlebells",
                "Machine",
                "Other",
            };
        }

        void LoadExerciseList()
        {
            var muscle = SelectedMuscle == "--All--" || SelectedMuscle == null ? null : SelectedMuscle.ToLower();
            FilteredExercises = HomeViewModel.DataSource.exercises;
            if (muscle == "lower back")
            {
                muscle = "lower_back";
            }
            if (muscle == "middle back")
            {
                muscle = "middle_back";
            }
            FilteredExercises = muscle != null 
                ? FilteredExercises.Where((exercise) => exercise.Muscle == muscle).ToList()
                : FilteredExercises;
            FilteredExercises = SelectedLevel == "--All--" || SelectedLevel == null
                ? FilteredExercises 
                : FilteredExercises.Where((exercise) => exercise.Difficulty == SelectedLevel.ToLower()).ToList();

            var equipment = SelectedEquipment == "Body weight" ? "body_only" : SelectedEquipment.ToLower();
            FilteredExercises = SelectedEquipment == "--All--" || SelectedEquipment == null
                ? FilteredExercises
                : FilteredExercises.Where((exercise) => exercise.Equipment == equipment).ToList();
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
