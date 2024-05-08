using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class FriendDataPage : ContentPage
{
	public FriendDataPage(FriendDataPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
		Loaded += vm.loadCharts;
		Appearing += vm.loadCharts;
	}
}