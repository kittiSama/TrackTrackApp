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
        public EventHandler GetUser {  get; protected set; }

        private User sessionUser;
        public User SessionUser { get { return sessionUser; } set {  sessionUser = value; OnPropertyChanged(); } }

        public ObservableCollection<string> searchTypes { get; protected set; }

        private string chosentype;
        public string chosenType { get { return chosentype; }set { chosentype = value; OnPropertyChanged(); } }

        public string[] QueryAndSearchType { get { return (new string[] { query, chosenType }); } }



        public UserMainPageViewModel(TrackTrackServices service)
        {
            searchTypes = new ObservableCollection<string> { "Album Title", "Artist Name", "Label Name", "Genre", "Style", "Country", "Year" };
            chosenType = "Album Title";

            GetUser = new EventHandler(async (s, e) => { SessionUser = await service.GetSessionUser(); WelcomeMessage = "Welcome " + SessionUser.Name; });
            //+"&SType="+chosenType

            QuerySubmitButton = new Command(async () => {await Shell.Current.GoToAsync("//SearchPage" + "?QNSType=" + Query + "~" + chosenType); });//transfers to search page
            DatapageButton = new Command(async () => await Shell.Current.GoToAsync("//DataPage"));
        }

    }
}
