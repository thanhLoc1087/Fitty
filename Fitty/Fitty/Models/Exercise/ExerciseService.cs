using Fitty.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Fitty.Models
{
    class ExerciseService
    {
        public static async Task<Exercise> GetExercise(int id)
        {
            await ExerciseAPIService.Init();
            var exercise = (await ExerciseAPIService.db.Table<Exercise>().Where(e => e.Id == id).ToListAsync()).FirstOrDefault();
            return exercise;
        }
        public static async Task<List<Exercise>> GetExercisesByMuscle(string muscle)
        {
            await ExerciseAPIService.Init();
            var exercises = await ExerciseAPIService.db.Table<Exercise>().Where(e => e.Muscle == muscle).ToListAsync();
            return exercises;
        }
        public static async Task<List<Exercise>> GetExercises()
        {
            await ExerciseAPIService.Init();
            var exercises = await ExerciseAPIService.db.Table<Exercise>().ToListAsync();
            return exercises;
        }
        public static async Task AddExercise(string name, string type, string muscle, string equipment, string difficulty, string instructions, bool userCreated)
        {
            await ExerciseAPIService.Init();
            var exercise = new Exercise(name,type,muscle,equipment,difficulty,instructions,userCreated);
            await ExerciseAPIService.db.InsertAsync(exercise);
        }
        public static async Task UpdateExercise(Exercise exercise)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.UpdateAsync(exercise);
        }
        public static async Task RemoveExercise(int id)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.DeleteAsync<Exercise>(id);
        }

        internal static async Task<Exercise> GetExerciseByName(string name)
        {
            await ExerciseAPIService.Init();
            name = name.Trim().ToLower();
            var exercise = (await ExerciseAPIService.db.Table<Exercise>().Where(e => e.Name.ToLower().Contains(name)).ToListAsync()).FirstOrDefault();
            return exercise;
        }
    }
}
