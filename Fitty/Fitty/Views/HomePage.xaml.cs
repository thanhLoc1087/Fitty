﻿using Fitty.Models;
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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var data = await ExerciseService.GetExercises();
            HomeViewModel.DataSource.exercises = data;
        }
    }
}