using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fitty.Models
{
    class RoutineDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int RoutineId { get; set; }
        [Indexed]
        public int ExerciseId { get; set; }
        public int Duration { get; set; }
        public int Index { get; set; }
        [Ignore]
        public Exercise exercise { get; set; }
        public RoutineDetail() { }
        public RoutineDetail(RoutineDetail routineDetail) 
        {
            Id = routineDetail.Id;
            RoutineId = routineDetail.RoutineId;
            Duration = routineDetail.Duration;
            ExerciseId = routineDetail.ExerciseId;
            exercise = routineDetail.exercise;
            Duration = routineDetail.Duration;
        }
        public RoutineDetail(int routineId, int exerciseId, int duration, int index)
        {
            RoutineId = routineId;
            ExerciseId = exerciseId;
            Duration = duration;
            Index = index;
        }
    }
}
