using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        const string URL = @"https://hln4n3n5-7247.uks1.devtunnels.ms/TrackTrack/";

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

        public async Task<AlbumAndHeart[]> QueryTop5(string q)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetClosestAlbumsForApp" + "?q=" + q);
                AlbumAndHeart[] content = (AlbumAndHeart[])(JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(), typeof(AlbumAndHeart[]), _serializerOptions));
                //AlbumAndHeart[] toReturn = new AlbumAndHeart[content.Length];  // i changed everything makje it work :))))
                //string user = ((await GetSessionUser()).Id).ToString(); //something here doesnt work, could also be in the server
                //List<SavedAlbum> allSaved = JsonSerializer.Deserialize<List<SavedAlbum>>(await (await _httpClient.GetAsync(URL + "GetAlbumsInCollectionByName" + "?userId=" + user + "&collectionName=" + "favorites" )).Content.ReadAsStringAsync(),_serializerOptions);
                //for (int i = 0; i < content.Length; i++)
                //{
                //    toReturn[i] = new albumandheart();
                //    toReturn[i].album = content[i];
                //    if ((allSaved.Where(x=> x.Id == content[i].AlbumID)).Any())
                //    {
                //        toReturn[i].image = "heart_icon_happy.png";
                //    }
                //    else
                //    {
                //        toReturn[i].image = "heart_icon.png";
                //    }
                //}
                return (content);
            }
            catch
            {
                return (null);
            }
        }

        public async Task<User> GetSessionUser()
        {
            var u = await SecureStorage.Default.GetAsync("CurrentUser");
            User user = JsonSerializer.Deserialize<User>(u, _serializerOptions);
            return user;
        }

        public async Task<HttpResponseMessage> FavoriteAlbum(long albumID)
        {
            try
            {
                SavedAlbum z = new SavedAlbum();
                z.AlbumId = albumID;
                SaveAlbumByNameDTO dto = new SaveAlbumByNameDTO() { collectionName = "favorites", savedAlbum = z };
                string json = JsonSerializer.Serialize(dto, _serializerOptions);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(URL + "SaveAlbumByName", stringContent);
                return (response);
            }
            catch { return null; }
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
