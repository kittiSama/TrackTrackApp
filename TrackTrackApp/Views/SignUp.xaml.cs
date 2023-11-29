using TrackTrackApp.ViewModels;
namespace TrackTrackApp.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
		this.BindingContext = new SignUpViewModel();
	}
}