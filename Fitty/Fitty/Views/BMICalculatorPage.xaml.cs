using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Fitty.Models;
namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BMICalculatorPage : ContentPage
    {

        private int UserWeight = UserLocal.GetUserFromFile().Weight;
        private int UserHeight = UserLocal.GetUserFromFile().Height;
        public BMICalculatorPage()
        {
            InitializeComponent();

            // Calculate BMI
            Calculate(UserWeight, UserHeight);
        }

        public void Calculate(int height, int weight)
        {
            double heightInMeters = height / 100.0;
            double bmi = weight / (heightInMeters * heightInMeters);
            bmiLabel.Text = bmi.ToString("F1");

            ColorReset();
            if (bmi < 18.5)
            {
                healthStatusLabel.Text = "Underweight";
                healthStatusLabel.TextColor = Color.Orange;
                underweightRangeLabel.TextColor = Color.Orange;
                underweightLabel.TextColor = Color.Orange;
                underweightDescription.TextColor = Color.Orange;
            }
            else if (bmi < 25)
            {
                healthStatusLabel.Text = "Healthy weight";
                healthStatusLabel.TextColor = Color.Green;
                healthyRangeLabel.TextColor = Color.Green;
                healthyLabel.TextColor = Color.Green;
                healthyDescription.TextColor = Color.Green;
            }
            else if (bmi < 30)
            {
                healthStatusLabel.Text = "Overweight";
                healthStatusLabel.TextColor = Color.Orange;
                overweightRangeLabel.TextColor = Color.Orange;
                overweightLabel.TextColor = Color.Orange;
                overweightDescription.TextColor = Color.Orange;
            }
            else if (bmi < 35)
            {
                healthStatusLabel.Text = "Obese class 1";
                healthStatusLabel.TextColor = Color.OrangeRed;
                obeseLabel1.TextColor = Color.OrangeRed;
                obeseRangeLabel1.TextColor = Color.OrangeRed;
                obeseDescription1.TextColor = Color.OrangeRed;
            }
            else if (bmi < 40)
            {
                healthStatusLabel.Text = "Obese class 2";
                healthStatusLabel.TextColor = Color.OrangeRed;
                obeseRangeLabel2.TextColor = Color.OrangeRed;
                obeseLabel2.TextColor = Color.OrangeRed;
                obeseDescription2.TextColor = Color.OrangeRed;
            }
            else
            {
                healthStatusLabel.Text = "Obese class 3";
                healthStatusLabel.TextColor = Color.Red;
                obeseRangeLabel3.TextColor = Color.Red;
                obeseLabel3.TextColor = Color.Red;
                obeseDescription3.TextColor = Color.Red;
            }
        }
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            int height = 0, weight = 0;
            if (!string.IsNullOrEmpty(heightEntry.Text))
            {
                height = int.Parse(heightEntry.Text);
            }

            if (!string.IsNullOrEmpty(weightEntry.Text))
            {
                weight = int.Parse(weightEntry.Text);
            }
            Calculate(height, weight);
            customPopup.IsVisible = false;
            heightEntry.Text = "";
            weightEntry.Text = "";
        }
        private void OnCustomClicked(object sender, EventArgs e)
        {
            customPopup.IsVisible = true;
        }
        private void OnResetClicked(object sender, EventArgs e)
        {
            heightEntry.Text = "";
            weightEntry.Text = "";
            Calculate(UserHeight, UserWeight);
        }
        private void ColorReset()
        {
            healthStatusLabel.TextColor =
            underweightRangeLabel.TextColor = underweightLabel.TextColor = underweightDescription.TextColor =
            healthyRangeLabel.TextColor = healthyLabel.TextColor = healthyDescription.TextColor =
            overweightRangeLabel.TextColor = overweightLabel.TextColor = overweightDescription.TextColor =
            obeseLabel1.TextColor = obeseRangeLabel1.TextColor = obeseDescription1.TextColor =
            obeseLabel2.TextColor = obeseRangeLabel2.TextColor = obeseDescription2.TextColor =
            obeseRangeLabel3.TextColor = obeseLabel3.TextColor = obeseDescription3.TextColor = Color.Gray;
        }
    }
}