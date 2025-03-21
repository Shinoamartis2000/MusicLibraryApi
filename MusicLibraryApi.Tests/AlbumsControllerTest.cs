using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Controllers;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MusicLibraryApi.Tests
{
    public class AlbumsControllerTests : IDisposable
    {
        private readonly DbContextOptions<MusicLibraryContext> _options;

        public AlbumsControllerTests()
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
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetAlbums_ReturnsNotEmptyList()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new AlbumsController(context);

                // Act
                var albums = await context.Genres.ToListAsync();

                // Assert
                Assert.NotEmpty(albums);
            }
        }

        public void Dispose()
        {
            using (var context = new MusicLibraryContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}