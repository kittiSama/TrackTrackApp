using TrackTrackApp.ViewModels;
namespace TrackTrackApp.Views;

public partial class UserMainPage : ContentPage
{
	public UserMainPage(UserMainPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
		Loaded += vm.GetUser;
	}
}