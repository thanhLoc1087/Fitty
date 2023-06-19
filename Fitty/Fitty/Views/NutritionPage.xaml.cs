using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
public enum ActivityLevel
{
    LittleNoExercise,
    OneTwoTimesWeek,
    TwoThreeTimesWeek,
    ThreeFiveTimesWeek,
    SixSevenTimesWeek,
    ProfessionalAthlete
}

public enum Gender
{
    Male,
    Female
}

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class NutritionPage : ContentPage
    {
        public List<DRIResult> DRIResults;



        public NutritionPage()
        {
            InitializeComponent();
            DRIResults = new List<DRIResult>();
        }
        bool IsValidNumber(string number)
        {
            if (number == null || number == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void OnGenderRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //For handling data if needed
        }


        private void activityLevelPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Handling data if needed
        }
        private void OnCalculateButtonClicked(object sender, EventArgs e)
        {
            string age = ageEntry.Text;
            string weight = weightEntry.Text;
            string height = heightEntry.Text;
            Gender gender;
            ActivityLevel activityLevel;
            if (maleRadioButton.IsChecked)
            {
                gender = Gender.Male;
            }
            else
            {
                gender = Gender.Female;
            }
            activityLevel = Utils.ActivityLevelFromString((string)activityLevelPicker.SelectedItem);
            if (age == String.Empty || weight == String.Empty || height == String.Empty
                || age == null && weight == null && height == null
                )
            {
                DisplayAlert("Invalid Values", "Please enter valid non-zero numbers.", "OK");
            }
            else
            {
                bool success = NutrientCalculator.CalculateDRI(age, weight, height, activityLevel, gender, DRIResults);
                //resultsLayout.Opacity = DRIResults.Count > 0 ? 1 : 0; // Show/hide the results layout
                //DisplayAlert("Count", DRIResults.Count.ToString(), "OK");
                String content = "";
                foreach (DRIResult result in DRIResults)
                {
                    content += result.Nutrient + " : " + result.Value + " " + result.Unit + "\n";
                }
                DisplayAlert("Nutrient", content, "OK");
            }
        }
        private void OnFillDataButtonClicked(object sender, EventArgs e)
        {
            UserLocal user = UserLocal.GetUserFromFile();
            ageEntry.Text = user.Age.ToString();
            weightEntry.Text = user.Weight.ToString();
            heightEntry.Text = user.Height.ToString();
            if (user.Gender == Models.Gender.Male)
            {
                maleRadioButton.IsChecked = true;

            }
            else
            {
                femaleRadioButton.IsChecked = true;

            }
            switch((Models.ActivityLevel) user.ActivityLevel)
            {
                case Models.ActivityLevel.littleNoExercise:
                    activityLevelPicker.SelectedIndex = 0;
                    break;
                    case Models.ActivityLevel.oneTwoTimesWeek:
                    activityLevelPicker.SelectedIndex = 1; 
                    break;
                    case Models.ActivityLevel.twoThreeTimesWeek:
                    activityLevelPicker.SelectedIndex = 2;
                    break;
                    case Models.ActivityLevel.threeFiveTimesWeek:
                    activityLevelPicker.SelectedIndex = 3;
                    break;
                    case Models.ActivityLevel.sixSevenTimesWeek:
                    activityLevelPicker.SelectedIndex = 4;

                    break;
                    case Models.ActivityLevel.professionalAthlete:
                    activityLevelPicker.SelectedIndex = 5;
                    break;

            }
        }
    }
}

//Source for all calculation: https://www.omnicalculator.com/health/dri#what-is-dri
//Exclude pregnancy and smoking factor
// Vitamin calculation : https://www.ncbi.nlm.nih.gov/books/NBK56068/table/summarytables.t2/?report=objectonly

public class DRIResult
{
    public string Nutrient { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
}


public class Utils
{
    public static Gender GenderFromString(string genderString)
    {
        if (genderString == "male")
            return Gender.Male;
        else
            return Gender.Female;
    }

    public static ActivityLevel ActivityLevelFromString(string activityLevelString)
    {
        switch (activityLevelString)
        {
            case "littleNoExercise":
                return ActivityLevel.LittleNoExercise;
            case "oneTwoTimesWeek":
                return ActivityLevel.OneTwoTimesWeek;
            case "twoThreeTimesWeek":
                return ActivityLevel.TwoThreeTimesWeek;
            case "threeFiveTimesWeek":
                return ActivityLevel.ThreeFiveTimesWeek;
            case "sixSevenTimesWeek":
                return ActivityLevel.SixSevenTimesWeek;
            case "professionalAthlete":
                return ActivityLevel.ProfessionalAthlete;
            default:
                return ActivityLevel.LittleNoExercise;
        }
    }
}

public class NutrientCalculator
{
    public static bool CalculateDRI(string age, string weight, string height, ActivityLevel selectedActivityLevel, Gender selectedGender, List<DRIResult> driResults)
    {
        driResults.Clear();

        // Get user inputs


        int parsedAge = 0;
        if (!int.TryParse(age, out parsedAge))
            return false;

        double parsedWeight = 0;
        if (!double.TryParse(weight, out parsedWeight))
            return false;

        double parsedHeight = 0;
        if (!double.TryParse(height, out parsedHeight))
            return false;

        // Calculate DRI for vitamins
        double vitaminA = CalculateVitaminA(parsedAge, selectedGender);
        double vitaminD = CalculateVitaminD(parsedAge, selectedGender);
        double vitaminE = CalculateVitaminE(parsedAge);
        double vitaminK = CalculateVitaminK(parsedAge, selectedGender);
        double vitaminC = CalculateVitaminC(parsedAge, selectedGender);

        // Calculate DRI for calories
        double calories = CalculateCalories(parsedWeight, parsedHeight, selectedActivityLevel, selectedGender);

        // Calculate DRI for macronutrients
        double protein = CalculateProtein(calories);
        double fat = CalculateFat(calories);
        double carbs = CalculateCarbohydrates(calories);


        // Add DRI results to the collection
        driResults.Add(new DRIResult { Nutrient = "Vitamin A", Value = vitaminA, Unit = "mg" });
        driResults.Add(new DRIResult { Nutrient = "Vitamin D", Value = vitaminD, Unit = "mg" });
        driResults.Add(new DRIResult { Nutrient = "Vitamin E", Value = vitaminE, Unit = "mg" });
        driResults.Add(new DRIResult { Nutrient = "Vitamin K", Value = vitaminK, Unit = "mg" });
        driResults.Add(new DRIResult { Nutrient = "Vitamin C", Value = vitaminC, Unit = "mg" });
        driResults.Add(new DRIResult { Nutrient = "Calories", Value = calories, Unit = "" });
        driResults.Add(new DRIResult { Nutrient = "Protein", Value = protein, Unit = "g" });
        driResults.Add(new DRIResult { Nutrient = "Fat", Value = fat, Unit = "g" });
        driResults.Add(new DRIResult { Nutrient = "Carbohydrates", Value = carbs, Unit = "g" });


        return true;
    }


    public static double CalculateVitaminA(int age, Gender gender)
    {
        if (gender == Gender.Male)
        {
            if (age >= 1 && age <= 3) return 300;
            if (age >= 4 && age <= 8) return 400;
            if (age >= 9 && age <= 13) return 600;
            if (age >= 14 && age <= 18) return 900;
            if (age >= 19) return 900;
        }
        else
        {
            if (age >= 1 && age <= 3) return 300;
            if (age >= 4 && age <= 8) return 400;
            if (age >= 9 && age <= 13) return 600;
            if (age >= 14 && age <= 18) return 700;
            if (age >= 19) return 700;
        }
        return 0;
    }

    public static double CalculateVitaminD(int age, Gender gender)
    {
        if (gender == Gender.Male)
        {
            if (age >= 1 && age <= 3) return 15;
            if (age >= 4 && age <= 8) return 15;
            if (age >= 9 && age <= 13) return 15;
            if (age >= 14 && age <= 18) return 15;
            if (age >= 19 && age <= 70) return 15;
            if (age > 70) return 20;
        }
        else
        {
            if (age >= 1 && age <= 3) return 15;
            if (age >= 4 && age <= 8) return 15;
            if (age >= 9 && age <= 13) return 15;
            if (age >= 14 && age <= 18) return 15;
            if (age >= 19 && age <= 70) return 15;
            if (age > 70) return 20;
        }
        return 0;
    }

    public static double CalculateVitaminE(int age)
    {
        if (age >= 1 && age <= 3) return 6;
        if (age >= 4 && age <= 8) return 7;
        if (age >= 9 && age <= 13) return 11;
        if (age >= 14 && age <= 18) return 15;
        if (age >= 19) return 15;
        return 0;
    }

    public static double CalculateVitaminK(int age, Gender gender)
    {
        if (gender == Gender.Male)
        {
            if (age >= 1 && age <= 3) return 30;
            if (age >= 4 && age <= 8) return 55;
            if (age >= 9 && age <= 13) return 60;
            if (age >= 14 && age <= 18) return 75;
            if (age >= 19) return 120;
        }
        else
        {
            if (age >= 1 && age <= 3) return 30;
            if (age >= 4 && age <= 8) return 55;
            if (age >= 9 && age <= 13) return 60;
            if (age >= 14 && age <= 18) return 75;
            if (age >= 19) return 90;
        }
        return 0;
    }

    public static double CalculateVitaminC(int age, Gender gender)
    {
        if (gender == Gender.Male)
        {
            if (age >= 1 && age <= 3) return 15;
            if (age >= 4 && age <= 8) return 25;
            if (age >= 9 && age <= 13) return 45;
            if (age >= 14 && age <= 18) return 75;
            if (age >= 19) return 90;
        }
        else
        {
            if (age >= 1 && age <= 3) return 15;
            if (age >= 4 && age <= 8) return 25;
            if (age >= 9 && age <= 13) return 45;
            if (age >= 14 && age <= 18) return 65;
            if (age >= 19) return 75;
        }
        return 0;
    }

    public static double CalculateCalories(double weight, double height, ActivityLevel activityLevel, Gender gender)
    {
        double bmr;

        // Basal Metabolic Rate Formula (Mifflin-St Jeor)
        if (gender == Gender.Male)
        {
            bmr = 10 * weight + 6.25 * height - 5 * 30 + 5;
        }
        else
        {
            bmr = 10 * weight + 6.25 * height - 5 * 30 - 161;
        }

        double activityFactor = 1.0;

        switch (activityLevel)
        {
            case ActivityLevel.LittleNoExercise:
                activityFactor = 1.2;
                break;
            case ActivityLevel.OneTwoTimesWeek:
                activityFactor = 1.375;
                break;
            case ActivityLevel.TwoThreeTimesWeek:
                activityFactor = 1.55;
                break;
            case ActivityLevel.ThreeFiveTimesWeek:
                activityFactor = 1.725;
                break;
            case ActivityLevel.SixSevenTimesWeek:
                activityFactor = 1.9;
                break;
            case ActivityLevel.ProfessionalAthlete:
                activityFactor = 2.3;
                break;
        }

        return bmr * activityFactor;
    }

    public static double CalculateProtein(double calories)
    {
        return (calories * 0.15) / 4;
    }

    public static double CalculateFat(double calories)
    {
        return (calories * 0.3) / 9;
    }

    public static double CalculateCarbohydrates(double calories)
    {
        return (calories * 0.55) / 4;
    }
}