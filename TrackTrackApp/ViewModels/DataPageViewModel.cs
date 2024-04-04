using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTrackApp.Models;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{
    public class DataPageViewModel
    {
        public ChartEntry[] entries { get; set; }

        public BarChart artistChart { get; set; }

        public EventHandler loadCharts {  get; set; }

        public DataPageViewModel(TrackTrackServices service)
        {



            entries = new[]
            {
                new ChartEntry(212)
                {
                    Label = "a",
                    ValueLabel = "b",
                },
                new ChartEntry(111)
                {
                    Label = "a",
                    ValueLabel = "b",
                }
            };
            artistChart = new BarChart() { Entries=entries};

            loadCharts = new EventHandler((s, e) => internalLoad(service));
            
        }

        private async void internalLoad(TrackTrackServices service)
        {
            User currUser = await service.GetSessionUser();
            //APP DOESNT DO THE PROCESSING, ALL PROCESSING GOES TO THE SERVER IM GENIUS, SERVER RETURNS VALUES AND RESULTS

        }
    }
}
