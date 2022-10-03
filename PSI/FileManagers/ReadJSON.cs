using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PSI.Models;

namespace PSI.FileManagers
{
    internal class ReadJSON
    {

        public static List<LocationItem> readAllLocations()
        {
            List<LocationItem> items;

            using (StreamReader r = new StreamReader(Constants.jsonFilePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<LocationItem>>(json);
            }
            return items;
        }
    }

}
