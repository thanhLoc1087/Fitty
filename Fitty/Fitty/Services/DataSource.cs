using Fitty.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitty.Services
{
    internal class DataSource : IDataStore<Exercise>
    {
        public List<Exercise> exercises;
        public ExerciseAPIService exerciseAPIService;
        public DataSource() 
        {
            exerciseAPIService = new ExerciseAPIService();
            exercises = exerciseAPIService.ReadJsonFile();
        }
        public async Task<bool> AddItemAsync(Exercise item)
        {
            exercises.Add(item);
            return await Task.FromResult(true);
        }
        
        public async Task<bool> DeleteItemAsync(string name)
        {
            var oldItem = exercises.Where((Exercise arg) => arg.Name == name).FirstOrDefault();
            exercises.Remove(oldItem);
            return await Task.FromResult(true);
        }

        public async Task<Exercise> GetItemAsync(string name)
        {
            return await Task.FromResult(exercises.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Exercise>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(exercises);
        }

        public async Task<bool> UpdateItemAsync(Exercise item)
        {
            var oldItem = exercises.Where((Exercise arg) => arg.Name == item.Name).FirstOrDefault();
            exercises.Remove(oldItem);
            exercises.Add(item);
            return await Task.FromResult(true);
        }
    }
}
