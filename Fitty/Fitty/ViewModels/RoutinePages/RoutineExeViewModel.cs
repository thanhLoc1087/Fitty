using Fitty.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class RoutineExeViewModel : BaseViewModel
    {
        public RoutineExeViewModel() {
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // Interval of 1 second (1000 milliseconds)
            countdownTimer.Elapsed += CountdownTimerElapsed;
            countdownTimer.AutoReset = true;
            StartCommand = new Command(HandleStartCommand);
            PauseCommand = new Command(HandlePauseCommand);
            SkipCommand = new Command(HandleSkipCommand);
            IsNotRunning = true;
            StartRestart = "Start";
        }
        private int id;
        private string name;
        private int totalDuration;
        private int timeRemaining;
        private int totalExercises;
        private int numberOfSet;
        private int remainingSet;
        private Timer countdownTimer;
        private int remainingTimeInSeconds;
        private RoutineDetail currentDetail;
        private ObservableCollection<RoutineDetail> details;
        private ObservableCollection<RoutineDetail> ogDetails;
        private string _startRestart;
        private bool _isRunning;
        private bool _isNotRunning;
        public string StartRestart { get { return _startRestart; } set { SetProperty(ref _startRestart, value); } }
        public bool IsRunning { get { return _isRunning; } set { SetProperty(ref _isRunning, value); } }
        public bool IsNotRunning { get { return _isNotRunning; } set { SetProperty(ref _isNotRunning, value); } }

        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
                LoadItemId(value);
            }
        }
        public string Name
        { get => name; set { SetProperty(ref name, value); } }
        public int TotalDuration
        { get => totalDuration; set { SetProperty(ref totalDuration, value); } }
        public int TimeRemaining
        { get => timeRemaining; set { SetProperty(ref timeRemaining, value); } }
        public int RemainingTimeInSeconds
        { get => remainingTimeInSeconds; set { SetProperty(ref remainingTimeInSeconds, value); } }
        public int TotalExercises
        { get => totalExercises; set { SetProperty(ref totalExercises, value); } }

        public int NumberOfSet
        { get => numberOfSet; set { SetProperty(ref numberOfSet, value); } }
        public int RemainingSet
        { get => remainingSet; set { SetProperty(ref remainingSet, value); } }
        public ObservableCollection<RoutineDetail> Details
        { get => details; set { SetProperty(ref details, value); } }
        public RoutineDetail CurrentDetail
        { get => currentDetail; set { SetProperty(ref currentDetail, value); } }
        public Command StartCommand { get; set; }
        public Command PauseCommand { get; set; }
        public Command SkipCommand { get; set; }


        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await RoutineService.GetRoutine(itemId);
                if (item != null)
                {
                    Title = Name = item.Name;
                    TotalDuration = item.TotalDuration;
                    TotalExercises = item.TotalExercises;
                    NumberOfSet = item.NumberOfSet;
                    ogDetails = new ObservableCollection<RoutineDetail>(item.Details);
                    Reset();
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Routine");
            }
        }

        private void CountdownTimerElapsed(object sender, ElapsedEventArgs e)
        {
            RemainingTimeInSeconds--; // Decrease the remaining time by 1 second
            TimeRemaining--;
            if (RemainingTimeInSeconds== 1 ||
                RemainingTimeInSeconds == 2 ||
                RemainingTimeInSeconds == 3) {
                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("beep_sound_fx.mp3");
                });
            }

            // Check if the timer reached 0
            if (RemainingTimeInSeconds <= 0)
            {
                // Stop the timer
                countdownTimer.Stop();
                IsRunning = false;
                IsNotRunning = true;

                Device.BeginInvokeOnMainThread(() =>
                {
                    DependencyService.Get<IAudio>().PlayAudioFile("beep_heavy_fx.mp3");
                });

                // Display an alert or perform any other action when the timer reaches 0
                if (Details.Count > 0) 
                {
                    CurrentDetail = Details[0];
                    RemainingTimeInSeconds = CurrentDetail.Duration;
                    countdownTimer.Start();
                    IsRunning = true;
                    IsNotRunning = false;
                    Details.RemoveAt(0);
                }
            }
            if (Details.Count == 0 && RemainingTimeInSeconds <= 0 && RemainingSet <= 1)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Finished", "You have completed your work out.", "OK");
                });
                Reset();
                StartRestart = "Restart";
            }
            if (Details.Count == 0 && RemainingTimeInSeconds <= 0 && RemainingSet > 1)
            {
                RemainingSet--;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Congrats", "You have completed your set.\nGet ready for your next one.", "OK");
                });
                Reset();
            }
        }
        private void HandleStartCommand()
        {
            if (StartRestart == "Restart")
            {
                StartRestart = "Start";
            }
            countdownTimer.Start();
            IsRunning = true;
            IsNotRunning = false;
        }
        private void HandlePauseCommand()
        {
            countdownTimer.Stop();
            IsRunning = false;
            IsNotRunning = true;
        }
        private void HandleSkipCommand()
        {
            countdownTimer.Stop();
            IsNotRunning = true;
            IsRunning = false;
            if (Details.Count > 0)
            {
                CurrentDetail = Details[0];
                TimeRemaining -= RemainingTimeInSeconds;
                RemainingTimeInSeconds = CurrentDetail.Duration;
                Details.RemoveAt(0);
            } 
        }
        public void Reset()
        {
            countdownTimer.Stop();
            IsRunning = false;
            IsNotRunning = true;
            Details = new ObservableCollection<RoutineDetail>(ogDetails);
            TimeRemaining = TotalDuration;
            RemainingSet = NumberOfSet;
            CurrentDetail = new RoutineDetail(Details[0]);
            RemainingTimeInSeconds = CurrentDetail.Duration;
            Details.RemoveAt(0);
        }
    }
}
