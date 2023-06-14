﻿using Fitty.Models;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    internal class AddRoutineViewModel:BaseViewModel
    {
        public Command<Exercise> ItemTapped { get; }
        public Command Test { get; }
        Routine _routine;
        public Routine Routine { get { return _routine; } set
            {
                SetProperty(ref _routine, value);
            }
        }
        List<Exercise> _exercises;
        public List<Exercise> Exercises
        {
            get => _exercises;
            set
            {
                SetProperty(ref _exercises, value);
            }
        }
        List<RoutineDetail> _details;
        public List<RoutineDetail> Details
        {
            get => _details;
            set
            {
                Debug.Write(value);
                SetProperty(ref _details, value);
            }
        }
        public Command SaveCommand { get; set; }
        public Command ChooseExerciseCommand { get; set; }
        public AddRoutineViewModel() {
            Title = "Hello";
            Details = new List<RoutineDetail>();
            Routine = new Routine();
            RoutineDetail newRoutineDetail = new RoutineDetail();
            newRoutineDetail.Duration = 30;
            newRoutineDetail.exercise = new Exercise("TEst", "testtype", "Muscle", "body", "easy", "No ins", false);
            newRoutineDetail.ExerciseId = 1;
            newRoutineDetail.RoutineId = Routine.Id;

            Routine.Details = new List<RoutineDetail> { newRoutineDetail };
            Details = Routine.Details;
            Test = new Command(() =>
            {
                RoutineDetail test = new RoutineDetail();
                test.Duration = 30;
                test.exercise = new Exercise("TEst1", "testtype", "Muscle", "body", "easy", "No ins", false);
                test.ExerciseId = 1;
                test.RoutineId = Routine.Id;
                Routine.AddRoutineDetail(test);
                Details.Clear();
                Details = Routine.Details;
                Title = "Test pressed";
            });

            ItemTapped = new Command<Exercise>(OnExerciseSelected);
            SaveCommand = new Command(async () =>
            {
                await RoutineService.AddRoutine(Routine.Name, Routine.TotalDuration, Routine.NumberOfSet, Routine.TotalExercises);
                Routine.Details.ForEach(async d =>
                {
                    await RoutineDetailService.AddRoutineDetail(d.RoutineId, d.ExerciseId, d.Duration);
                });
            });
            ChooseExerciseCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(ChooseExercisePage)}");
            });
            Refresh = new Command(async () =>
            {
                IsRefreshing = true;
                IsBusy = true;
                Exercises = await ExerciseService.GetExercises();
                Details.Clear();
                //Details = Routine.Details;
                IsRefreshing = false;
            });
        }
        private async void OnExerciseSelected(Exercise item)
        {
            if (item == null)
                return;
            string duration = await Application.Current.MainPage.DisplayPromptAsync("Exercise time", "Enter the duration for this exercise (you can change this later):", "OK", "Cancel",placeholder:"30 (s)", keyboard: Keyboard.Numeric);
            if (duration == null) duration = "30";
            RoutineDetail newRoutineDetail = new RoutineDetail();
            newRoutineDetail.Duration = string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30;
            newRoutineDetail.exercise = item;
            newRoutineDetail.ExerciseId = item.Id;
            newRoutineDetail.RoutineId = Routine.Id;

            Routine.AddRoutineDetail(newRoutineDetail);
            Details.Clear();
            Details = Routine.Details;
            await Shell.Current.GoToAsync("..");
        }

        public Command Refresh { get; set; }

        public bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing; set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
    }
}
