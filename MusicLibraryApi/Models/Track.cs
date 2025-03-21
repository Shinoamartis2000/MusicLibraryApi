using System.ComponentModel.DataAnnotations;

namespace MusicLibraryApi.Models
{
    public class Track
    {
        public int TrackId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int AlbumId { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Range(1, 3600, ErrorMessage = "Duration must be between 1 and 3600 seconds.")]
        public int Duration { get; set; }

        // Navigation property for Album
        public Album? Album { get; set; }

        // Navigation property for Genre
        public Genre? Genre { get; set; }
    }
}