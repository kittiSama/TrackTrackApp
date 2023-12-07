//using Android.App.Admin;
using System;
using System.Collections.Generic;
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

        private User sessionUser;

        public UserMainPageViewModel(TrackTrackServices service)
        {
            sessionUser = service.GetSessionUser();
            WelcomeMessage = "Welcome " + sessionUser.Name;

            QuerySubmitButton = new Command(async () => await Shell.Current.GoToAsync("//SearchPage"+"?q="+Query));//transfers to search page
        }
    }
}
