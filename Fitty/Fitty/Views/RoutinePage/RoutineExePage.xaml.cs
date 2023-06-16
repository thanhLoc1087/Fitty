﻿using Fitty.ViewModels;
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
        public RoutineExePage()
        {
            InitializeComponent();
            BindingContext = new RoutineExeViewModel();
        }
    }
}