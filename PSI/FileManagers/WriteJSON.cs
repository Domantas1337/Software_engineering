using System.Text.Json;
using PSI.Models;

namespace PSI.FileManagers
{
    static class WriteJSON
    {
        public static async void write(this LocationItem locationItem)
        {
            List<LocationItem> locationItems = ReadJSON.readAllLocations();
            locationItems.Add(locationItem);

            await using FileStream createStream = File.Create(Constants.locationsFilePath);
            await JsonSerializer.SerializeAsync(createStream, locationItems);
        }
    }
}
