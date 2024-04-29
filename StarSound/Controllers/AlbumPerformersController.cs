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
public class AlbumPerformersController : ControllerBase
{
    private readonly StarSoundContext _context;

    public AlbumPerformersController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/AlbumPerformers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlbumPerformer>>> GetAlbumPerformers()
    {
        return await _context.AlbumPerformers.ToListAsync();
    }

    // GET: api/AlbumPerformers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AlbumPerformer>> GetAlbumPerformer(int id)
    {
        var albumPerformer = await _context.AlbumPerformers.FindAsync(id);

        if (albumPerformer == null)
        {
            return NotFound();
        }

        return albumPerformer;
    }

    // PUT: api/AlbumPerformers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlbumPerformer(int id, AlbumPerformer albumPerformer)
    {
        if (id != albumPerformer.Id)
        {
            return BadRequest();
        }

        _context.Entry(albumPerformer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AlbumPerformerExists(id))
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

    // POST: api/AlbumPerformers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<AlbumPerformer>> PostAlbumPerformer(AlbumPerformer albumPerformer)
    {
        _context.AlbumPerformers.Add(albumPerformer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAlbumPerformer", new { id = albumPerformer.Id }, albumPerformer);
    }

    // DELETE: api/AlbumPerformers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbumPerformer(int id)
    {
        var albumPerformer = await _context.AlbumPerformers.FindAsync(id);
        if (albumPerformer == null)
        {
            return NotFound();
        }

        _context.AlbumPerformers.Remove(albumPerformer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AlbumPerformerExists(int id)
    {
        return _context.AlbumPerformers.Any(e => e.Id == id);
    }
}
