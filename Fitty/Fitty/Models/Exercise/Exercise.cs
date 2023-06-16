using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fitty.Models
{
    internal class Exercise
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Muscle { get; set; }
        public string Equipment { get; set; }
        public string Difficulty { get; set; }
        public string Instructions { get; set; }
        public bool UserCreated { get; set; }
        public Exercise() { }
        public Exercise(string name, string type, string muscle, string equipment, string difficulty, string instructions, bool userCreated)
        {
            Name = name;
            Type = type;
            Muscle = muscle;
            Equipment = equipment;
            Difficulty = difficulty;
            Instructions = instructions;
            UserCreated = userCreated;
        }
        public override string ToString() => Name;
    }
}
