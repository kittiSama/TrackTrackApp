using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackTrackApp.Models;

namespace TrackTrackApp.Services
{
    public class TrackTrackServices
    {
        readonly HttpClient _httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"https://fbjg3s2d-7247.euw.devtunnels.ms/TrackTrack/";

        public TrackTrackServices()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
        }
        
        public async Task<HttpStatusCode> SignUp(string name, string password, string email)
        {
            User user = new User() { Name = name, Password = password, Email = email };
            var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(URL + "AddUser", stringContent);
                return(response.StatusCode);
            }
            catch { return HttpStatusCode.BadRequest; }

        }

        public async Task<HttpStatusCode> Login(string name, string password)
        {
            User user = new User() { Name = name, Password = password};
            var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(URL + "Login", stringContent);

                await SecureStorage.Default.SetAsync("CurrentUser", await response.Content.ReadAsStringAsync());
                return (response.StatusCode);
            }
            catch { return HttpStatusCode.BadRequest; }

        }

        public async Task<Album[]> QueryTop5(string q)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetClosestAlbumsForApp" + "?q=" + q);
                Album[] content = (Album[])(JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(), typeof(Album[]), _serializerOptions));
                return (content);
            }
            catch
            {
                return (null);
            }
        }

        public User GetSessionUser()
        {
            var u = SecureStorage.Default.GetAsync("CurrentUser");
            return JsonSerializer.Deserialize<User>(u.Result, _serializerOptions);
        }

        public async Task<HttpStatusCode> Hello()
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "Hello");
                return(response.StatusCode);
            }
            catch { return HttpStatusCode.BadRequest; }
        }
    }
}
