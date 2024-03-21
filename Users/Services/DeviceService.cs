using System;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Users.Models;

namespace Users.Services{
	public class DeviceService{
        static readonly string Url = "https://api.restful-api.dev/objects";
        //static readonly string Url = $"{BaseAddress}/api/";

        HttpClient _httpClient;
        public DeviceService() {
            _httpClient = new HttpClient();
        }




        public async Task<IEnumerable<Models.Device>> GetDevices() {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, Url);

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);


                var devices = JsonSerializer.Deserialize<IEnumerable<Models.Device>>(response.Content.ReadAsStringAsync().Result);

                return devices;
            }

            return new List<Models.Device>();

        }

        public async Task<Models.Device> AddDevice(Models.Device device) {

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Url);

            message.Content = JsonContent.Create<Models.Device>(device);

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);


                var devicer = JsonSerializer.Deserialize<Models.Device>(response.Content.ReadAsStringAsync().Result);

                return devicer;
            }

            return null;
        }

    }
}

