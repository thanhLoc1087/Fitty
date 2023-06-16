using Fitty.Models;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace Fitty.ViewModels
{
    internal class AddRoutineViewModel:BaseViewModel
    {
        public Command<Exercise> ItemTapped { get; }
        public Command Test { get; }
        Routine _routine;
        public Routine Routine { get { return _routine; } set
            {
                Debug.Write("routine changed");
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
        ObservableCollection<RoutineDetail> _details;
        public ObservableCollection<RoutineDetail> Details
        {
            get => _details;
            set
            {
                Debug.Write("details changed");
                SetProperty(ref _details, value);
            }
        }
        public Command SaveCommand { get; set; }
        public Command<RoutineDetail> RemoveExercise { get; set; }
        public Command ChooseExerciseCommand { get; set; }
        public AddRoutineViewModel() {
            Title = "New routine";
            Details = new ObservableCollection<RoutineDetail>();
            Routine = new Routine();

            AddRestCommand = new Command(async () =>
            {
                string duration = await Application.Current.MainPage.DisplayPromptAsync("Rest time", "you can change this later:", "OK", "Cancel", placeholder: "30 (s)", keyboard: Keyboard.Numeric);
                if (duration == null) return;
                RoutineDetail rest = new RoutineDetail
                {
                    Duration = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30
                };
                var restExercise = await ExerciseService.GetExerciseByName("rest");
                if (restExercise == null)
                {
                    Debug.WriteLine("rest null");
                    return;
                }
                rest.exercise = restExercise;
                rest.ExerciseId = restExercise.Id;

                Routine.AddRoutineDetail(rest);
                Details.Add(rest);
                Debug.WriteLine("rest added");
            });

            ItemTapped = new Command<Exercise>(OnExerciseSelected);
            RemoveExercise = new Command<RoutineDetail>(HandleRemoveExercise);

            SaveCommand = new Command(async () =>
            {
                if (Routine.Details.Count <= 0) { 
                    await Application.Current.MainPage.DisplayAlert("Error", "Your routine must have at least one exercise.", "OK");
                    return; 
                }
                if (Routine.NumberOfSet <= 0) Routine.NumberOfSet = 1;
                var routineId = await RoutineService.AddRoutine(Routine.Name, Routine.TotalDuration, Routine.NumberOfSet, Routine.TotalExercises);
                Routine.Details.ForEach(async d =>
                {
                    Debug.WriteLine(d.exercise.Name);
                    await RoutineDetailService.AddRoutineDetail(routineId, d.ExerciseId, d.Duration);
                });
                Debug.WriteLine(Routine.ToString());
                Routine = new Routine();
                Details = new ObservableCollection<RoutineDetail>();
                await Shell.Current.GoToAsync("..");
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
                IsRefreshing = false;
            });
        }
        private async void OnExerciseSelected(Exercise item)
        {
            if (item == null)
                return;
            string duration = await Application.Current.MainPage.DisplayPromptAsync("Exercise time", "Enter the duration for this exercise (you can change this later):", "OK", "Cancel",placeholder:"30 (s)", keyboard: Keyboard.Numeric);
            if (duration == null) return;
            RoutineDetail newRoutineDetail = new RoutineDetail
            {
                Duration = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30,
                ExerciseId = item.Id,
                exercise = item,
            };

                Debug.Write(Routine.Details.Count);
            Routine.AddRoutineDetail(newRoutineDetail);
                Debug.Write(Routine.Details.Count);
            Details.Add(newRoutineDetail);
            await Shell.Current.GoToAsync("..");
        }

        public Command Refresh { get; set; }
        public Command AddRestCommand { get; set; }

        public bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing; set
            {
                SetProperty(ref _isRefreshing, value);
            }
        }
        private void HandleRemoveExercise(RoutineDetail item)
        {
            if (item == null) return;

            Routine.RemoveRoutineDetail(item); 
            Details.Remove(item);
        }
    }
}
