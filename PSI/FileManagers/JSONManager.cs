using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;
using PSI.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PSI.FileManagers
{
    internal static class JSONManager
    {
        public static List<T> DeserializeFromJSONString<T>(string json)
        {
            var items = JsonConvert.DeserializeObject<List<T>>(json)
                ?? new();
            return items;
        }
        public static string SerializeToJSONString<T>(T obj)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string jsonString = JsonSerializer.Serialize<T>(obj, jsonSerializerOptions);
            return jsonString;
        }
        public static List<T> Read<T>(string filePath)
        {
            
            List<T> items;
            
            if (File.Exists(filePath) == false)
            {
                File.Create(filePath);
            }
            StreamReader readStream = new(filePath);
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

            await using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, items);
        }
    }
}
