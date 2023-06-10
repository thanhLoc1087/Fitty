using Fitty.Services;
using Fitty.Views;
using Fitty;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
