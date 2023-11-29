using TrackTrackApp.ViewModels;
namespace TrackTrackApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();

        }
    }
}