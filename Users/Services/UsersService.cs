using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Users.Models;

namespace Users.Services {
    internal class UsersService {

        static readonly string Url = "https://gorest.co.in/public/v2/users";
        //static readonly string Url = $"{BaseAddress}/api/";

        HttpClient _httpClient;
        public UsersService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 4fbc0562253041b8c905b720308b4727ce30a263e67040903e92fd2be9f120dd");
        }



        public async Task<IEnumerable<User>> GetUsers() {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, Url);

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);


                var users = JsonSerializer.Deserialize<IEnumerable<User>>(response.Content.ReadAsStringAsync().Result);

                return users;
            }

            return new List<User>();
           
        }

        public async Task<User> AddUser(User user) {

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, Url);

            message.Content = JsonContent.Create<User>(user);

            HttpResponseMessage response = await _httpClient.SendAsync(message);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);


                var userr = JsonSerializer.Deserialize<User>(response.Content.ReadAsStringAsync().Result);

                return user;
            }

            return null;
        }
    }
}
