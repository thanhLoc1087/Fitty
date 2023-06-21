using Fitty.Models;
using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace Fitty.ViewModels
{
    internal class AddRoutineViewModel : BaseViewModel
    {
        public Command<Exercise> ItemTapped { get; }

        Routine _routine;
        public Routine Routine
        {
            get { return _routine; }
            set
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
                SetProperty(ref _details, value);
            }
        }
        public Command SaveCommand { get; set; }
        public Command<RoutineDetail> RemoveExercise { get; set; }
        public Command ChooseExerciseCommand { get; set; }
        public AddRoutineViewModel()
        {
            Title = "New routine";
            Details = new ObservableCollection<RoutineDetail>();
            Routine = new Routine();
            Routine.NumberOfSet = 1;

            AddRestCommand = new Command(async () =>
            {
                string duration = await Application.Current.MainPage.DisplayPromptAsync("Rest time", "you can change this later:", "OK", "Cancel", placeholder: "30 (s)", keyboard: Keyboard.Numeric);
                if (duration == null) return;
                Debug.WriteLine(duration);
                var input = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30;
                while (input < 5)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Your exercise duration must be at least 5 secs.", "Ok");
                    duration = await Application.Current.MainPage.DisplayPromptAsync("Exercise time", "Enter the duration for this exercise (you can change this later):", "OK", "Cancel", placeholder: "30 (s)", keyboard: Keyboard.Numeric);
                    if (duration == null) return;
                    input = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30;
                }
                RoutineDetail rest = new RoutineDetail
                {
                    Duration = input
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
                int routineId;
                if (Routine.Details.Count <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Your routine must have at least one exercise.", "OK");
                    return;
                }
                if (Routine.NumberOfSet <= 0) Routine.NumberOfSet = 1;
                Routine.Name = !string.IsNullOrEmpty(Routine.Name) ? Routine.Name.Trim() : "My routine";
                    routineId = await RoutineService.AddRoutine(Routine.Name, Routine.TotalDuration, Routine.NumberOfSet, Routine.TotalExercises);
                foreach(var d in Routine.Details.Select((value, index) => new { index,value }))
                {
                    await RoutineDetailService.AddRoutineDetail(routineId, d.value.ExerciseId, d.value.Duration, d.index);
                }
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
            string duration = await Application.Current.MainPage.DisplayPromptAsync("Exercise time", "Enter the duration for this exercise (you can change this later):", "OK", "Cancel", placeholder: "30 (s)", keyboard: Keyboard.Numeric);
            if (duration == null) return;
            var input = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30;
            while (input < 5)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Your exercise duration must be at least 5 secs.", "Ok");
                duration = await Application.Current.MainPage.DisplayPromptAsync("Exercise time", "Enter the duration for this exercise (you can change this later):", "OK", "Cancel", placeholder: "30 (s)", keyboard: Keyboard.Numeric);
                if (duration == null) return;
                input = !string.IsNullOrEmpty(duration) ? int.Parse(duration) : 30;
            }
            RoutineDetail newRoutineDetail = new RoutineDetail
            {
                Duration = input,
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