using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fitty.Models
{
    class ExerciseDisplay : BaseViewModel
    {
        public Command ItemBookmarked { get; }
        Exercise value;
        public Exercise Value
        {
            get => value;
            set
            {
                this.value = value;
                ButtonSource = value.IsBookmarked ? "bookmark.png" : "ribbon.png";
            }
        }
        string buttonSource;
        public string ButtonSource
        {
            get => buttonSource;
            set
            {
                SetProperty(ref buttonSource, value);
            }
        }
        public ExerciseDisplay(Exercise exercise)
        {
            ItemBookmarked = new Command(OnItemBookmarked);
            this.value = exercise;
            ButtonSource = exercise.IsBookmarked ? "bookmark.png" : "ribbon.png";
        }
        private async void OnItemBookmarked()
        {
            await HandleBookmark();
        }
        async Task HandleBookmark()
        {
            Value.IsBookmarked = !Value.IsBookmarked;
            ButtonSource = Value.IsBookmarked ? "bookmark.png" : "ribbon.png";
            await ExerciseService.UpdateExercise(Value);
        }
    }
}
