using TrackTrackApp.ViewModels;
namespace TrackTrackApp.Views;

public partial class SignUp : ContentPage
{
	public SignUp(SignUpViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}