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
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly Location currentLocation = new(54.72908271722996, 25.264220631657665);


        public event EventHandler<LocationEventArgs> LocationsExist;


        public RestService(HttpClient httpClient)
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

        public async Task AddLocationItemAsync(LocationItem locationItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LocationItem>(locationItem, _jsonSerializerOptions);
                StringContent content = new (jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/psi", content).ConfigureAwait(false); ;



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

        public async Task PureAddLocationItemAsync(LocationItem locationItem)
        {
            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LocationItem>(locationItem, _jsonSerializerOptions);
                StringContent content = new(jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/psi", content).ConfigureAwait(false); ;

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


        public async Task PureDeleteLocationItemAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/psi/{id}");
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

        public async Task DeleteLocationItemAsync(int id)
        {

            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/psi/{id}");

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

        public async Task<List<LocationItem>> PureGetAllLocationItemsAsync()
        {
            List<LocationItem> locationItems = new ();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/psi");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var something = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationItem>>(content)
                        ?? new();

                    foreach (LocationItem i in something)
                    {
                        locationItems.Add(i);
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

            return locationItems;
        }

        public async Task<List<LocationItem>> GetAllLocationItemsAsync()
        {
            List<LocationItem> locationItems = new ();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return locationItems;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/psi");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    
                    double distance = 1e9;
                    LocationItem nearestLocation = default;

                    var tempLocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationItem>>(content)
                        ?? new (); 

                    foreach(LocationItem item in tempLocations){
                        Location location = new((double)item.Latitude, (double)item.Longitude);

                        if (location.CalculateDistance(currentLocation, DistanceUnits.Kilometers) < distance) {
                            distance = location.CalculateDistance(currentLocation, DistanceUnits.Kilometers);
                            Debug.WriteLine(distance);
                            nearestLocation = item;
                        }

                        locationItems.Add(
                                            new LocationItem()
                                            {
                                                Street = item.Street,
                                                City = item.City,
                                                Id = item.Id,
                                                Longitude = item.Longitude,
                                                Latitude = item.Latitude,
                                                State = item.State,
                                                Position = location
                                            }
                        );
                    }

                    LocationsExist(this, new LocationEventArgs(nearestLocation, distance));

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

            return locationItems;
        }

        public async Task UpdateLocationItemAsync(LocationItem locationItem)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonLocationItem = JsonSerializer.Serialize<LocationItem>(locationItem, _jsonSerializerOptions);
                StringContent content = new (jsonLocationItem, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/psi/{locationItem.Id}", content);

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