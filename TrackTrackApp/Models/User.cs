using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackTrackApp.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio {  get; set; }
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();

        public virtual ICollection<SavedAlbum> SavedAlbums { get; set; } = new List<SavedAlbum>();

        public User()
        {
            Id = 0;
            Name = "";
            Email = "";
            Password = "";
            Bio = "";
            Collections = new List<Collection>();
            SavedAlbums = new List<SavedAlbum>();
        }
    }
}
