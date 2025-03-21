using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Models;

public class MusicLibraryContext : DbContext
{
    public MusicLibraryContext(DbContextOptions<MusicLibraryContext> options) : base(options)
    {
    }

    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=musiclibrary.db");
        }
    }
}