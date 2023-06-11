using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class WelcomeViewModel : BaseViewModel
    {
        public Command LoginCommand { get; set; }
        public Command StartCommand { get; set; }
        public WelcomeViewModel() {
            StartCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(Login)}");
            });
            LoginCommand = new Command(async () =>
            {
                var name = string.IsNullOrEmpty(Name) ? "User" : Name;
                await Shell.Current.GoToAsync($"{nameof(HomePage)}?{nameof(HomeViewModel.Name)}={Name}");
            });
        }
        string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value); ;
            }
        }
    }
}
