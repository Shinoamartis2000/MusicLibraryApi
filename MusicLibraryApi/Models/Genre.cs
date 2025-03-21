using System.Collections.Generic;

namespace MusicLibraryApi.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property for Tracks
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}