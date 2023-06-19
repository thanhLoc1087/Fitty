using Fitty.ViewModels;
using Fitty.Views;
using Fitty.Views.RoutinePage;
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
            Routing.RegisterRoute(nameof(UserPage) , typeof(UserPage));
            Routing.RegisterRoute(nameof(Welcome), typeof(Welcome));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(HydrationAlarm), typeof(HydrationAlarm));
            Routing.RegisterRoute(nameof(CalculatorPage), typeof(CalculatorPage));
            Routing.RegisterRoute(nameof(NutritionPage), typeof(NutritionPage));
            Routing.RegisterRoute(nameof(MusclesPage), typeof(MusclesPage));
            Routing.RegisterRoute(nameof(ExercisePage), typeof(ExercisePage));
            Routing.RegisterRoute(nameof(ExerciseDetailPage), typeof(ExerciseDetailPage));
            Routing.RegisterRoute(nameof(RoutinesPage), typeof(RoutinesPage));
            Routing.RegisterRoute(nameof(RoutineDetailPage), typeof(RoutineDetailPage));
            Routing.RegisterRoute(nameof(RoutineExePage), typeof(RoutineExePage));
            Routing.RegisterRoute(nameof(AddRoutinePage), typeof(AddRoutinePage));
            Routing.RegisterRoute(nameof(ChooseExercisePage), typeof(ChooseExercisePage));
        }

    }
}
