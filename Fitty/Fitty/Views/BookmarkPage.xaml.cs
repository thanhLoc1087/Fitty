using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookmarkPage : ContentPage
    {
        public BookmarkPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var exercises = await ExerciseService.GetBookmarkedExercises();
            ObservableCollection<ExerciseDisplay> data = new ObservableCollection<ExerciseDisplay>();
            exercises.ForEach(exercise =>
            {
                data.Add(new ExerciseDisplay(exercise));
            });
            lblError.IsVisible = data.Count() <= 0;
            cvExerciseList.ItemsSource = data;
        }
    }
}