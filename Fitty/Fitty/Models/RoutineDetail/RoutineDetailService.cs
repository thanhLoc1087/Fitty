using Fitty.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fitty.Models
{
    class RoutineDetailService
    {
        public static async Task<RoutineDetail> GetRoutineDetail(int id)
        {
            await ExerciseAPIService.Init();
            var routineDetails = await ExerciseAPIService.db.Table<RoutineDetail>().Where(e => e.Id == id).ToListAsync();
            routineDetails.ForEach(async rd =>
            {
                rd.exercise = await ExerciseService.GetExercise(rd.ExerciseId);
            });
            return routineDetails.Count == 1 ? routineDetails[0] : null;
        }
        public static async Task<List<RoutineDetail>> GetRoutineDetails()
        {
            await ExerciseAPIService.Init();
            var routineDetails = await ExerciseAPIService.db.Table<RoutineDetail>().ToListAsync();
            routineDetails.ForEach(async rd =>
            {
                rd.exercise = await ExerciseService.GetExercise(rd.ExerciseId);
            });
            return routineDetails;
        }
        public static async Task AddRoutineDetail(int routineId, int exerciseId, int duration)
        {
            await ExerciseAPIService.Init();
            var routineDetail = new RoutineDetail(routineId, exerciseId, duration);
            await ExerciseAPIService.db.InsertAsync(routineDetail);
        }
        public static async Task UpdateRoutineDetail(RoutineDetail routineDetail)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.UpdateAsync(routineDetail);
        }
        public static async Task RemoveRoutineDetail(int id)
        {
            await ExerciseAPIService.Init();
            await ExerciseAPIService.db.DeleteAsync<RoutineDetail>(id);
        }

        public static async Task<List<RoutineDetail>> GetRoutineDetailsByRoutine(int id)
        {
            await ExerciseAPIService.Init();
            var routineDetails = await ExerciseAPIService.db.Table<RoutineDetail>().Where(rd => rd.RoutineId == id).ToListAsync();
            routineDetails.ForEach(async rd =>
            {
                rd.exercise = await ExerciseService.GetExercise(rd.ExerciseId);
            });
            return routineDetails;
        }
    }
}
