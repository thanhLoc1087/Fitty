namespace Fitty.Models
{
    enum MuscleGroupEnum
    {
        abdominals,
        biceps,
        calves,
        chest,
        forearms,
        glutes,
        hamstrings,
        lats,
        lower_back,
        middle_back,
        neck,
        quadriceps,
        traps,
        tricep,
    }
    class MuscleGroup
    {
        public MuscleGroup(string Name, MuscleGroupEnum Enum, string imagePath)
        {
            this.Name = Name;
            this.Enum = Enum;
            ImagePath = imagePath;
        }
        public string Name { get; set; }
        public MuscleGroupEnum Enum { get; set; }
        public string ImagePath { get; set; }
    }
    enum DifficultyEnum
    {
        beginner,
        intermediate,
        expert,
    }
    enum ExerciseTypeEnum
    {
        cardio,
        olympic_weightlifting,
        plyometrics,
        powerlifting,
        strength,
        stretching,
        strongman,
    }
}
