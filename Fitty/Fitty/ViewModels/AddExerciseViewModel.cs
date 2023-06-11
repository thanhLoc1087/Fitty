using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class AddExerciseViewModel : BaseViewModel
    {
        public AddExerciseViewModel() {
            SaveCommand = new Command(async () =>
            {
                if (Validate())
                {
                    await ExerciseService.AddExercise(Name, Type, Muscle, Equipment, Difficulty, Instructions, true);
                    Debug.WriteLine("Success");
                    ClearFields();
                }
                else
                    ErrorMessage = "Your exercise name can not be empty!";
            });
            Muscles = new List<string>
            {
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
                "Other",
            };
            Levels = new List<string>
            {
                "Beginner",
                "Intermediate"
            };
            Equipments = new List<string>
            {
                "Body weight",
                "Cable",
                "Barbel",
                "Dumbbell",
                "Kettlebells",
                "Machine",
                "Other",
            };
            Types = new List<string>
            {
                "Cardio",
                "Olympic Weightlifting",
                "Plyometrics",
                "Powerlifting",
                "Strength",
                "Stretching",
                "Strongman",
                "Other",
            };
            Muscle = Muscles[0];
            Equipment = Equipments[0];
            Type = Types[0];
            Difficulty = Levels[0];
        }
        string _name;
        string _type;
        string _muscle;
        string _equipment;
        string _difficulty;
        string _instruction;
        string _errorMsg;
        public string Name { get => _name;
            set {
                SetProperty(ref _name, value);
            }
        }
        public string Type { get => _type; 
            set
            {
                SetProperty(ref _type, value);
            }
        }
        public string Muscle { get => _muscle; set
            {
                SetProperty(ref _muscle, value);
            }
        }
        public string Equipment { get => _equipment; set
            {
                SetProperty(ref _equipment, value);
            }
        }
        public string Difficulty { get => _difficulty; set
            {
                SetProperty(ref _difficulty, value);
            }
        }
        public string Instructions { get => _instruction; set
            {
                SetProperty(ref _instruction, value);
            }
        }
        public string ErrorMessage
        { get => _errorMsg; set
            {
                SetProperty(ref _errorMsg, value);
            }
        }
        bool Validate()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Type)
                || string.IsNullOrEmpty(Muscle) || string.IsNullOrEmpty(Equipment)
                || string.IsNullOrEmpty(Difficulty));
        }
        void ClearFields()
        {
            Name = null;
            Instructions = null;
        }
        public List<String> Muscles { get; set; }
        public List<String> Levels { get; set; }
        public List<String> Equipments { get; set; }
        public List<String> Types { get; set; }
        public Command SaveCommand { get; set; }
    }
}
