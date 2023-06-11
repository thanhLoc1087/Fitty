using SQLite;
using System;

namespace Fitty.Models
{
    class Routine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } // Name of the routine
        public int TotalDuration { get; set; } // Total time to complete the routine (in second)
        public int NumberOfSet { get; set; }
        public Routine() { }
        public Routine(string name, int totalDuration, int numberOfSet)
        {
            Name = name;
            TotalDuration = totalDuration;
            NumberOfSet = numberOfSet;
        }
    }
}
