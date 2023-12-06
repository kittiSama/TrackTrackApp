using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class Collection
    {
        public long Id { get; set; }

        public long OwnerId { get; set; }

        public string Name { get; set; } = null!;

        public virtual User Owner { get; set; } = null!;

        public virtual ICollection<SavedAlbum> SavedAlbums { get; set; } = new List<SavedAlbum>();
    }
}
