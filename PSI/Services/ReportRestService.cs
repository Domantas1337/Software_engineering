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
    public class ReportRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly Location currentLocation = new(54.72908271722996, 25.264220631657665);


        private Lazy<LocationItem> nearestLocation;

        public ReportRestService(HttpClient httpClient)
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

        public async Task AddLocationItemAsync(ReportItem reportItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<ReportItem>(reportItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/report", content).ConfigureAwait(false); ;



                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Successfully created locationItem");
                }
                else
                {
                    Debug.WriteLine("---> Non Http xxxxxxx2 response");
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Whoops exception: {ex.Message}");
            }

            return;

        }

        public async Task PureAddLocationItemAsync(ReportItem reportItem)
        {
            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<ReportItem>(reportItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/report", content).ConfigureAwait(false); ;



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
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/report/{id}");
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
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/report/{id}");

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

        public async Task<List<ReportItem>> PureGetAllLocationItemsAsync()
        {
            List<ReportItem> reportItems = new();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/report");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var something = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportItem>>(content)
                        ?? new();

                    foreach (ReportItem i in something)
                    {
                        reportItems.Add(i);
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

            return reportItems;
        }

        public async Task<List<ReportItem>> GetAllLocationItemsAsync()
        {
            List<ReportItem> reportItems = new();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return reportItems;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/location");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    double distance = 1e9;

                    var tempLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportItem>>(content)
                        ?? new();

                    reportItems = tempLocations;


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

            return reportItems;
        }

        public async Task UpdateLocationItemAsync(ReportItem reportItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<ReportItem>(reportItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/report/{reportItem.ID}", content);

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