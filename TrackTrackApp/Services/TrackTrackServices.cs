using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackTrackApp.Models;
using TrackTrackApp.Views;
//using static Java.Util.Jar.Attributes;

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
                await Login(name, password);
                return (response.StatusCode);
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

        public async Task<AlbumAndHeart[]> QueryTop5(string q, string SType, bool LocalSearch, IGeocoding geocoding)
        {
            try
            {
                HttpResponseMessage response;
                if (LocalSearch)
                {
                    response = await _httpClient.GetAsync(URL + "GetClosestAlbumsForApp" + "?q=" + q + "&SType=" + SType + "&country="+ await GetCurrentLocation(geocoding));

                }
                else
                {
                    response = await _httpClient.GetAsync(URL + "GetClosestAlbumsForApp" + "?q=" + q + "&SType=" + SType);
                }
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

        public async Task<bool> FavoriteAlbum(long albumID)
        {
            try
            {
                SavedAlbum z = new SavedAlbum();
                z.Album = new AlbumDatum();
                z.Album.ArtistName = "";
                z.Album.Country = "";
                z.AlbumId = albumID;
                SaveAlbumByNameDTO dto = new SaveAlbumByNameDTO() { collectionName = "favorites", savedAlbum = z };
                string json = JsonSerializer.Serialize(dto, _serializerOptions);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(URL + "SaveAlbumByName", stringContent); 
                if(response.IsSuccessStatusCode) { return true; }
                return false;
            }
            catch { return false; }
        }
        public async Task<bool> UnfavoriteAlbum(long albumID)
        {
            try
            {
                SavedAlbum z = new SavedAlbum();
                z.AlbumId = albumID;
                z.Album = new AlbumDatum();
                z.User = await GetSessionUser();
                z.Album.Id = albumID;
                z.Album.ArtistName = "";
                z.Album.Country = "";
                SaveAlbumByNameDTO dto = new SaveAlbumByNameDTO() { collectionName = "favorites", savedAlbum = z };
                string json = JsonSerializer.Serialize(dto, _serializerOptions);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(URL + "DeleteAlbumByName", stringContent);
                if (response.IsSuccessStatusCode) { return true; }
                return false;
            }
            catch { return false; }
        }

        public async Task<List<StringAndValue>> GetArtistChartValues(long id = -1)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetArtistChartValues?id=" + id);
                string st = await response.Content.ReadAsStringAsync();
                List<StringAndValue> toReturn = JsonSerializer.Deserialize<List<StringAndValue>>(st, _serializerOptions);
                return (toReturn);
            }
            catch { return null; }
        }
        public async Task<List<StringAndValue>> GetGenreChartValues(long id = -1)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetGenreChartValues?id=" + id);
                string st = await response.Content.ReadAsStringAsync();
                List<StringAndValue> toReturn = JsonSerializer.Deserialize<List<StringAndValue>>(st, _serializerOptions);
                return (toReturn);
            }
            catch { return null; }
        }
        public async Task<List<StringAndValue>> GetStyleChartValues(long id = -1)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetStyleChartValues?id="+id);
                string st = await response.Content.ReadAsStringAsync();
                List<StringAndValue> toReturn = JsonSerializer.Deserialize<List<StringAndValue>>(st, _serializerOptions);
                return (toReturn);
            }
            catch { return null; }
        }
        public async Task<List<StringAndValue>> GetYearChartValues(long id = -1)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetYearChartValues?id="+id);
                string st = await response.Content.ReadAsStringAsync();
                List<StringAndValue> toReturn = JsonSerializer.Deserialize<List<StringAndValue>>(st, _serializerOptions);
                return (toReturn);
            }
            catch { return null; }
        }

        public async Task<User> GetUserByID(long id)
        {
            try
            {
                var response = await _httpClient.GetAsync(URL + "GetUsers?param=id&value=" + id);
                string st = await response.Content.ReadAsStringAsync();
                return (JsonSerializer.Deserialize<User>(st, _serializerOptions));
            }
            catch { return null; }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {//fix this
                var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(URL + "UpdateUser", stringContent);
                string st = await response.Content.ReadAsStringAsync();

                if (JsonSerializer.Deserialize<bool>(st, _serializerOptions))
                {
                    await SecureStorage.Default.SetAsync("CurrentUser", JsonSerializer.Serialize(user, _serializerOptions));
                    return (true);
                }
                
                return false;
                
            }
            catch { return false; }
        }
        public async Task SignOut()
        {
            SecureStorage.Default.Remove("CurrentUser");
            await _httpClient.GetAsync(URL + "SignOut");
        }

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task<string> GetCurrentLocation(IGeocoding geocoding)
        {
            try
            {
                
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {

                    //geocoding.SetMapServiceToken("AtCnTPhBaznCCE3PH2Dnwh8u19Ab1DMO2ImALyflGcujD5EHf5AgRIw_GbvbbSaK");



                    IEnumerable<Placemark> placemarks = await geocoding.GetPlacemarksAsync(location);
                    Placemark placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        return placemark.CountryName.ToLower();
                    }
                }
             return string.Empty;
            }
            
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }
    }
}
