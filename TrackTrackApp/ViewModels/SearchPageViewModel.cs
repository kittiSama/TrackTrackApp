using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackTrackApp.Models;
using TrackTrackApp.Services;

namespace TrackTrackApp.ViewModels
{


    [QueryProperty("Query", "q")]
    public class SearchPageViewModel:ViewModel
    {
        private string query;
        public string Query {  get { return query; } set {  query = value; OnPropertyChanged(); } }

        public Album[] albums { get { return albums; } set {  albums = value; OnPropertyChanged(); } }

        public SearchPageViewModel(TrackTrackServices service)
        {
            var z = (async () => albums = await service.QueryTop5(query));
            z.Invoke();

            //need to actually build the place where the albums will display. do that.
            // also im not yet sure anything here actually works properly
        }

    }
}
