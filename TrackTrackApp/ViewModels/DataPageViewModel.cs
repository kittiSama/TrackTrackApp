using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.ViewModels
{
    public class DataPageViewModel
    {
        public ChartEntry[] entries { get; set; }


        public DataPageViewModel()
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
        }
    }
}
