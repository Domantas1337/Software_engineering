using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PSI.FileManagers;
using PSI.Logging;
using PSI.Models;

namespace PSI.Services
{
    public class LogService : ILogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private const string _mediaType = "application/json";
        private const string _endpoint = "log";

        public LogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5153" : "https://localhost:7120";
            _url = $"{_baseAddress}/api";
        }

        private void InnerOnResponseOutcome(HttpResponseMessage response, string methodMsg)
        {
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Successfully {methodMsg} logItem");
            }
            else
            {
                Debug.WriteLine("---> Non Http 2xx2x2xxx response");
                Debug.WriteLine("---> Non Http 2xx response");
            }
        }

        public async Task AddLogItemAsync(LogItem item)
        {
            try
            {
                string jsonString = JSONManager.SerializeToJSONString<LogItem>(item);
                StringContent content = new(jsonString, Encoding.UTF8, _mediaType);

                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/{_endpoint}", content).ConfigureAwait(false);
                InnerOnResponseOutcome(response, "added");
            }
            catch (Exception ex)
            {
                await Logger.LogAsync(ex);
            }
            return;
        }

        public async Task DeleteLogItemAsync(string id)
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

        public async Task<List<LogItem>> GetAllLogItemsAsync()
        {
            List<LogItem> items = new();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{_endpoint}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    items = JSONManager.DeserializeFromJSONString<LogItem>(content);
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
            return items;
        }
    }
}