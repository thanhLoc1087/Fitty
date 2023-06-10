using Fitty.ViewModels;
using Fitty.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fitty
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Welcome), typeof(Welcome));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(HydrationAlarm), typeof(HydrationAlarm));
            Routing.RegisterRoute(nameof(NutritionPage), typeof(NutritionPage));
            Routing.RegisterRoute(nameof(MusclesPage), typeof(MusclesPage));
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
        }

    }
}
