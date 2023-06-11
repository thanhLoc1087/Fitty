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
            return routineDetails.Count == 1 ? routineDetails[0] : null;
        }
        public static async Task<List<RoutineDetail>> GetRoutineDetails()
        {
            await ExerciseAPIService.Init();
            var routineDetails = await ExerciseAPIService.db.Table<RoutineDetail>().ToListAsync();
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
    }
}
