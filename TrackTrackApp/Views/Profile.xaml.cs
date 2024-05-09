using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class Profile : ContentPage
{
	public Profile(ProfileViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        Loaded += vm.GetUser;
		Appearing += vm.GetUser;
    }
}