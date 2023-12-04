using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrackTrackApp.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private string username;
        public string Username { get { return username; } set { username = value; OnPropertyChanged(); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); } }
        public ICommand BackButton { get; protected set; }


        public LoginViewModel()
        {

            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//MainPage"); });
        }
    }
}
