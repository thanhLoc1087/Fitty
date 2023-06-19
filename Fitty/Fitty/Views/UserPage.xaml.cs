using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fitty.Models;

using System.IO;
using Xamarin.Essentials;
using System.Xml.XPath;
using System.Runtime.InteropServices;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        UserLocal user;
        public UserPage()
        {
            
            
            InitializeComponent();
         



        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUserAsync();
        }
        private async Task LoadUserAsync()
        {
            await CheckUserFileAsync();

            AgeLabel.Text = user.Age.ToString();
            HeightLabel.Text = user.Height.ToString();
            WeightLabel.Text = user.Weight.ToString();
            NameLabel.Text = user.Name;
            string genderLabelContent;
            switch ((Gender)user.Gender)
            {
                case Gender.Male:
                    {
                        genderLabelContent = "Male";
                        break;
                    }
                case Gender.Female:
                    {
                        genderLabelContent = "Female";
                        break;
                    }
                default:
                    {
                        genderLabelContent = "Unknown";
                        break;
                    }
            }
            GenderLabel.Text = genderLabelContent;



            string activityLabelContent;
            switch ((ActivityLevel)user.ActivityLevel)
            {
                case ActivityLevel.LittleNoExercise:
                    activityLabelContent = "Little exercise";
                    break;
                case ActivityLevel.OneTwoTimesWeek:
                    activityLabelContent = "1-2 times/week";
                    break;
                case ActivityLevel.TwoThreeTimesWeek:
                    activityLabelContent = "2-3 times/week";
                    break;
                case ActivityLevel.ThreeFiveTimesWeek:
                    activityLabelContent = "3-5 times/week";
                    break;
                case ActivityLevel.SixSevenTimesWeek:
                    activityLabelContent = "6-7 times/week";
                    break;
                case ActivityLevel.ProfessionalAthlete:
                    activityLabelContent = "Professional athlete";
                    break;

                default:
                    activityLabelContent = "Unknown";
                    break;
            }
            ActivityLabel.Text = activityLabelContent;


        }
        async Task CheckUserFileAsync()
        {
            string infoPath = UserLocal.GetInfoPath();
           

            if (File.Exists(infoPath))
            {
                
                
                user = UserLocal.GetUserFromFile();
                
            }
            else
            {
                DisplayAlert("New user", "Please fill the form", "OK");
                user = new UserLocal();
                await UserLocal.SaveUserToFile(user);
            }
        }


        private void EditButton_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new EditUserPage());
        }
    }
}