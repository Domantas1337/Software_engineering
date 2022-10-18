using Newtonsoft.Json;
using PSI.Models;

namespace PSI.FileManagers
{
    internal class ReadJSON<T>
    {

        public static List<T> readAllLocations()
        {
            List<T> items;
            if (File.Exists(Constants.locationsFilePath) == false)
            {
                File.Create(Constants.locationsFilePath);
            }
            StreamReader readStream = new(Constants.locationsFilePath);
            string json = readStream.ReadToEnd();
            Debug.WriteLine($"Read from {Constants.locationsFilePath}");
            readStream.Close();
            items = JsonConvert.DeserializeObject<List<T>>(json)
                        ?? new();

            return items;
        }
    }
}
