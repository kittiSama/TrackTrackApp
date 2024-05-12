//using Android.App.Admin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Models;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{


    public class UserMainPageViewModel : ViewModel
    {
        private string welcomeMessage;
        public string WelcomeMessage { get { return welcomeMessage; } set { welcomeMessage = value; OnPropertyChanged(); } }

        private string query;
        public string Query { get { return query; } set { query = value; OnPropertyChanged(); } }
        public ICommand QuerySubmitButton { get; protected set; }
        public ICommand DatapageButton { get; protected set; }
        public ICommand ProfileButton { get; protected set; }
        public ICommand SignOutButton {  get; protected set; }
        public EventHandler GetUser {  get; protected set; }

        private User sessionUser;
        public User SessionUser { get { return sessionUser; } set {  sessionUser = value; OnPropertyChanged(); } }

        public ObservableCollection<string> searchTypes { get; protected set; }

        private string chosentype;
        public string chosenType { get { return chosentype; }set { chosentype = value; OnPropertyChanged(); } }

        public string[] QueryAndSearchType { get { return (new string[] { query, chosenType }); } }


        private string friendidquery;
        public string friendIDQuery { get { return friendidquery; } set { friendidquery = value; OnPropertyChanged(); } }

        public ICommand FriendIDSubmit { get; protected set; }


        public UserMainPageViewModel(TrackTrackServices service, IGeocoding geocoding)
        {
            searchTypes = new ObservableCollection<string> { "Album Title", "Artist Name", "Label Name", "Genre", "Style", "Country", "Year" };
            chosenType = "Album Title";

            GetUser = new EventHandler(async (s, e) => { SessionUser = await service.GetSessionUser(); WelcomeMessage = "Welcome " + SessionUser.Name; Query = ""; friendIDQuery = ""; Query = await service.GetCurrentLocation(geocoding); });
            //+"&SType="+chosenType

            QuerySubmitButton = new Command(async () => {await Shell.Current.GoToAsync("//SearchPage" + "?QNSType=" + Query + "~" + chosenType); });//transfers to search page
            DatapageButton = new Command(async () => await Shell.Current.GoToAsync("//DataPage"));
            FriendIDSubmit = new Command(async () => await Shell.Current.GoToAsync("//FriendDataPage?friendID=" + friendIDQuery));
            ProfileButton = new Command(async () => await Shell.Current.GoToAsync("//Profile"));
            SignOutButton = new Command(async () => { await service.SignOut(); await Shell.Current.GoToAsync("//MainPage"); });
        }

    }
}
