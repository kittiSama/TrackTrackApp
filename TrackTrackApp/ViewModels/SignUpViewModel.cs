using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrackTrackApp.ViewModels
{
    internal class SignUpViewModel:ViewModel
    {
        private string username;
        public string Username { get {  return username; } set { username = value; OnPropertyChanged(); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); } }

        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }
        public ICommand BackButton { get; protected set; }
        public ICommand SignUpButton { get; protected set; }


        public SignUpViewModel()
        {
            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//MainPage"); });
            //SignUpButton = new Command(async () => )

        }

    }
}
