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

        private Album[] albums;
        public Album[] Albums { get { return albums; } set { albums = value; OnPropertyChanged(); } }

        public EventHandler PopulateAlbums { get; set; }
        public EventHandler ResetQuery { get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand LikeAlbumCommand {  get; set; }

        public SearchPageViewModel(TrackTrackServices service)
        {
            PopulateAlbums = new EventHandler(async (s,e) => Albums = await service.QueryTop5(query));
            ResetQuery = new EventHandler((s,e) => NewQuery = Query);
            SearchCommand = new Command(async () => Albums = await service.QueryTop5(newQuery));
            LikeAlbumCommand = new Command(async b => await LikeAlbumInternal((long)b, service));
            
        }

        private async Task LikeAlbumInternal(long albumid, TrackTrackServices service)
        {
            var resp = await service.FavoriteAlbum(albumid);


            if(resp == HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("success", "success", "yes");
            }
            //make the server save that album in the user's favorites
            //make the heart turn red or something, after receiving a 200OK from the server (it successfully saved the album)
        }


    }
}
