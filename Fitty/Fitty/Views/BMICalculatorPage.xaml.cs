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
    public partial class BMICalculatorPage : ContentPage
    {

        private int UserWeight = 75;
        private int UserHeight = 175;
        public BMICalculatorPage()
        {
            InitializeComponent();

            // Calculate BMI
            double heightInMeters = UserHeight / 100.0;
            double bmi = UserWeight / (heightInMeters * heightInMeters);
            bmiLabel.Text = bmi.ToString("F1");

            // Determine Basic Health Status based on BMI
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

    }
}