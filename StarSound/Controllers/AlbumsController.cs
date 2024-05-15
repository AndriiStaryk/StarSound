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
public class AlbumsController : ControllerBase
{
    private readonly StarSoundContext _context;

    public AlbumsController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/Albums
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Album>>> GetAlbums()
    {
        return await _context.Albums.ToListAsync();
    }

    // GET: api/Albums/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(int id)
    {
        var album = await _context.Albums.FindAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        return album;
    }

    // PUT: api/Albums/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlbum(int id, Album album)
    {
        //if (!AlbumExistsById(id))
        //{
        //    return NotFound();
        //}

        if (await AlbumExists(album))
        {
            return Conflict("Album already exists.");
        }


        var existingAlbum = await _context.Albums.FindAsync(id);

        if (existingAlbum == null)
        {
            return NotFound();
        }

        existingAlbum.Name = album.Name;
        existingAlbum.Image = album.Image;
        existingAlbum.ReleaseYear = album.ReleaseYear;
        existingAlbum.Description = album.Description;
        existingAlbum.Duration = album.Duration;
        //existingAlbum.Songs = album.Songs;
        //existingAlbum.Performers = album.Performers;


        // _context.Entry(album).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await AlbumExistsById(id))
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

    // POST: api/Albums
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Album>> PostAlbum(Album album)
    {
        if (await AlbumExists(album))
        {
            return Conflict("Album already exists.");
        }

        _context.Albums.Add(album);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
    }

    // DELETE: api/Albums/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var album = await _context.Albums.FindAsync(id);
        if (album == null)
        {
            return NotFound();
        }

        _context.Albums.Remove(album);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> AlbumExistsById(int id)
    {
        return await _context.Albums.AnyAsync(e => e.Id == id);
    }

    private async Task<bool> AlbumExists(Album album)
    {
        var wantedAlbum = await _context.Albums
            .FirstOrDefaultAsync(
            a => a.Name == album.Name &&
            a.ReleaseYear == album.ReleaseYear &&
            a.Description == album.Description &&
            a.Duration == album.Duration);

        if (wantedAlbum != null)
        {
            return true;
        }

        return false;
    }



}