using PSI.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using PSI.Services;

namespace PSI.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Car> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<List<Car>> RefreshDataAsync()
        {

            Debug.WriteLine("WHATT");

            Items = new List<Car>();

            Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
            Debug.WriteLine("WHATT");

            try
            {
                HttpResponseMessage response = null;

                    response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Car>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }



    }
}
