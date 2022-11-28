using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


using PSI.Models;
using PSI.Services;

namespace PSI.Services
{
    public class LogRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly Location currentLocation = new(54.72908271722996, 25.264220631657665);


        private Lazy<LocationItem> nearestLocation;

        public LogRestService(HttpClient httpClient)
        {
            //_httpClient = new HttpClient();
            _httpClient = httpClient;

            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5153" : "https://localhost:7120";
            _url = $"{_baseAddress}/api";

            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddLocationItemAsync(LogItem logItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LogItem>(logItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/log", content).ConfigureAwait(false); ;



                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx2x2xxx response");
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;

        }

        public async Task PureAddLocationItemAsync(LogItem logItem)
        {
            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LogItem>(logItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/log", content).ConfigureAwait(false); ;



                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx2x2xxx response");
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;

        }


        public async Task PureDeleteLocationItemAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/log/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }

        public async Task DeleteLocationItemAsync(string id)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/log/{id}");

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }

        public async Task<List<LogItem>> PureGetAllLocationItemsAsync()
        {
            List<LogItem> logItems = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/log");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var something = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LogItem>>(content)
                        ?? new();

                    foreach (LogItem i in something)
                    {
                        logItems.Add(i);
                    }
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return logItems;
        }

        public async Task<List<LogItem>> GetAllLocationItemsAsync()
        {
            List<LogItem> logItems = new();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return logItems;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/location");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    double distance = 1e9;

                    var tempLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LogItem>>(content)
                        ?? new();

                    logItems = tempLocations;                    


                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return logItems;
        }

        public async Task UpdateLocationItemAsync(LogItem logItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LogItem>(logItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/log/{logItem.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;
        }
    }
}