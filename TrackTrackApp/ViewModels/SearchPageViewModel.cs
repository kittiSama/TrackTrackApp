using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Models;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{


    [QueryProperty("Query", "q")]
    public class SearchPageViewModel:ViewModel
    {
        private string query;
        public string Query {  get { return query; } set {  query = value; OnPropertyChanged(); } }

        private string newQuery;
        public string NewQuery { get { return newQuery; } set { newQuery = value; OnPropertyChanged(); } }

        private albumandheart[] albums;
        public albumandheart[] Albums { get { return albums; } set { albums = value; OnPropertyChanged(); } }

        public EventHandler PopulateAlbums { get; set; }
        public EventHandler ResetQuery { get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand LikeAlbumCommand {  get; set; }

        public SearchPageViewModel(TrackTrackServices service)
        {
            albums = new albumandheart[5];
            PopulateAlbums = new EventHandler(async (s, e) =>
                {
                    //death
                    var arr = await service.QueryTop5(query);
                    for (int i = 0; i < arr.Length; i++) { Albums[i] = new albumandheart { album = arr[i], image = "heart_icon.png" }; OnPropertyChanged(); }
                });
            ResetQuery = new EventHandler((s,e) => NewQuery = Query);
                SearchCommand = new Command(async () =>
                {
                    var arr = await service.QueryTop5(query);
                    for (int i = 0; i < arr.Length; i++) { Albums[i] = new albumandheart { album = arr[i], image = "heart_icon.png" }; OnPropertyChanged(); }
                });
            LikeAlbumCommand = new Command(async b => await LikeAlbumInternal((albumandheart)b, service));
            
        }



        private async Task LikeAlbumInternal(albumandheart al, TrackTrackServices service)
        {
            var resp = await service.FavoriteAlbum(al.album.AlbumID);


            if(resp == HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("success", "success", "yes");
                al.image = "heart_icon_happy.png";
            }

            //make the server save that album in the user's favorites
            //make the heart turn red or something, after receiving a 200OK from the server (it successfully saved the album)
        }


    }
}
