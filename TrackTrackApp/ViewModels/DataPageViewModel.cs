using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrackTrackApp.Models;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{
    public class DataPageViewModel:ViewModel
    {
        public ChartEntry[] entries { get; set; }

        public BarChart artistChart { get; set; }
        public BarChart genreChart { get; set; }
        public BarChart styleChart { get; set; }

        public EventHandler loadCharts { get; set; }
        public ICommand BackButton { get; set; }


        public DataPageViewModel(TrackTrackServices service)
        {
            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//UserMainPage"); });

            loadCharts = new EventHandler((s, e) => internalLoad(service));
            
        }

        private async void internalLoad(TrackTrackServices service)
        {
            //artist chart
            var artists = await service.GetArtistChartValues();
            var artistEntries = new ChartEntry[artists.Count];
            for (int i = 0; i < artists.Count; i++)
            {
                artistEntries[i] = new ChartEntry(artists[i].Value) { Label = artists[i].String, ValueLabel = artists[i].Value.ToString() };
            }
            artistChart=new BarChart() { Entries = artistEntries, };
            OnPropertyChanged(nameof(artistChart));

            //genre chart
            var genres = await service.GetGenreChartValues();
            var genreEntries = new ChartEntry[genres.Count];
            for (int i = 0; i < genres.Count; i++)
            {
                genreEntries[i] = new ChartEntry(genres[i].Value) { Label = genres[i].String, ValueLabel = genres[i].Value.ToString() };
            }
            genreChart = new BarChart() { Entries = genreEntries};
            OnPropertyChanged(nameof(genreChart));

            //style chart
            var styles = await service.GetStyleChartValues();
            var styleEntries = new ChartEntry[styles.Count];
            for (int i = 0; i < styles.Count; i++)
            {
                styleEntries[i] = new ChartEntry(styles[i].Value) { Label = styles[i].String, ValueLabel = styles[i].Value.ToString() };
            }
            styleChart = new BarChart() { Entries = styleEntries };
            OnPropertyChanged(nameof(styleChart));
            //APP DOESNT DO THE PROCESSING, ALL PROCESSING GOES TO THE SERVER IM GENIUS, SERVER RETURNS VALUES AND RESULTS

        }
    }
}
