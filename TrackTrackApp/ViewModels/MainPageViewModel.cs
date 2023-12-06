using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{
    public class MainPageViewModel:ViewModel
    {
        private int counter = 0;
        public int Counter { get { return counter; } set { counter = value; OnPropertyChanged(); } }
        public ICommand OnCounterClicked { get; set; }
        public ICommand SignUpButton { get; protected set; }
        public ICommand LoginButton { get; protected set; }
        public ICommand HelloButton { get; protected set; }
        public MainPageViewModel(TrackTrackServices service)
        {
            Counter = 0;
            OnCounterClicked = new Command(x => { Counter = Counter + 1;}) ;
            SignUpButton = new Command(async () => { await Shell.Current.GoToAsync("//SignUp"); });
            LoginButton = new Command(async () => { await Shell.Current.GoToAsync("//Login"); });
            HelloButton = new Command(async () =>
            {
                var resp = await service.Hello();

                if (resp == System.Net.HttpStatusCode.OK) { await Shell.Current.DisplayAlert("success", "success", "yes"); }
                else { await Shell.Current.DisplayAlert("bad", "no", "sad"); }
            });
        }
        
    }
}
