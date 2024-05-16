using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{
    public class SignUpViewModel:ViewModel
    {
        private string username;
        public string Username { get {  return username; } set { username = value; OnPropertyChanged(); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); } }

        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }
        public ICommand BackButton { get; protected set; }
        public ICommand SubmitButton { get; protected set; }
        public EventHandler Reset {  get; protected set; }


        public SignUpViewModel(TrackTrackServices service)
        {
            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//MainPage"); });
            SubmitButton = new Command(async () => 
            { 
                var resp = await service.SignUp(username, password, email);
                
                if(resp=="good") {
                    await Shell.Current.GoToAsync("//UserMainPage", true);
                }
                else { await Shell.Current.DisplayAlert(resp, "no", "sad"); }
            });

            Reset = new EventHandler(async (s, e) => { Password = ""; Username = ""; Email = ""; });

        }

    }
}
