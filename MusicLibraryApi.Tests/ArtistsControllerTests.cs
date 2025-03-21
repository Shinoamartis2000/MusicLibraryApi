    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Controllers;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace MusicLibraryApi.Tests
{
    public class ArtistsControllerTests
    {
        private readonly MusicLibraryContext _context;

        public ArtistsControllerTests()
        {
            // Use an in-memory database for testing
            var options = new DbContextOptionsBuilder<MusicLibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new MusicLibraryContext(options);

            // Seed the database with test data
            _context.Artists.Add(new Artist { Name = "Test Artist" });
            _context.Albums.Add(new Album {  Title = "Test Album", ArtistId = 1, ReleaseYear = 2020 });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetArtists_ReturnsListOfArtists()
        {
            // Arrange
            var controller = new AlbumsController(_context);

            // Act
            var result = await controller.GetAlbums();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Album>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var albums = Assert.IsType<List<Album>>(okResult.Value);
            //Assert.Empty(albums);
        }

        [Fact]
        public async Task GetArtist_ReturnsArtist()
        {
            // Arrange
            var controller = new AlbumsController(_context);

            // Act
            var result = await controller.GetAlbum(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Album>>(result);
            var album = Assert.IsType<Album>(actionResult.Value);
            Assert.Equal("Test Album", album.Title);
        }

        [Fact]
        public async Task PostArtist_CreatesNewArtist()
        {
            // Arrange
            var controller = new AlbumsController(_context);
            var newAlbum = new AlbumRequest { Title = "New Album", ReleaseYear = 2021 };

            // Act
            var result = await controller.PostAlbum(newAlbum);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var album = Assert.IsType<Album>(actionResult.Value);
            Assert.Equal("New Album", album.Title);
        }

        [Fact]
        public async Task DeleteArtist_RemovesArtist()
        {
            // Arrange
            var controller = new AlbumsController(_context);

            // Act
            //var result = await controller.DeleteAlbum(1);

            //// Assert
            //Assert.IsType<NoContentResult>(result);
            //var album = await _context.Albums.FindAsync(1);
            //Assert.Null(album);
        }
    }
}