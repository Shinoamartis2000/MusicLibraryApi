using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicLibraryApi.Data
{
    public static class DataSeeder
    {
        public static void Seed(MusicLibraryContext context)
        {
            if (!context.Artists.Any())
            {
                // Step 1: Add Artists
                var artists = new List<Artist>
                {
                    new Artist { Name = "Artist 1" },
                    new Artist { Name = "Artist 2" },
                    new Artist { Name = "Artist 3" },
                    new Artist { Name = "Artist 4" },
                    new Artist { Name = "Artist 5" },
                    new Artist { Name = "Artist 6" },
                    new Artist { Name = "Artist 7" },
                    new Artist { Name = "Artist 8" },
                    new Artist { Name = "Artist 9" },
                    new Artist { Name = "Artist 10" },
                    new Artist { Name = "Artist 11" },
                    new Artist { Name = "Artist 12" },
                    new Artist { Name = "Artist 13" },
                    new Artist { Name = "Artist 14" },
                    new Artist { Name = "Artist 15" }
                };
                context.Artists.AddRange(artists);
                context.SaveChanges(); // Save Artists first

                // Step 2: Add Genres
                var genres = new List<Genre>
                {
                    new Genre { Name = "Rock" },
                    new Genre { Name = "Pop" },
                    new Genre { Name = "Jazz" },
                    new Genre { Name = "Classical" },
                    new Genre { Name = "Hip-Hop" },
                    new Genre { Name = "Electronic" },
                    new Genre { Name = "Blues" },
                    new Genre { Name = "Country" },
                    new Genre { Name = "Reggae" },
                    new Genre { Name = "Metal" },
                    new Genre { Name = "Folk" },
                    new Genre { Name = "R&B" },
                    new Genre { Name = "Punk" },
                    new Genre { Name = "Soul" },
                    new Genre { Name = "Funk" }
                };
                context.Genres.AddRange(genres);
                context.SaveChanges(); // Save Genres next

                // Step 3: Add Albums (depends on Artists)
                var albums = new List<Album>
                {
                    new Album { Title = "Album 1", ArtistId = 1, ReleaseYear = 2020 },
                    new Album { Title = "Album 2", ArtistId = 2, ReleaseYear = 2019 },
                    new Album { Title = "Album 3", ArtistId = 3, ReleaseYear = 2021 },
                    new Album { Title = "Album 4", ArtistId = 4, ReleaseYear = 2018 },
                    new Album { Title = "Album 5", ArtistId = 5, ReleaseYear = 2022 },
                    new Album { Title = "Album 6", ArtistId = 6, ReleaseYear = 2017 },
                    new Album { Title = "Album 7", ArtistId = 7, ReleaseYear = 2020 },
                    new Album { Title = "Album 8", ArtistId = 8, ReleaseYear = 2019 },
                    new Album { Title = "Album 9", ArtistId = 9, ReleaseYear = 2021 },
                    new Album { Title = "Album 10", ArtistId = 10, ReleaseYear = 2018 },
                    new Album { Title = "Album 11", ArtistId = 11, ReleaseYear = 2022 },
                    new Album { Title = "Album 12", ArtistId = 12, ReleaseYear = 2017 },
                    new Album { Title = "Album 13", ArtistId = 13, ReleaseYear = 2020 },
                    new Album { Title = "Album 14", ArtistId = 14, ReleaseYear = 2019 },
                    new Album { Title = "Album 15", ArtistId = 15, ReleaseYear = 2021 }
                };
                context.Albums.AddRange(albums);
                context.SaveChanges(); // Save Albums next

                // Step 4: Add Tracks (depends on Albums and Genres)
                var tracks = new List<Track>
                {
                    new Track { Title = "Track 1", AlbumId = 1, GenreId = 1, Duration = 300 },
                    new Track { Title = "Track 2", AlbumId = 2, GenreId = 2, Duration = 240 },
                    new Track { Title = "Track 3", AlbumId = 3, GenreId = 3, Duration = 320 },
                    new Track { Title = "Track 4", AlbumId = 4, GenreId = 4, Duration = 280 },
                    new Track { Title = "Track 5", AlbumId = 5, GenreId = 5, Duration = 360 },
                    new Track { Title = "Track 6", AlbumId = 6, GenreId = 6, Duration = 200 },
                    new Track { Title = "Track 7", AlbumId = 7, GenreId = 7, Duration = 310 },
                    new Track { Title = "Track 8", AlbumId = 8, GenreId = 8, Duration = 270 },
                    new Track { Title = "Track 9", AlbumId = 9, GenreId = 9, Duration = 330 },
                    new Track { Title = "Track 10", AlbumId = 10, GenreId = 10, Duration = 290 },
                    new Track { Title = "Track 11", AlbumId = 11, GenreId = 11, Duration = 350 },
                    new Track { Title = "Track 12", AlbumId = 12, GenreId = 12, Duration = 250 },
                    new Track { Title = "Track 13", AlbumId = 13, GenreId = 13, Duration = 340 },
                    new Track { Title = "Track 14", AlbumId = 14, GenreId = 14, Duration = 260 },
                    new Track { Title = "Track 15", AlbumId = 15, GenreId = 15, Duration = 370 }
                };
                context.Tracks.AddRange(tracks);
                context.SaveChanges(); // Save Tracks last
            }
        }
    }
}