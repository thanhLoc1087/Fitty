using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        public EditUserPage()
        {

            InitializeComponent();

            UserLocal user = UserLocal.GetUserFromFile();
            nameEntry.Text = user.Name;
            ageEntry.Text = user.Age.ToString();
            heightEntry.Text = user.Height.ToString();
            weightEntry.Text = user.Weight.ToString();
            if (user.Gender == Models.Gender.Male)
                maleRadioButton.IsChecked = true;
            else
                femaleRadioButton.IsChecked = true;
            switch (user.ActivityLevel)
            {
                case Models.ActivityLevel.littleNoExercise:
                    {
                        activityLevelPicker.SelectedItem = "Little/No exercise";
                        break;
                    }
                case Models.ActivityLevel.oneTwoTimesWeek:
                    {
                        activityLevelPicker.SelectedItem = "1-2 times a week";
                        break;
                    }
                case Models.ActivityLevel.threeFiveTimesWeek:
                    {
                        activityLevelPicker.SelectedItem = "3-5 times a week";
                        break;
                    }
                    case Models.ActivityLevel.sixSevenTimesWeek:
                    {
                        activityLevelPicker.SelectedItem = "6-7 times a week";
                        break;
                    }
                  case Models.ActivityLevel.professionalAthlete:
                    {
                        activityLevelPicker.SelectedItem = "Professional athlete";
                        break;
                    }
                    
            }
        }

        private void  SaveButton_Clicked(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(ageEntry.Text) 
                || 
                string.IsNullOrEmpty(heightEntry.Text)
                ||
                string.IsNullOrEmpty(weightEntry.Text)
                ||
                string.IsNullOrEmpty(nameEntry.Text)
                
                )
            {
                DisplayAlert("Error", "Please fill all the fields", "OK");

            }
            else
            {
               
                UserLocal user = UserLocal.GetUserFromFile();
                user.Name =  nameEntry.Text;
                user.Weight = int.Parse(weightEntry.Text);
                user.Age = int.Parse(ageEntry.Text);
                user.Height = int.Parse(heightEntry.Text);
                if (maleRadioButton.IsChecked)
                {
                    user.Gender = Models.Gender.Male;
                }
                else
                {
                    user.Gender = Models.Gender.Female;
                }

                switch (activityLevelPicker.SelectedItem.ToString())
                {
                    case "Little/No exercise":
                        {
                            user.ActivityLevel = Models.ActivityLevel.littleNoExercise;
                            break;
                        }
                    case "1-2 times a week":
                        {
                            user.ActivityLevel = Models.ActivityLevel.oneTwoTimesWeek;
                            break;
                        }
                    case "3-5 times a week":
                        {
                            user.ActivityLevel = Models.ActivityLevel.threeFiveTimesWeek;
                            break;
                        }
                        case "6-7 times a week":
                        {
                            user.ActivityLevel = Models.ActivityLevel.sixSevenTimesWeek;
                            break;
                        }
                        case "Professional athlete":
                        {
                            user.ActivityLevel = Models.ActivityLevel.professionalAthlete;
                            break;
                        }

                }
                UserLocal.SaveUserToFile(user);
                Navigation.PopAsync();
            }
        }
    }
}