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

        public EventHandler loadCharts { get; set; }
        public ICommand BackButton { get; set; }


        public DataPageViewModel(TrackTrackServices service)
        {
            BackButton = new Command(async () => { await Shell.Current.GoToAsync("//UserMainPage"); });

            loadCharts = new EventHandler((s, e) => internalLoad(service));
            
        }

        private async void internalLoad(TrackTrackServices service)
        {
            var artists = await service.GetArtistChartValues();
            var artistEntries = new ChartEntry[artists.Length];
            for (int i = 0; i < artists.Length; i++)
            {
                artistEntries[i] = new ChartEntry(artists[i].Value) { Label = artists[i].String, ValueLabel = artists[i].Value.ToString() };
            }
            artistChart=new BarChart() { Entries = artistEntries, };
            OnPropertyChanged(nameof(artistChart));
            //APP DOESNT DO THE PROCESSING, ALL PROCESSING GOES TO THE SERVER IM GENIUS, SERVER RETURNS VALUES AND RESULTS

        }
    }
}
