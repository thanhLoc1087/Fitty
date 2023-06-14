using Fitty.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitty.Models
{
    class RoutineService
    {
        public static async Task<Routine> GetRoutine(int id)
        {
            await ExerciseAPIService.Init();
            var routine = (await ExerciseAPIService.db.Table<Routine>().Where(e => e.Id == id).ToListAsync()).FirstOrDefault();
            if (routine != null)
            {
                routine.Details = await RoutineDetailService.GetRoutineDetailsByRoutine(id);
            }
            return routine;
        }
        public static async Task<List<Routine>> GetRoutines()
        {
            await ExerciseAPIService.Init();
            var routines = await ExerciseAPIService.db.Table<Routine>().ToListAsync();
            foreach ( var routine in routines)
            {
                routine.Details = await RoutineDetailService.GetRoutineDetailsByRoutine(routine.Id);
            }
            return routines;
        }
        public static async Task<int> AddRoutine(string name, int totalDuration, int numberOfSet, int totalExercises = 0)
        {
            await ExerciseAPIService.Init();
            var routine = new Routine(name, totalDuration, numberOfSet, totalExercises);
            await ExerciseAPIService.db.InsertAsync(routine);
            return routine.Id;
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
