using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using MusicLibraryApi.Controllers;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MusicLibraryApi.Tests
{
    public class AuthControllerTests
    {
        private readonly DbContextOptions<MusicLibraryContext> _options;
        private readonly IConfiguration _configuration;

        public AuthControllerTests()
        {
            // Create a new DbContextOptions for each test
            _options = new DbContextOptionsBuilder<MusicLibraryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Mock configuration for JWT settings
            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(c => c["JwtSettings:SecretKey"]).Returns("SuperSecretKeyForTesting");
            mockConfig.Setup(c => c["JwtSettings:Issuer"]).Returns("TestIssuer");
            mockConfig.Setup(c => c["JwtSettings:Audience"]).Returns("TestAudience");

            _configuration = mockConfig.Object;
        }

        [Fact]
        public async Task Register_CreatesNewUser()
        {
            // Arrange
            using (var context = new MusicLibraryContext(_options))
            {
                var controller = new AuthController(context, _configuration);
                var newUser = new User { Username = "testuser", Password = "testpass" };

                // Act
                var result = await controller.Register(newUser);

                // Assert
                var actionResult = Assert.IsType<OkObjectResult>(result.Result);
                var user = Assert.IsType<User>(actionResult.Value);
                Assert.Equal("testuser", user.Username);
            }
        }
    }
}