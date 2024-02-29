using Microcharts;
using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class DataPage : ContentPage
{
	public DataPage(DataPageViewModel vm)
	{
		InitializeComponent();
		chartView.Chart = new BarChart
		{
			Entries = vm.entries
		};
	}
}