using TrackTrackApp.ViewModels;
namespace TrackTrackApp.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel();
	}
}