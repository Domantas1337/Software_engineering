using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PSI.Models;
using PSI.FileManagers;

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

            await using FileStream createStream = File.Create(Constants.jsonFilePath);
            await JsonSerializer.SerializeAsync(createStream, locationItems);
        }
    }
}
