using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private string username;
        public string Username { get { return username; } set { username = value; OnPropertyChanged(); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(); } }
        public ICommand BackButton { get; protected set; }
        public ICommand SubmitButton { get; protected set; }


        public LoginViewModel(TrackTrackServices service)
        {

            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//MainPage"); });
            SubmitButton = new Command(async () =>
            {
                var resp = await service.Login(username, password);

                if (resp == System.Net.HttpStatusCode.OK) {
                    {
                        await Shell.Current.GoToAsync("//UserMainPage", true);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Incorrect username or password", "", "sad");
                }
            });
        }
    }
}
