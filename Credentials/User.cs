/*************************************************
 * FileName     = User.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Contains all the user data 
 *************************************************/
using Credentials;
using MessageLog;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Credentials
{
    public class User
    {
        public static readonly List<User> UList = new()
        {
            new User("lead1", "password1"),
            new User("lead2", "password2"),
            new User("lead3", "password3"),
            new User("studenthead", "password4"),
            new User("executive", "password5"),
            new User("facultyincharge", "password6")
        };

        public string Position { get; set; }
        public string Password { get; set; }
        public List<MLog.MItem> Log { get; private set; }

        public User(string position, string password)
        {
            Position = position;
            Password = password;
            Log = LoadLogFromFile();
        }

        private List<MLog.MItem> LoadLogFromFile()
        {
            if (File.Exists($"{Position}_log.json"))
            {
                var json = File.ReadAllText($"{Position}_log.json");
                return JsonSerializer.Deserialize<List<MLog.MItem>>(json) ?? new List<MLog.MItem>();
            }
            return new List<MLog.MItem>();
        }

        public void SaveLogToFile()
        {
            var json = JsonSerializer.Serialize(Log);
            File.WriteAllText($"{Position}_log.json", json);
        }
    }
}
