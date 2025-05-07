using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicLibraryApi.Data;
using MusicLibraryApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly MusicLibraryContext _context;

        public GenresController(MusicLibraryContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            return genre;
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre([FromBody] GenreRequest request)
        {
            var genre = new Genre
            {
                Name = request.Name
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.GenreId) return BadRequest();
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        //// GET: api/Genres/1/Tracks
        //[HttpGet("{id}/tracks")]
        //public async Task<ActionResult<IEnumerable<Track>>> GetTracksByGenre(int id)
        //{
        //    var genre = await _context.Genres
        //        .Include(g => g.Tracks)
        //        .FirstOrDefaultAsync(g => g.GenreId == id);

        //    if (genre == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(genre.Tracks);
        //}
    }
}