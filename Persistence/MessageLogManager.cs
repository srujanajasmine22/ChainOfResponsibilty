using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using MessageLog;

namespace LogManager
{
    public static class MessageLogManager
    {
        public static void LogMessage(string position, MLog.MItem message)
        {
            var log = LoadLog(position);
            log.Add(message);
            SaveLog(position, log);
        }

        public static void UpdateMessage(string position, MLog.MItem message)
        {
            var log = LoadLog(position);
            var existingMessage = log.Find(m => m.Description == message.Description);
            if (existingMessage != null)
            {
                existingMessage.Status = message.Status;
                existingMessage.Remarks = message.Remarks;
                SaveLog(position, log);
            }
        }

        private static List<MLog.MItem> LoadLog(string position)
        {
            if (File.Exists($"{position}_log.json"))
            {
                var json = File.ReadAllText($"{position}_log.json");
                return JsonSerializer.Deserialize<List<MLog.MItem>>(json) ?? new List<MLog.MItem>();
            }
            return new List<MLog.MItem>();
        }

        private static void SaveLog(string position, List<MLog.MItem> log)
        {
            var json = JsonSerializer.Serialize(log);
            File.WriteAllText($"{position}_log.json", json);
        }
    }
}
