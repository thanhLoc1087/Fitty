using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseMainPage : TabbedPage
    {
        public ExerciseMainPage ()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(AddExercisePage)}");
        }

        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(SearchPage)}");
        }
    }
}