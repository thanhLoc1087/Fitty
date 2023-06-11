using Fitty.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Name), nameof(Name))]
    class ExerciseDetailViewModel : BaseViewModel
    {
        private int id;
        private string name;
        private string type;
        private string muscle;
        private string equipment;
        private string difficulty;
        private string instruction;
        private string gif;
        private string userCreated;

        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                LoadItemId(value);
            }
        }

        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        public string Muscle
        {
            get => muscle;
            set => SetProperty(ref muscle, value);
        }
        public string Equipment
        {
            get => equipment;
            set => SetProperty(ref equipment, value);
        }
        public string Difficulty
        {
            get => difficulty;
            set => SetProperty(ref difficulty, value);
        }
        public string Instructions
        {
            get => instruction;
            set => SetProperty(ref instruction, value);
        }
        public string Gif 
        { 
            get => gif;
            set
            {
                SetProperty(ref gif, value);
            }
        }
        public string UserCreated
        { 
            get => userCreated;
            set
            {
                SetProperty(ref userCreated, value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await MusclesViewModel.DataSource.GetItemAsync(itemId);
                Id = item.Id;
                Type = item.Type.ToString();
                Muscle = item.Muscle.ToString();
                Equipment = item.Equipment;
                Difficulty = item.Difficulty.ToString();
                Instructions = item.Instructions;
                Gif = "glute_bridge.gif";
                UserCreated = item.UserCreated.ToString();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Exercise");
            }
        }
    }
}
