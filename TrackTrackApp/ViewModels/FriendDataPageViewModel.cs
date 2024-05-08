using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Services;
using TrackTrackApp.Models;

namespace TrackTrackApp.ViewModels
{

    [QueryProperty(nameof(friendID), "friendID")]
    public class FriendDataPageViewModel : ViewModel
    {
        public ChartEntry[] entries { get; set; }

        public BarChart artistChart { get; set; }
        public BarChart genreChart { get; set; }
        public BarChart styleChart { get; set; }
        public BarChart yearChart { get; set; }

        public EventHandler loadCharts { get; set; }
        public ICommand BackButton { get; set; }
        public long friendID { get; set; }

        private User frienduser;
        public User friendUser { get { return frienduser; } set { frienduser = value; OnPropertyChanged(); } }

        

        public FriendDataPageViewModel(TrackTrackServices service)
        {

            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//UserMainPage"); });

            loadCharts = new EventHandler((s, e) => internalLoad(service));
        }

        private bool keepfirst = true;
        private async void internalLoad(TrackTrackServices service)
        {
            if (keepfirst) { keepfirst = false; }
            else
            {
                friendUser = await service.GetUserByID(friendID);

                //artist chart
                var artists = await service.GetArtistChartValues(friendID);
                var artistEntries = new ChartEntry[artists.Count];
                for (int i = 0; i < artists.Count; i++)
                {
                    artistEntries[i] = new ChartEntry(artists[i].Value) { Label = artists[i].String, ValueLabel = artists[i].Value.ToString(), Color = new SkiaSharp.SKColor(203, 151, 231) };
                }
                artistChart = new BarChart() { Entries = artistEntries, BackgroundColor = new SkiaSharp.SKColor(223, 216, 247), CornerRadius = 10, LabelTextSize = 20 };
                OnPropertyChanged(nameof(artistChart));

                //genre chart
                var genres = await service.GetGenreChartValues(friendID);
                var genreEntries = new ChartEntry[genres.Count];
                for (int i = 0; i < genres.Count; i++)
                {
                    genreEntries[i] = new ChartEntry(genres[i].Value) { Label = genres[i].String, ValueLabel = genres[i].Value.ToString(), Color = new SkiaSharp.SKColor(203, 151, 231) };
                }
                genreChart = new BarChart() { Entries = genreEntries, BackgroundColor = new SkiaSharp.SKColor(223, 216, 247), CornerRadius = 10, LabelTextSize = 20 };
                OnPropertyChanged(nameof(genreChart));

                //style chart
                var styles = await service.GetStyleChartValues(friendID);
                var styleEntries = new ChartEntry[styles.Count];
                for (int i = 0; i < styles.Count; i++)
                {
                    styleEntries[i] = new ChartEntry(styles[i].Value) { Label = styles[i].String, ValueLabel = styles[i].Value.ToString(), Color = new SkiaSharp.SKColor(203, 151, 231) };
                }
                styleChart = new BarChart() { Entries = styleEntries, BackgroundColor = new SkiaSharp.SKColor(223, 216, 247), CornerRadius = 10, LabelTextSize = 20 };
                OnPropertyChanged(nameof(styleChart));

                //year chart

                var years = await service.GetYearChartValues(friendID);
                var yearEntries = new ChartEntry[years.Count];
                for (int i = 0; i < years.Count; i++)
                {
                    yearEntries[i] = new ChartEntry(years[i].Value) { Label = years[i].String, ValueLabel = years[i].Value.ToString(), Color = new SkiaSharp.SKColor(203, 151, 231) };
                }
                yearChart = new BarChart() { Entries = yearEntries, BackgroundColor = new SkiaSharp.SKColor(223, 216, 247), CornerRadius = 10, LabelTextSize = 20 };
                OnPropertyChanged(nameof(yearChart));
            }
        }
    }
}
