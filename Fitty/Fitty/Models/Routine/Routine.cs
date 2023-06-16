using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Fitty.Models
{
    class Routine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } // Name of the routine
        public int TotalDuration { get; set; } // Total time to complete the routine (in second)
        public int TotalExercises { get; set; } // Total time to complete the routine (in second)
        public int NumberOfSet { get; set; }
        [Ignore]
        public List<RoutineDetail> Details { get; set; }
        public Routine() { 
            Details = new List<RoutineDetail> { };
        }
        public Routine(string name, int totalDuration, int numberOfSet, int totalExercises = 0)
        {
            Name = name;
            TotalDuration = totalDuration;
            NumberOfSet = numberOfSet;
            TotalExercises = totalExercises;
            Details = new List<RoutineDetail>();
        }
        public override string ToString()
        {
            string ans = "";
            Details.ForEach(d =>
            {
                ans += d.exercise.Name;
            });
            return ans;
        }
        public void AddRoutineDetail(RoutineDetail routineDetail)
        {
            if (Details == null) Details = new List<RoutineDetail>();
            Details.Add(routineDetail);
            TotalDuration += routineDetail.Duration;
            TotalExercises = Details.Count;
            Debug.WriteLine(Details.Count);
        }
        public void RemoveRoutineDetail(RoutineDetail routineDetail)
        {
            if (Details == null) Details = new List<RoutineDetail>();
            Details.Remove(routineDetail);
            TotalDuration -= routineDetail.Duration;
            TotalExercises = Details.Count;
        }
    }
}
