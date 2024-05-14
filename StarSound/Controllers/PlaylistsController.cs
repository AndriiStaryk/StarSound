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
public class PlaylistsController : ControllerBase
{
    private readonly StarSoundContext _context;

    public PlaylistsController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/Playlists
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
    {
        return await _context.Playlists.ToListAsync();
    }

    // GET: api/Playlists/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Playlist>> GetPlaylist(int id)
    {
        var playlist = await _context.Playlists.FindAsync(id);

        if (playlist == null)
        {
            return NotFound();
        }

        return playlist;
    }

    // PUT: api/Playlists/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlaylist(int id, Playlist playlist)
    {
        //if (!PlaylistExistsById(id))
        //{
        //    return NotFound();
        //}

        if (PlaylistExists(playlist))
        {
            return Conflict("Playlist already exists.");
        }


        var existingPlaylist = await _context.Playlists.FindAsync(id);

        if (existingPlaylist == null)
        {
            return NotFound();
        }

        existingPlaylist.Name = playlist.Name;
        existingPlaylist.Image = playlist.Image;
        existingPlaylist.CreationYear = playlist.CreationYear;
        existingPlaylist.Description = playlist.Description;
        existingPlaylist.Duration = playlist.Duration;
        //existingPlaylist.Songs = playlist.Songs;

        //_context.Entry(playlist).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlaylistExistsById(id))
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

    // POST: api/Playlists
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
    {
        if (PlaylistExists(playlist))
        {
            return Conflict("Playlist already exists.");
        }

        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPlaylist", new { id = playlist.Id }, playlist);
    }

    // DELETE: api/Playlists/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(int id)
    {
        var playlist = await _context.Playlists.FindAsync(id);
        if (playlist == null)
        {
            return NotFound();
        }

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PlaylistExistsById(int id)
    {
        return _context.Playlists.Any(e => e.Id == id);
    }

    public bool PlaylistExists(Playlist playlist)
    {
        var wantedPlaylist = _context.Playlists
            .FirstOrDefault(
            p => p.Name == playlist.Name &&
            p.CreationYear == playlist.CreationYear &&
            p.Description == playlist.Description &&
            p.Duration == playlist.Duration);

        if (wantedPlaylist != null)
        {
            return true;
        }

        return false;
    }

}