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
public class GenresController : ControllerBase
{
    private readonly StarSoundContext _context;

    public GenresController(StarSoundContext context)
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

        if (genre == null)
        {
            return NotFound();
        }

        return genre;
    }

    // PUT: api/Genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, Genre genre)
    {
        if (!GenreExistsById(id))
        {
            return NotFound();
        }

        if (GenreExists(genre))
        {
            return Conflict("Genre already exists.");
        }

        var existingGenre = await _context.Genres.FindAsync(id);

        if (existingGenre == null)
        {
            return NotFound();
        }

        existingGenre.Name = genre.Name;
        existingGenre.Image = genre.Image;
        existingGenre.Songs = genre.Songs;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GenreExistsById(id))
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


    // POST: api/Genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(Genre genre)
    {
        if(GenreExists(genre))
        {
            return Conflict("Genre already exists.");
        }

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetGenre", new { id = genre.Id }, genre);
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GenreExistsById(int id)
    {
        return _context.Genres.Any(e => e.Id == id);
    }

    private bool GenreExists(Genre genre)
    {
        var wantedGenre = _context.Genres
            .FirstOrDefault(g => g.Name.ToLower() == genre.Name.ToLower());

        return wantedGenre != null;
    } 

    //maybe add field by filed valiidation
}
