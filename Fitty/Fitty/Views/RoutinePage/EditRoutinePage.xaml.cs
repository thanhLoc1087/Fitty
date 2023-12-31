﻿using Fitty.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fitty.Views.RoutinePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditRoutinePage : ContentPage
    {
        EditRoutineViewModel _viewModel;
        public EditRoutinePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = RoutinesViewModel.editRoutineViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Task.Delay(200);
            _viewModel.Details = new ObservableCollection<Models.RoutineDetail>(_viewModel.Routine.Details);
        }
    }
}