using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTrackApp.Services;
using TrackTrackApp.Models;
using System.Windows.Input;

namespace TrackTrackApp.ViewModels
{
    public class ProfileViewModel:ViewModel
    {
        public EventHandler GetUser { get; protected set; }
        public ICommand BackButton {  get; protected set; }

        private User sessionuser;
        public User SessionUser { get { return sessionuser; } set { sessionuser = value; OnPropertyChanged(); } }

        public ICommand SubmitChangesCommand { get; protected set; }

        public ProfileViewModel(TrackTrackServices service)
        {

            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//UserMainPage"); });
            GetUser = new EventHandler(async (s, e) => { SessionUser = await service.GetSessionUser(); });

            SubmitChangesCommand = new Command(async () => await service.UpdateUser(SessionUser));
        }
    }
}
