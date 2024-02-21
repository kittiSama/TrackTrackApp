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
        public ICommand OnCounterClicked { get; set; }
        public ICommand SignUpButton { get; protected set; }
        public ICommand LoginButton { get; protected set; }
        public MainPageViewModel(TrackTrackServices service)
        {
            SignUpButton = new Command(async () => { await Shell.Current.GoToAsync("//SignUp"); });
            LoginButton = new Command(async () => { await Shell.Current.GoToAsync("//Login"); });
        }
        
    }
}
