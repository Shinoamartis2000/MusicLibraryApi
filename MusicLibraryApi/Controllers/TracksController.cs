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
    public class TracksController : ControllerBase
    {
        private readonly MusicLibraryContext _context;

        public TracksController(MusicLibraryContext context)
        {
            _context = context;
        }

        // GET: api/Tracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetTracks(
            string search = "",
            string sortBy = "title",
            int page = 1,
            int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Page and pageSize must be greater than 0.");
            }

            var query = _context.Tracks.Include(t => t.Album).Include(t => t.Genre).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.Title.Contains(search));
            }

            // Sort using a traditional switch statement
            switch (sortBy.ToLower())
            {
                case "title":
                    query = query.OrderBy(t => t.Title);
                    break;
                case "title_desc":
                    query = query.OrderByDescending(t => t.Title);
                    break;
                case "duration":
                    query = query.OrderBy(t => t.Duration);
                    break;
                case "duration_desc":
                    query = query.OrderByDescending(t => t.Duration);
                    break;
                default:
                    query = query.OrderBy(t => t.TrackId);
                    break;
            }

            var result = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/Tracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(int id)
        {
            var track = await _context.Tracks.Include(t => t.Album).Include(t => t.Genre).FirstOrDefaultAsync(t => t.TrackId == id);
            if (track == null) return NotFound();
            return track;
        }

        // POST: api/Tracks
        [HttpPost]
        public async Task<ActionResult<Track>> PostTrack(TrackRequest request)
        {
            var track = new Track
            {
                Title = request.Title,
                AlbumId = request.AlbumId,
                Duration = request.Duration,
                GenreId = request.GenreId
            };
            _context.Tracks.Add(track);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrack), new { id = track.TrackId }, track);
        }

        // PUT: api/Tracks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(int id, Track track)
        {
            if (id != track.TrackId) return BadRequest();
            _context.Entry(track).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Tracks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var track = await _context.Tracks.FindAsync(id);
            if (track == null) return NotFound();
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}