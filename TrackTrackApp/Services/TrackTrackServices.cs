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
    internal class TrackTrackServices
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
        /*
        public async Task<HttpStatusCode> SignUp(User user)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");

        }*/
    }
}
