using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Timers;
using System.Diagnostics;

namespace Fitty.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HydrationAlarm : ContentPage
    {
        private Timer timer;
        private int intervalMinutes;
        public HydrationAlarm()
        {
            InitializeComponent();
        }
        private void SendNotification(object sender, EventArgs e)
        {
            if (!int.TryParse(interval.Text, out intervalMinutes))
            {
                DisplayAlert("Error", "Please enter a valid interval", "OK");
                return;
            }

            string message = "Don't forget to drink water!";
            DependencyService.Get<INotification>().CreateNotification("Fitty", message);

            if (timer != null)
            {
                timer.Stop();
            }
            // create and start timer
            timer = new Timer(TimeSpan.FromMinutes(intervalMinutes).TotalMilliseconds);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            // change button text
            sendButton.Text = "Update interval";
            sendButton.Clicked -= SendNotification;
            sendButton.Clicked += UpdateInterval;

            // display reminder text
            remindLabel.Text = "Remind every " + intervalMinutes + " minutes";
        }

        private void UpdateInterval(object sender, EventArgs e)
        {
            if (!int.TryParse(interval.Text, out intervalMinutes))
            {
                DisplayAlert("Error", "Please enter a valid interval", "OK");
                return;
            }

            if (timer != null)
            {
                timer.Stop();
            }
            // create and start timer
            timer = new Timer(TimeSpan.FromMinutes(intervalMinutes).TotalMilliseconds);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            // display reminder text
            remindLabel.Text = "Remind every " + intervalMinutes + " minutes";
        }

        private void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string message = "Don't forget to drink water!";
            DependencyService.Get<INotification>().CreateNotification("Fitty", message);
        }
    }
}