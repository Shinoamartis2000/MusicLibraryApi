using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicLibraryApi.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ArtistId { get; set; }

        [Range(1900, 2023, ErrorMessage = "ReleaseYear must be between 1900 and 2023.")]
        public int ReleaseYear { get; set; }

        // Navigation property for Artist
        public Artist? Artist { get; set; }

        // Navigation property for Tracks
        public ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}