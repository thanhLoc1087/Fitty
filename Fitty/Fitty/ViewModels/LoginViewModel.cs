using Fitty.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        string _name;
        public string Name
        {
            get => _name;
            set {
                SetProperty(ref _name, value);;
            }
        }
        public Command loginCommand { get; set; }
        public LoginViewModel() {
            loginCommand = new Command(async () =>
            {
                var name = string.IsNullOrEmpty(Name) ? "User" : Name;
                await Shell.Current.GoToAsync($"{nameof(HomePage)}?{nameof(HomeViewModel.Name)}={Name}");
            });
        }
    }
}
