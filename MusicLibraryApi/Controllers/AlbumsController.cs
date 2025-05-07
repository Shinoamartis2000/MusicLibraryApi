using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly MusicLibraryContext _context;

        public AlbumsController(MusicLibraryContext context)
        {
            _context = context;
        }

        // GET: api/Albums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAlbums(
            string search = "",
            string sortBy = "(releaseYear)",
            int page = 1,
            int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var query = _context.Albums.Include(a => a.Artist).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Title.Contains(search));
            }

            // Sort using a traditional switch statement
            switch (sortBy.ToLower())
            {
                case "releaseyear":
                    query = query.OrderBy(a => a.ReleaseYear);
                    break;
                case "releaseyear_desc":
                    query = query.OrderByDescending(a => a.ReleaseYear);
                    break;
                default:
                    query = query.OrderBy(a => a.AlbumId);
                    break;
            }

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Albums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _context.Albums.Include(a => a.Artist).FirstOrDefaultAsync(a => a.AlbumId == id);
            if (album == null) return NotFound();
            return album;
        }

        // GET: api/Albums/1/Tracks
        [HttpGet("{id}/tracks")]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracksByAlbum(int id)
        {
            var album = await _context.Albums
                .Include(a => a.Tracks)
                .FirstOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album.Tracks);
        }

        // POST: api/Albums
        [HttpPost]
        public async Task<ActionResult<Album>> PostAlbum(AlbumRequest request)
        {
            var album = new Album
            {
                ArtistId = request.ArtistId,
                Title = request.Title,
                ReleaseYear = request.ReleaseYear
            };

            _context.Albums.Add(album);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAlbum), new { id = album.AlbumId }, album);
        }

        // PUT: api/Albums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, Album album)
        {
            if (id != album.AlbumId) return BadRequest();
            _context.Entry(album).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Albums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album == null) return NotFound();
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}