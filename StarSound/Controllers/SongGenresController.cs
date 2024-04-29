using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSound.Models;

namespace StarSound.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongGenresController : ControllerBase
{
    private readonly StarSoundContext _context;

    public SongGenresController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/SongGenres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongGenre>>> GetSongGenres()
    {
        return await _context.SongGenres.ToListAsync();
    }

    // GET: api/SongGenres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SongGenre>> GetSongGenre(int id)
    {
        var songGenre = await _context.SongGenres.FindAsync(id);

        if (songGenre == null)
        {
            return NotFound();
        }

        return songGenre;
    }

    // PUT: api/SongGenres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSongGenre(int id, SongGenre songGenre)
    {
        if (id != songGenre.Id)
        {
            return BadRequest();
        }

        _context.Entry(songGenre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongGenreExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/SongGenres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SongGenre>> PostSongGenre(SongGenre songGenre)
    {
        _context.SongGenres.Add(songGenre);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSongGenre", new { id = songGenre.Id }, songGenre);
    }

    // DELETE: api/SongGenres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSongGenre(int id)
    {
        var songGenre = await _context.SongGenres.FindAsync(id);
        if (songGenre == null)
        {
            return NotFound();
        }

        _context.SongGenres.Remove(songGenre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongGenreExists(int id)
    {
        return _context.SongGenres.Any(e => e.Id == id);
    }
}
