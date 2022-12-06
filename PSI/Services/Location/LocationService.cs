using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using PSI.FileManagers;
using PSI.Models;

namespace PSI.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private const string _mediaType = "application/json";
        private const string _endpoint = "location";


        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5153" : "https://localhost:7120";
            _url = $"{_baseAddress}/api";
        }

        private void InnerOnResponseOutcome(HttpResponseMessage response, string methodMsg)
        {
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Successfully {methodMsg} locationItem");
            }
            else
            {
                Debug.WriteLine("---> Non Http 2xx2x2xxx response");
                Debug.WriteLine("---> Non Http 2xx response");
            }
        }

        public async Task AddLocationItemAsync(LocationItem locationItem)
        {
            try
            {
                string jsonLocationItem = JSONManager.SerializeToJSONString<LocationItem>(locationItem);
                StringContent content = new (jsonLocationItem, Encoding.UTF8, _mediaType);

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/{_endpoint}", content).ConfigureAwait(false);
                InnerOnResponseOutcome(response, "added");
            }
            catch (Exception ex)
            {
                await Logger.LogAsync(ex);
            }
            return;
        }

        public async Task DeleteLocationItemAsync(string id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/{_endpoint}/{id}");
                InnerOnResponseOutcome(response, "deleted");
            }
            catch (Exception ex)
            {
                await Logger.LogAsync(ex);
            }
            return;
        }

        public async Task<List<LocationItem>> GetAllLocationItemsAsync()
        {

            List<LocationItem> locationItems = new();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{_endpoint}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    locationItems = JSONManager.DeserializeFromJSONString<LocationItem>(content);
                    foreach(LocationItem item in locationItems)
                    {
                        if (CurrentLocation.GetCurrentLocation.CalculateDistance(currentLocation, DistanceUnits.Kilometers) < distance)
                        {
                            distance = location.CalculateDistance(currentLocation, DistanceUnits.Kilometers);
                            Debug.WriteLine(distance);
                            nearestLocation = item;
                        }
                        item.Position = new Location(item.Latitude, item.Longitude);
                    }
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }
            }
            catch (Exception ex)
            {
                await Logger.LogAsync(ex);
            }
            return locationItems;
        }

        public async Task UpdateLocationItemAsync(LocationItem locationItem)
        {
            try
            {
                string jsonLocationItem = JSONManager.SerializeToJSONString<LocationItem>(locationItem);
                StringContent content = new (jsonLocationItem, Encoding.UTF8, _mediaType);
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{_endpoint}/{locationItem.ID}", content);
                InnerOnResponseOutcome(response, "updated");
            }
            catch (Exception ex)
            {
                await Logger.LogAsync(ex);
            }
            return;
        }
    }
}