using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class AlbumDatum
    {
        public long Id { get; set; }

        public long Year { get; set; }

        public long ArtistId { get; set; }

        public string ArtistName { get; set; } = null!;

        public string Country { get; set; } = null!;

        public virtual ICollection<AlbumGenre> AlbumGenres { get; set; } = new List<AlbumGenre>();

        public virtual ICollection<AlbumStyle> AlbumStyles { get; set; } = new List<AlbumStyle>();

        public virtual ICollection<SavedAlbum> SavedAlbums { get; set; } = new List<SavedAlbum>();

    }
}
