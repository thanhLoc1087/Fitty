using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutineExePage : ContentPage
    {
        RoutineExeViewModel _viewModel;
        public RoutineExePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RoutineExeViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(300);
            _viewModel.Reset();
        }
    }
}