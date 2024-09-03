/*************************************************
 * FileName     = MessageLogManager.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Used for saving, updating and retreiving requests for particular user 
 *************************************************/
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MessageLog
{
    public static class MessageLogManager
    {
        private static readonly string DirectoryPath = "Logs";
        private static readonly string FilePath = Path.Combine(DirectoryPath, "messageLogs.json");    // all requests are saved in this messageLogs.json file

        private static Dictionary<string, List<MLog.MItem>> _log = new Dictionary<string, List<MLog.MItem>>();

        static MessageLogManager()
        {
            // Load logs from file on startup
            LoadLogs();
        }
        /// <summary>
        /// used to save requests using SaveLogs
        /// </summary>
        /// <param name="userPosition"></param>
        /// <param name="message"></param>
        public static void LogMessage(string userPosition, MLog.MItem message)
        {
            if (!_log.ContainsKey(userPosition))
            {
                _log[userPosition] = new List<MLog.MItem>();
            }
            _log[userPosition].Add(message);
            SaveLogs();
        }
        /// <summary>
        /// Updating requests
        /// </summary>
        /// <param name="userPosition"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void UpdateMessage(string userPosition, MLog.MItem message)
        {
            // Check if userPosition and message are not null
            if (string.IsNullOrEmpty(userPosition))
            {
                throw new ArgumentNullException(nameof(userPosition), "User position cannot be null or empty.");
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "Message cannot be null.");
            }

            // Normalize the userPosition key
            var normalizedUserPosition = userPosition.ToLower();

            // Check if the log contains the normalized user position
            if (_log.ContainsKey(normalizedUserPosition))
            {
                // Find the message to update
                var existing = _log[normalizedUserPosition].FirstOrDefault(m => m.Description == message.Description);
                if (existing != null)
                {
                    // Update the message details
                    existing.Status = message.Status;
                    existing.Remarks = message.Remarks;
                    SaveLogs();
                }
            }
            else
            {
                Console.WriteLine("User position not found in the log.");
            }
        }

        /// <summary>
        /// used to load requests from the file
        /// </summary>
        private static void LoadLogs()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                _log = JsonConvert.DeserializeObject<Dictionary<string, List<MLog.MItem>>>(json)
                       ?? new Dictionary<string, List<MLog.MItem>>();
            }
            else
            {
                _log = new Dictionary<string, List<MLog.MItem>>();
            }
        }
        /// <summary>
        /// used to get messages for particular user
        /// </summary>
        /// <param name="userPosition"></param>
        /// <returns></returns>
        public static List<MLog.MItem> GetMessages(string userPosition)
        {
            var key = userPosition.ToLower();
            return _log.ContainsKey(key) ? _log[key] : new List<MLog.MItem>();
        }
        /// <summary>
        /// used to save requests
        /// </summary>
        private static void SaveLogs()
        {
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            var json = JsonConvert.SerializeObject(_log, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}

