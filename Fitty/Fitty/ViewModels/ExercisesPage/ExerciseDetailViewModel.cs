using Fitty.Models;
using Fitty.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    //[QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Id), nameof(Id))]
    [QueryProperty(nameof(Name), nameof(Name))]
    class ExerciseDetailViewModel : BaseViewModel
    {
        public ExerciseDetailViewModel() {
            BookmarkCommand = new Command(HandleBookmark);
        }
        private Exercise exercise;
        private int id;
        private string name;
        private string type;
        private string muscle;
        private string equipment;
        private string difficulty;
        private string instruction;
        private string gif;
        private bool userCreated;
        private bool isBookmarked;
        private string bookmarkIcon;
        public Command BookmarkCommand { get; set; }

        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                LoadItemId(value);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                LoadItemName(value);
            }
        }

        public string Type
        {
            get => type;
            set => SetProperty(ref type, FormatString(value));
        }
        public string Muscle
        {
            get => muscle;
            set => SetProperty(ref muscle, FormatString(value));
        }
        public string Equipment
        {
            get => equipment;
            set => SetProperty(ref equipment, FormatString(value));
        }
        public string Difficulty
        {
            get => difficulty;
            set => SetProperty(ref difficulty, FormatString(value));
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
        public string BookmarkIcon
        { 
            get => bookmarkIcon;
            set
            {
                SetProperty(ref bookmarkIcon, value);
            }
        }
        public bool UserCreated
        { 
            get => userCreated;
            set
            {
                SetProperty(ref userCreated, value);
            }
        }
        public bool IsBookmarked
        { 
            get => isBookmarked;
            set
            {
                SetProperty(ref isBookmarked, value);
                BookmarkIcon = isBookmarked ? "bookmark.png" : "ribbon.png";
            }
        }
        private string FormatString(string value)
        {
            ////// add spaces between words and make the first letter uppercase
            value = value.Replace("_", " ");
            value = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            return value;
        }
        public async void LoadItemId(int itemId)
        {
            try
            {
                exercise = await HomeViewModel.DataSource.GetItemAsyncById(itemId);
                Name = exercise.Name;
                Type = exercise.Type.ToString();
                Muscle = exercise.Muscle.ToString();
                Equipment = exercise.Equipment;
                Difficulty = exercise.Difficulty.ToString();
                Instructions = exercise.Instructions;
                IsBookmarked = exercise.IsBookmarked;
                // Format the gif value
                Gif = FormatGifName(name) + ".gif";

                UserCreated = exercise.UserCreated;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Exercise");
            }
        }

        public async void LoadItemName(string name)
        {
            try
            {
                exercise = await HomeViewModel.DataSource.GetItemAsync(name);
                Id = exercise.Id;
                Type = exercise.Type.ToString();
                Muscle = exercise.Muscle.ToString();
                Equipment = exercise.Equipment;
                Difficulty = exercise.Difficulty.ToString();
                Instructions = exercise.Instructions;
                IsBookmarked = exercise.IsBookmarked;

                // Format the gif value
                Gif = FormatGifName( name ) + ".gif";

                UserCreated = exercise.UserCreated;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Exercise");
            }
        }

        private string FormatGifName(string value)
        {
            string newValue = Regex.Replace(value, @"[^a-zA-Z0-9_.]+", "");
            newValue = newValue.ToLower();

            return newValue;
            
        }
        async void HandleBookmark()
        {
            exercise.IsBookmarked = !IsBookmarked; 
            IsBookmarked = IsBookmarked;
            await ExerciseService.UpdateExercise(exercise);
        }
    }
}
