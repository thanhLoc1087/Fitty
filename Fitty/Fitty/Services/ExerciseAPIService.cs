using Fitty.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fitty.Services
{
    internal class ExerciseAPIService
    {
        public static SQLiteAsyncConnection db;
        public static bool read = false;
        public static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            Debug.WriteLine("Int db");

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fitty.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Exercise>();
            await db.CreateTableAsync<Routine>();
            await db.CreateTableAsync<RoutineDetail>();
            //ReadJsonFile();
        }
        static public List<Exercise> ReadJsonFile()
        {
            bool read = Preferences.ContainsKey("read_json");
            string jsonString;
            string jsonFileName = "Configs.exercises.json";
            var assembly = typeof(AppShell).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }
            List<Exercise> exercises = JsonConvert.DeserializeObject<List<Exercise>>(jsonString);

            if (read)
                return exercises;
            Debug.WriteLine("Made it to 1");
            exercises.ForEach(async exercise =>
            {
                await ExerciseService.AddExercise(
                    exercise.Name,
                    exercise.Type,
                    exercise.Muscle,
                    exercise.Equipment,
                    exercise.Difficulty,
                    exercise.Instructions,
                    false);
            });
            read = true;
            Preferences.Set("read_json", true);
            return exercises;
        }
    }
}
