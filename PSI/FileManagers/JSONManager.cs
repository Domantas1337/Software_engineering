using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;
using PSI.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PSI.FileManagers
{
    public static class JSONManager
    {
        private static JsonSerializerOptions _jsonSerializerOptions = new() {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        public static List<T> DeserializeFromJSONString<T>(string json)
        {
            var items = JsonConvert.DeserializeObject<List<T>>(json)
                ?? new();
            return items;
        }
        public static string SerializeToJSONString<T>(T obj)
        {
            string jsonString = JsonSerializer.Serialize<T>(obj, _jsonSerializerOptions);
            return jsonString;
        }
        public static List<T> Read<T>(string filePath)
        {
            
            List<T> items;
            
            if (File.Exists(filePath) == false)
            {
                using FileStream createStream = File.Create(filePath);
                createStream.Dispose();
            }
            StreamReader readStream = new (filePath);
            lock(readStream)
            {
                string json = readStream.ReadToEnd();
                Debug.WriteLine($"Read from {filePath}");
                readStream.Close();
                items = DeserializeFromJSONString<T>(json);
            }
            return items;
        }
        public static async Task WriteAsync<T>(string filePath, T item = default, List<T> items = null)
        {
            if (items == null)
            {
                items = Read<T>(filePath);
                items.Add(item);
            }

            using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, items);
            await createStream.DisposeAsync();
        }
    }
}
