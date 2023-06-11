using Fitty.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fitty.Models
{
    class RoutineService
    {
        public static async Task<Routine> GetExercise(int id)
        {
            await ExerciseAPIService.Init();
            var routines = await ExerciseAPIService.db.Table<Routine>().Where(e => e.Id == id).ToListAsync();
            return routines.Count == 1 ? routines[0] : null;
        }
        public static async Task<List<Routine>> GetRoutines()
        {
            await ExerciseAPIService.Init();
            var routines = await ExerciseAPIService.db.Table<Routine>().ToListAsync();
            return routines;
        }
        public static async Task AddRoutine(string name, int totalDuration, int numberOfSet)
        {
            await ExerciseAPIService.Init();
            var routine = new Routine(name, totalDuration, numberOfSet);
            await ExerciseAPIService.db.InsertAsync(routine);
        }
        public static async Task UpdateRoutine(Routine routine)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.UpdateAsync(routine);
        }
        public static async Task RemoveRoutine(int id)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.DeleteAsync<Routine>(id);
        }
    }
}
