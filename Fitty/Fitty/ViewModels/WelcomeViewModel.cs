using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class WelcomeViewModel : BaseViewModel
    {
        public Command startCommand { get; set; }
        public WelcomeViewModel() {
            startCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(Login)}");
            });
        }
    }
}
