using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;
using PSI.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PSI.FileManagers
{
    internal static class WriteJSON<T> where T : struct
    {
        public static List<T> readFile(string filePath)
        {
            
            List<T> items;
            
            if (File.Exists(filePath) == false)
            {
                File.Create(filePath);
            }
            StreamReader readStream = new(filePath);
            string json = readStream.ReadToEnd();
            Debug.WriteLine($"Read from {filePath}");
            readStream.Close();

            items = JsonConvert.DeserializeObject<List<T>>(json)
                        ?? new();

            return items;
        }
        public static async void write(string filePath, T item = default, List<T> items = null)
        {
            if (items == null)
            {
                items = readFile(filePath);
                items.Add(item);
            }

            await using FileStream createStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(createStream, items);
        }
    }
}
