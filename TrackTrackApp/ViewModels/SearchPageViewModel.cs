using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<albumandheart> albums;
        public ObservableCollection<albumandheart> Albums { get { return albums; } set { albums = value; OnPropertyChanged("Albums"); } }

        public EventHandler PopulateAlbums { get; set; }
        public EventHandler ResetQuery { get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand LikeAlbumCommand {  get; set; }

        public SearchPageViewModel(TrackTrackServices service)
        {
            PopulateAlbums = new EventHandler(async (s, e) =>
                {
                    //death
                    Albums = new ObservableCollection<albumandheart>();
                    var arr = await service.QueryTop5(query);
                    for (int i = 0; i < arr.Length; i++) 
                    {
                        Albums.Add(new albumandheart { album = arr[i], image = "heart_icon.png" }); 
                    }
                });

            ResetQuery = new EventHandler((s,e) => NewQuery = Query);
            SearchCommand = new Command(async () =>
            {
                Albums = new ObservableCollection<albumandheart>();
                var arr = await service.QueryTop5(query);
                for (int i = 0; i < arr.Length; i++)
                {
                    Albums.Add(new albumandheart { album = arr[i], image = "heart_icon.png" });
                }
            });
            LikeAlbumCommand = new Command(async b => await LikeAlbumInternal((albumandheart)b, service));
            
        }



        private async Task LikeAlbumInternal(albumandheart al, TrackTrackServices service)
        {
            var resp = await service.FavoriteAlbum(al.album.AlbumID);


            if(resp == HttpStatusCode.OK)
            {
                await Shell.Current.DisplayAlert("success", "success", "yes");
                for (int i = 0; i < Albums.Count; i++)
                {
                    if (Albums[i].Equals(al))
                    {
                        Albums[i] = new albumandheart() { album = Albums[i].album, image = "heart_icon_happy.png" };
                    }
                }
            }

            //make the server save that album in the user's favorites
            //make the heart turn red or something, after receiving a 200OK from the server (it successfully saved the album)
        }


    }
}
