using System.Text.Json;
using PSI.Models;

namespace PSI.FileManagers
{
    internal class WriteJSON
    {
        public WriteJSON(LocationItem locationItem)
        {
            write(locationItem);
        }

        async void write(LocationItem locationItem)
        {
            List<LocationItem> locationItems = ReadJSON.readAllLocations();
            locationItems.Add(locationItem);

            await using FileStream createStream = File.Create(Constants.locationsFilePath);
            await JsonSerializer.SerializeAsync(createStream, locationItems);
        }
    }
}
