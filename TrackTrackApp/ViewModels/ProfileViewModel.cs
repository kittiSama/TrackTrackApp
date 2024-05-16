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

        private bool supdated;
        public bool successfullyUpdated { get { return supdated; } set {  supdated = value; OnPropertyChanged(); } }

        public ProfileViewModel(TrackTrackServices service)
        {
            supdated = false;
            BackButton = new Command(async () => { successfullyUpdated = false; await Shell.Current.GoToAsync("//UserMainPage"); });
            GetUser = new EventHandler(async (s, e) => { SessionUser = await service.GetSessionUser(); OnPropertyChanged(); });

            SubmitChangesCommand = new Command(async () => { string z = (await service.UpdateUser(SessionUser));
                if (z == ("good")) {
                    successfullyUpdated = true;
                }
                else
                {
                    await Shell.Current.DisplayAlert(z, "no", "sad");

                }
            });
        }
    }
}
