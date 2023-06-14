using Fitty_Xamarin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [assembly: Dependency(typeof(NotificationHelper))]
    public partial class HydrationAlarm : ContentPage
    {
        private int _intervalMinutes;
        private bool _isRunning;
        private CancellationTokenSource _cancellationTokenSource;

        public HydrationAlarm()
        {
            InitializeComponent();
            _intervalMinutes = Convert.ToInt32(intervalPicker.SelectedItem);
        }

        private void OnIntervalPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            _intervalMinutes = Convert.ToInt32(intervalPicker.SelectedItem);
        }

        private void OnStartButtonClicked(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                // Stop the reminder
                StopReminder();
            }
            else
            {
                // Start the reminder
                _cancellationTokenSource = new CancellationTokenSource();
                var cancellationToken = _cancellationTokenSource.Token;

                Task.Run(async () =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(_intervalMinutes));
                        SendNotification();
                    }
                }, cancellationToken);

                startButton.Text = "Stop";
                intervalPicker.IsEnabled = false;
                _isRunning = true;
            }
        }

        private void OnResetButtonClicked(object sender, EventArgs e)
        {
            StopReminder();
            _isRunning = false;
            startButton.Text = "Start";
            intervalPicker.IsEnabled = true;
        }

        private void StopReminder()
        {
            _cancellationTokenSource?.Cancel();
        }

        private void SendNotification()
        {
            var message = "It's time to drink some water!";

            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Water Reminder", message, "OK");
            });

            DependencyService.Get<INotification>().CreateNotification("Water Reminder", message);
        }

    }
}