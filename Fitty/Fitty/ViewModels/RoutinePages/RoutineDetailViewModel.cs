using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Fitty.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class RoutineDetailViewModel:BaseViewModel
    {
        private int id;
        private string name;
        private int totalDuration;
        private int totalExercises;
        private int numberOfSet;
        private List<RoutineDetail> details;

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
        public int TotalExercises
        { get => totalExercises; set { SetProperty(ref totalExercises, value); } }

        public int NumberOfSet
        { get => numberOfSet; set { SetProperty(ref numberOfSet, value); } }
        public List<RoutineDetail> Details
        { get => details; set { SetProperty(ref details, value); } }


        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await RoutineService.GetRoutine(itemId);
                Name = item.Name;
                TotalDuration = item.TotalDuration;
                TotalExercises = item.TotalExercises;
                NumberOfSet = item.NumberOfSet;
                Details = item.Details;
                Details.ForEach(d=> Debug.WriteLine(d.exercise.Name));
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Routine");
            }
        }
    }
}
