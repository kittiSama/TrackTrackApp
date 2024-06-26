﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class Album
    {
        public string ImageUrl { get; set; }

        public long AlbumID { get; set; }

        public string AlbumTitle { get; set; }

        public string ArtistName { get; set; }

        public string Year { get; set; }

        public string Genre { get; set; }

        public string Style { get; set; }


    }
}
