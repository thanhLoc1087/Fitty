using Fitty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitty.Services
{
    internal class RoutineDataSource : IDataStore<Routine>
    {
        public List<Routine> routines;
        public RoutineDataSource()
        {
            routines = new List<Routine>();
        }
        public async Task<bool> AddItemAsync(Routine item)
        {
            routines.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string name)
        {
            var oldItem = routines.Where((Routine arg) => arg.Name == name).FirstOrDefault();
            routines.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Routine> GetItemAsync(string name)
        {
            return await Task.FromResult(routines.FirstOrDefault(e => e.Name == name));
        }
        public async Task<Routine> GetItemAsyncById(int id)
        {
            return await Task.FromResult(
                await RoutineService.GetRoutine(id)
                );
        }

        public async Task<IEnumerable<Routine>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(routines);
        }

        public async Task<bool> UpdateItemAsync(Routine item)
        {
            var oldItem = routines.Where((Routine arg) => arg.Id == item.Id).FirstOrDefault();
            routines.Remove(oldItem);
            routines.Add(item);
            return await Task.FromResult(true);
        }
    }
}
