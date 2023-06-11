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
using Xamarin.Forms;

namespace Fitty.Services
{
    internal class ExerciseAPIService
    {
        public static SQLiteAsyncConnection db;
        public static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Fitty.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Exercise>();
            await db.CreateTableAsync<Routine>();
            await db.CreateTableAsync<RoutineDetail>();
        }
        public List<Exercise> ReadJsonFile()
        {
            string jsonString;
            string jsonFileName = "Configs.exercises.json";
            var assembly = typeof(AppShell).GetTypeInfo().Assembly;
            Debug.WriteLine("Made it to 1");

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            using (var reader = new StreamReader(stream))
            {
                jsonString = reader.ReadToEnd();
            }
            List<Exercise> exercises = JsonConvert.DeserializeObject<List<Exercise>>(jsonString);

            return exercises;
        }
    }
}
