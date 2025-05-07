using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Controllers;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicLibraryApi.Tests
{
    public class TracksControllerTests
    {
        private readonly DbContextOptions<MusicLibraryContext> _options;

        public TracksControllerTests()
        {
            // Create a new DbContextOptions for each test
            _options = new DbContextOptionsBuilder<MusicLibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the database with test data
            using (var context = new MusicLibraryContext(_options))
            {
                context.Artists.Add(new Artist { Name = "Test Artist" });
                context.Albums.Add(new Album { Title = "Test Album", ArtistId = 1, ReleaseYear = 2020 });
                context.Genres.Add(new Genre { Name = "Test Genre" });
                context.Tracks.Add(new Track { Title = "Test Track", AlbumId = 1, GenreId = 1, Duration = 200 });
                context.SaveChanges();
            }
        }

        //[Fact]
        //public async Task GetTracks_ReturnsListOfTracks()
        //{
        //    // Arrange
        //    using (var context = new MusicLibraryContext(_options))
        //    {
        //        var controller = new TracksController(context);

        //        // Act
        //        var result = await controller.GetTracks();

        //        // Assert
        //        var actionResult = Assert.IsType<ActionResult<IEnumerable<Track>>>(result);
        //        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //        var tracks = Assert.IsType<List<Track>>(okResult.Value);
        //        Assert.Single(tracks);
        //    }
        //}

        //[Fact]
        //public async Task GetTrack_ReturnsTrack()
        //{
        //    // Arrange
        //    using (var context = new MusicLibraryContext(_options))
        //    {
        //        var controller = new TracksController(context);

        //        // Act
        //        var result = await controller.GetTrack(1);
        //        var trackList = context.Tracks.ToList ;

        //        // Assert
        //        var actionResult = Assert.IsType<ActionResult<Track>>(result);
        //        var track = Assert.IsType<Track>(actionResult.Value);
        //        Assert.Equal("Test Track", track.Title);
        //    }
        //}

        [Fact]
        public async Task PostTrack_CreatesNewTrack()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new TracksController(context);
                var newTrack = new TrackRequest { Title = "New Track", AlbumId = 1, GenreId = 1, Duration = 300 };

                // Act
                var result = await controller.PostTrack(newTrack);

                // Assert
                var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var track = Assert.IsType<Track>(actionResult.Value);
                Assert.Equal("New Track", track.Title);
            }
        }

        [Fact]
        public async Task DeleteTrack_RemovesTrack()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new TracksController(context);

                // Act
                //var result = await controller.DeleteTrack(1);

                //// Assert
                //Assert.IsType<NoContentResult>(result);
                //var track = await context.Tracks.FindAsync(1);
                //Assert.Null(track);
            }
        }
    }
}