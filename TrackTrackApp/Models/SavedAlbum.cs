﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class SavedAlbum
    {
        public long Id { get; set; }

        public long AlbumId { get; set; }

        public long UserId { get; set; }

        public long CollectionId { get; set; }

        public DateTime Date { get; set; }

        public long? Rating { get; set; }

        public virtual Collection Collection { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
