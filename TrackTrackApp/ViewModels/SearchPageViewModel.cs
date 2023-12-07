using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.ViewModels
{


    [QueryProperty("Query", "q")]
    public class SearchPageViewModel:ViewModel
    {
        private string query;
        public string Query {  get { return query; } set {  query = value; OnPropertyChanged(); } }

        public SearchPageViewModel()
        {


        }

    }
}
