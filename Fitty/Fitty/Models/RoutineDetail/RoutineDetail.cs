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
        public RoutineDetail() { }
        public RoutineDetail(RoutineDetail routineDetail) 
        {
            Id = routineDetail.Id;
            RoutineId = routineDetail.RoutineId;
            Duration = routineDetail.Duration;
            ExerciseId = routineDetail.ExerciseId;
            exercise = routineDetail.exercise;
            ExerciseName = routineDetail.ExerciseName;
            ExerciseInstructions = routineDetail.ExerciseInstructions;
        }
        public RoutineDetail(int routineId, int exerciseId, int duration)
        {
            RoutineId = routineId;
            ExerciseId = exerciseId;
            Duration = duration;
        }
        [Ignore]
        public Exercise exercise { get; set; }
        [Ignore]
        public string ExerciseName { get; set; }
        [Ignore]
        public string ExerciseInstructions { get; set; }
    }
}
