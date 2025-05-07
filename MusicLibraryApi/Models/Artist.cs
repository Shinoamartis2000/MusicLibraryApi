using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MusicLibraryApi.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property for Albums
        [JsonIgnore]
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}