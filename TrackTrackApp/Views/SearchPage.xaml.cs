using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
		Loaded += vm.PopulateAlbums;
		Loaded += vm.ResetQuery;
	}
	
}