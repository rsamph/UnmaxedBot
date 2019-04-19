using Newtonsoft.Json;
using System.IO;

namespace UnmaxedBot.Core.Data
{
    public class JsonFileStore : IObjectStore
    {
        private readonly string _dataDirectory = "Data";

        public JsonFileStore()
        {
            if (!Directory.Exists(_dataDirectory))
                Directory.CreateDirectory(_dataDirectory);
        }

        public void SaveObject(object obj, string key)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var filePath = GetFilePath(key);
            File.WriteAllText(filePath, json);
        }

        public T LoadObject<T>(string key)
        {
            var filePath = GetFilePath(key);
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public bool KeyExists(string key)
        {
            var filePath = GetFilePath(key);
            return File.Exists(filePath);
        }

        private string GetFilePath(string key)
        {
            return $@"{_dataDirectory}\{key}.json";
        }
    }
}
