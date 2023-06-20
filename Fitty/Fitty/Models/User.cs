using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Fitty.Models
{

    internal class UserLocal
    {

        private Gender _gender = Gender.Male;
        private string _name = "User";
        private int _age = 25;
        private int _weight = 60; // In kilograms
        private int _height = 170; // In centimeters
        private ActivityLevel _activityLevel = ActivityLevel.littleNoExercise;
        private static List<int> _bookmarkedIds = new List<int>();
        private static string infoPath = Path.Combine(FileSystem.AppDataDirectory, "user_info_base.json");

        public UserLocal()
        {

            _name = "User";
            _gender = Gender.Male;
            _age = 18;
            _weight = 45;
            _height = 170;
            _activityLevel = ActivityLevel.littleNoExercise;
            _bookmarkedIds = new List<int>();

        }

        public UserLocal(int id, string name, Gender gender, int age, int weight, int height, ActivityLevel activityLevel, List<int> bookmarkedIds)
        {

            _name = name;
            _gender = gender;
            _age = age;
            _weight = weight;
            _height = height;
            _activityLevel = activityLevel;
            _bookmarkedIds = bookmarkedIds;

        }

        public Gender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public ActivityLevel ActivityLevel
        {
            get { return _activityLevel; }
            set { _activityLevel = value; }
        }

        public List<int> BookmarkedIds
        {
            get { return _bookmarkedIds; }
            set { _bookmarkedIds = value; }
        }

        public static string GetInfoPath()
        {

            return infoPath;
        }

        public static async Task SaveUserToFile(UserLocal user)
        {
            var filePath = GetInfoPath();

            try
            {
                var jsonData = JsonConvert.SerializeObject(user);

                using (var file = new StreamWriter(filePath))
                {
                    await file.WriteAsync(jsonData);
                }

                Console.WriteLine($"User information saved to file: {filePath}");
                var contentFromFile = File.ReadAllText(filePath);
                Console.WriteLine("Info saved:\n" + contentFromFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Info not saved: " + e.Message);
            }
        }

        public static UserLocal UserFromJson(Dictionary<string, object> json)
        {
            var age = Convert.ToInt32(json["Age"]);
            var weight = Convert.ToInt32(json["Weight"]);
            var height = Convert.ToInt32(json["Height"]);
            var activityLevel = (ActivityLevel)Enum.Parse(typeof(ActivityLevel), json["ActivityLevel"].ToString());
            var gender = (Gender)Enum.Parse(typeof(Gender), json["Gender"].ToString());
            var name = json["Name"].ToString();
            var bookmarkedIds = JsonConvert.DeserializeObject<List<int>>(json["BookmarkedIds"].ToString());

            return new UserLocal()
            {
                Name = name,
                Age = age,
                Weight = weight,
                Height = height,
                ActivityLevel = activityLevel,
                Gender = gender,
                BookmarkedIds = bookmarkedIds
            };
        }

        public static UserLocal GetUserFromFile()
        {
            var filePath = infoPath;
            var file = new StreamReader(filePath);
            var contentFromFile = file.ReadToEnd();
            file.Close();

            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentFromFile);
            return UserFromJson(json);
        }
        public Dictionary<string, object> ToJson()
        {
            return new Dictionary<string, object>
        {
            { "name", _name },
            { "gender", _gender.ToString() },
            { "age", _age },
            { "weight", _weight },
            { "height", _height },
            { "activityLevel", _activityLevel.ToString() },
            { "bookmarkedIds", JsonConvert.SerializeObject(_bookmarkedIds) },
        };
        }

        public void AddBookmark(int id)
        {
            _bookmarkedIds.Add(id);
        }
        public void RemoveBookmark(int id)
        {
            int item = _bookmarkedIds.Find(x => x == id);

            _bookmarkedIds.Remove(item);
        }

    }

}
