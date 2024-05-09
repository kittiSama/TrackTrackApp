using TrackTrackApp.ViewModels;
namespace TrackTrackApp.Views;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        Appearing += vm.Reset;

    }
}