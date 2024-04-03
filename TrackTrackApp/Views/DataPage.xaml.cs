using Microcharts;
using TrackTrackApp.ViewModels;

namespace TrackTrackApp.Views;

public partial class DataPage : ContentPage
{
	public DataPage(DataPageViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;

        Appearing += vm.loadCharts;
    }
}