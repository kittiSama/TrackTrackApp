using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class AlbumGenre
    {
        public long Id { get; set; }

        public long AlbumId { get; set; }

        public string Genre { get; set; } = null!;

        public virtual AlbumDatum Album { get; set; }
    }
}
