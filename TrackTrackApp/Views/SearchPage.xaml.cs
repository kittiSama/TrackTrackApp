using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;

		Appearing += vm.PopulateAlbums;
		Loaded += vm.PopulateAlbums;
		Appearing += vm.ResetQuery;
		Loaded += vm.ResetQuery;
	}
	
}