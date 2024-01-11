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

        public string Name { get; set; }

        public virtual User Owner { get; set; }

        public virtual ICollection<SavedAlbum> SavedAlbums { get; set; } = new List<SavedAlbum>();

        public Collection()
        {
            SavedAlbums = new List<SavedAlbum>();
            Id = 0;
            OwnerId = 0;
            Name = "";
            Owner = new User();
        }
    }
}
