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
        const string ONHEART = "heart_icon_happy.png";
        const string OFFHEART = "heart_icon.png";

        private string query;
        public string Query {  get { return query; } set {  query = value; OnPropertyChanged(); } }

        private string newQuery;
        public string NewQuery { get { return newQuery; } set { newQuery = value; OnPropertyChanged(); } }

        private ObservableCollection<AlbumAndHeart> albums;
        public ObservableCollection<AlbumAndHeart> Albums { get { return albums; } set { albums = value; OnPropertyChanged("Albums"); } }

        public EventHandler PopulateAlbums { get; set; }
        public EventHandler ResetQuery { get; set; }
        public ICommand SearchCommand {  get; set; }
        public ICommand LikeAlbumCommand {  get; set; }
        public ICommand BackButton { get; set; }

        public SearchPageViewModel(TrackTrackServices service)
        {
            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//UserMainPage"); });

            PopulateAlbums = new EventHandler(async (s, e) =>
            {
                Albums = new ObservableCollection<AlbumAndHeart>(new AlbumAndHeart[5]);
                var arr = await service.QueryTop5(query);
                for (int i = 0; i < arr.Length; i++) 
                {
                    Albums[i] = arr[i]; 
                }
            });

            ResetQuery = new EventHandler((s,e) => NewQuery = Query);

            SearchCommand = new Command(async () =>
            {
                Albums = new ObservableCollection<AlbumAndHeart>(new AlbumAndHeart[5]);
                var arr = await service.QueryTop5(NewQuery);
                for (int i = 0; i < arr.Length; i++)
                {
                    Albums[i] = arr[i];
                }
            });

            LikeAlbumCommand = new Command(async b => await LikeAlbumInternal((AlbumAndHeart)b, service));
            
        }



        private async Task LikeAlbumInternal(AlbumAndHeart al, TrackTrackServices service)
        {
            if (al.image == ONHEART)
            {
                if(await service.UnfavoriteAlbum(al.album.AlbumID))
                {
                    for (int i = 0; i < Albums.Count; i++)
                    {
                        if (Albums[i].Equals(al))
                        {
                            Albums[i] = new AlbumAndHeart() { album = Albums[i].album, image = OFFHEART };
                        }
                    }
                }
            }
            if(al.image == OFFHEART)
            {
                if(await service.FavoriteAlbum(al.album.AlbumID))
                {
                    for (int i = 0; i < Albums.Count; i++)
                    {
                        if (Albums[i].Equals(al))
                        {
                            Albums[i] = new AlbumAndHeart() { album = Albums[i].album, image = ONHEART };
                        }
                    }
                }
            }


        }


    }
}
