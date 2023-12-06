using TrackTrackApp.ViewModels;
namespace TrackTrackApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;

        }
    }
}