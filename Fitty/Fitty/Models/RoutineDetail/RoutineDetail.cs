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
        public RoutineDetail(int routineId, int exerciseId, int duration)
        {
            RoutineId = routineId;
            ExerciseId = exerciseId;
            Duration = duration;
        }
        [Ignore]
        public Exercise exercise { get; set; }
    }
}
