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
public class SongPlaylistsController : ControllerBase
{
    private readonly StarSoundContext _context;

    public SongPlaylistsController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/SongPlaylists
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongPlaylist>>> GetSongPlaylists()
    {
        return await _context.SongPlaylists.ToListAsync();
    }

    // GET: api/SongPlaylists/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SongPlaylist>> GetSongPlaylist(int id)
    {
        var songPlaylist = await _context.SongPlaylists.FindAsync(id);

        if (songPlaylist == null)
        {
            return NotFound();
        }

        return songPlaylist;
    }

    // PUT: api/SongPlaylists/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSongPlaylist(int id, SongPlaylist songPlaylist)
    {
        if (id != songPlaylist.Id)
        {
            return BadRequest();
        }

        _context.Entry(songPlaylist).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongPlaylistExists(id))
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

    // POST: api/SongPlaylists
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SongPlaylist>> PostSongPlaylist(SongPlaylist songPlaylist)
    {
        _context.SongPlaylists.Add(songPlaylist);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSongPlaylist", new { id = songPlaylist.Id }, songPlaylist);
    }

    // DELETE: api/SongPlaylists/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSongPlaylist(int id)
    {
        var songPlaylist = await _context.SongPlaylists.FindAsync(id);
        if (songPlaylist == null)
        {
            return NotFound();
        }

        _context.SongPlaylists.Remove(songPlaylist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongPlaylistExists(int id)
    {
        return _context.SongPlaylists.Any(e => e.Id == id);
    }
}
