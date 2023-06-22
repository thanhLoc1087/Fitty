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
        String _errorMsg;
        public String ErrorMsg
        { 
            get => _errorMsg; 
            set
            {
                SetProperty(ref _errorMsg, value);
            }
        }
        public List<String> Muscles { get; set; }
        public List<String> Levels { get; set; }
        public List<String> Equipments { get; set; }
        public Command EraseMuscleFilter { get; set; }
        bool isListEmpty;
        public bool IsListEmpty
        {
            get => isListEmpty;
            set
            {
                SetProperty(ref isListEmpty, value);
                ErrorMsg = isListEmpty ? "No exercise found." : "";
            }
        }

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
                "Lower back",
                "Middle back",
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
            FilteredExercises = muscle != null 
                ? FilteredExercises.Where((exercise) => exercise.Muscle == muscle).ToList()
                : FilteredExercises;
            FilteredExercises = SelectedLevel == "--All--" || SelectedLevel == null
                ? FilteredExercises 
                : FilteredExercises.Where((exercise) => exercise.Difficulty == SelectedLevel.ToLower()).ToList();
            FilteredExercises = SelectedEquipment == "--All--" || SelectedEquipment == null
                ? FilteredExercises
                : FilteredExercises.Where((exercise) => exercise.Equipment == SelectedEquipment.ToLower()).ToList();
            IsListEmpty = FilteredExercises.Count <=0;
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
