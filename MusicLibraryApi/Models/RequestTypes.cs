using System.ComponentModel.DataAnnotations;

namespace MusicLibraryApi.Models;

public class GenreRequest
{
    public string Name { get; set; } = string.Empty;
}

public class ArtistRequest
{
    public string Name { get; set; } = string.Empty;
}


public class AlbumRequest
{

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int ArtistId { get; set; }

    [Range(1900, 2023, ErrorMessage = "ReleaseYear must be between 1900 and 2023.")]
    public int ReleaseYear { get; set; }
}

public class TrackRequest
{

    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int AlbumId { get; set; }

    [Required]
    public int GenreId { get; set; }

    [Range(1, 3600, ErrorMessage = "Duration must be between 1 and 3600 seconds.")]
    public int Duration { get; set; }
}