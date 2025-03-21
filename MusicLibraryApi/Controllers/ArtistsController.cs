using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly MusicLibraryContext _context;
        private readonly IMemoryCache _cache;

        public ArtistsController(MusicLibraryContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists(
            string search = "",
            string sortBy = "name",
            int page = 1,
            int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var cacheKey = $"Artists_{search}_{sortBy}_{page}_{pageSize}";

            if (!_cache.TryGetValue(cacheKey, out List<Artist> artists))
            {
                var query = _context.Artists.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(a => a.Name.Contains(search));
                }

                // Sort using a traditional switch statement
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(a => a.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(a => a.Name);
                        break;
                    default:
                        query = query.OrderBy(a => a.ArtistId);
                        break;
                }

                artists = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache for 5 minutes

                _cache.Set(cacheKey, artists, cacheOptions);
            }

            return Ok(artists);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return NotFound();
            return artist;
        }

        // GET: api/Artists/1/Albums
        [HttpGet("{id}/albums")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbumsByArtist(int id)
        {
            var artist = await _context.Artists
                .Include(a => a.Albums)
                .FirstOrDefaultAsync(a => a.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist.Albums);
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(ArtistRequest request)
        {
            var artist = new Artist
            {
                Name = request.Name
            };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistId }, artist);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.ArtistId) return BadRequest();
            _context.Entry(artist).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null) return NotFound();
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}