using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Controllers;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MusicLibraryApi.Tests
{
    public class GenresControllerTests
    {
        private readonly DbContextOptions<MusicLibraryContext> _options;

        public GenresControllerTests()
        {
            // Create a new DbContextOptions for each test
            _options = new DbContextOptionsBuilder<MusicLibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the database with test data
            using (var context = new MusicLibraryContext(_options))
            {
                context.Genres.Add(new Genre { Name = "Test Genre" });
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetGenres_ReturnsListOfGenres()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new GenresController(context);

                // Act
                var result = await controller.GetGenres();
                var genreList = await context.Genres.ToListAsync();

                // Assert
                Assert.NotEmpty(genreList);
            }
        }

        [Fact]
        public async Task GetGenre_ReturnsGenre()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new GenresController(context);

                // Act
                var result = await controller.GetGenre(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Genre>>(result);
                var genre = Assert.IsType<Genre>(actionResult.Value);
                Assert.Equal("Test Genre", genre.Name);
            }
        }

        [Fact]
        public async Task PostGenre_CreatesNewGenre()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new GenresController(context);
                var newGenre = new GenreRequest { Name = "New Genre" };

                // Act
                var result = await controller.PostGenre(newGenre);

                // Assert
                var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
                var genre = Assert.IsType<Genre>(actionResult.Value);
                Assert.Equal("New Genre", genre.Name);
            }
        }

        [Fact]
        public async Task DeleteGenre_RemovesGenre()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new GenresController(context);

                // Act
                var result = await controller.DeleteGenre(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
                var genre = await context.Genres.FindAsync(1);
                Assert.Null(genre);
            }
        }
    }
}