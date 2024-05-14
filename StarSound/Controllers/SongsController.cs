using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarSound.Models;

namespace StarSound.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongsController : ControllerBase
{
    private readonly StarSoundContext _context;

    public SongsController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/Songs
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
    {
        return await _context.Songs.ToListAsync();
    }

    // GET: api/Songs/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> GetSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);

        if (song == null)
        {
            return NotFound();
        }

        return song;
    }

    // PUT: api/Songs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSong(int id, Song song)
    {
        //if (id != song.Id)
        //{
        //    return BadRequest();
        //}

        if (SongExists(song))
        {
            return Conflict("Song already exists.");
        }

        var existingSong = await _context.Songs.FindAsync(id);

        if (existingSong == null)
        {
            return NotFound();
        }

        existingSong.Name = song.Name;
        existingSong.Image = song.Image;
        existingSong.ReleaseYear = song.ReleaseYear;
        existingSong.Duration = song.Duration;
        existingSong.AlbumId = song.AlbumId;
        existingSong.IsExplicit = song.IsExplicit;
        existingSong.IsFavorite = song.IsFavorite;
        existingSong.Lyrics = song.Lyrics;   
        existingSong.Performers = song.Performers;
        existingSong.Genres = song.Genres;
        existingSong.Playlists = song.Playlists;



        //_context.Entry(song).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongExistsById(id))
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

    // POST: api/Songs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Song>> PostSong(Song song)
    {

        if (SongExists(song))
        {
            return Conflict("Song already exists.");
        }

        _context.Songs.Add(song);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSong", new { id = song.Id }, song);
    }

    // DELETE: api/Songs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSong(int id)
    {
        var song = await _context.Songs.FindAsync(id);
        if (song == null)
        {
            return NotFound();
        }

        _context.Songs.Remove(song);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongExistsById(int id)
    {
        return _context.Songs.Any(e => e.Id == id);
    }

    public bool SongExists(Song song)
    {
        var wantedSong = _context.Songs
            .FirstOrDefault(
            s => s.Name == song.Name &&
            s.ReleaseYear == song.ReleaseYear &&
            s.IsFavorite == song.IsFavorite &&
            s.IsExplicit == song.IsExplicit &&
            s.Duration == song.Duration);

        if (wantedSong != null)
        {
            return true;
        }

        return false;
    }
}
