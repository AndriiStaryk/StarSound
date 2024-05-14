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
public class PerformersController : ControllerBase
{
    private readonly StarSoundContext _context;

    public PerformersController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/Performers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Performer>>> GetPerformers()
    {
        return await _context.Performers.ToListAsync();
    }

    // GET: api/Performers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Performer>> GetPerformer(int id)
    {
        var performer = await _context.Performers.FindAsync(id);

        if (performer == null)
        {
            return NotFound();
        }

        return performer;
    }

    // PUT: api/Performers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerformer(int id, Performer performer)
    {
        if (!PerformerExistsById(id))
        {
            return NotFound();
        }

        if (PerformerExists(performer))
        {
            return Conflict("Performer already exists.");
        }

        var existingPerformer = await _context.Performers.FindAsync(id);

        if (existingPerformer == null)
        {
            return NotFound();
        }

        existingPerformer.Name = performer.Name;
        existingPerformer.Image = performer.Image;
        existingPerformer.Year = performer.Year;
        //existingPerformer.Albums = performer.Albums;
        //existingPerformer.Songs = performer.Songs;


        //_context.Entry(performer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PerformerExistsById(id))
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

    // POST: api/Performers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Performer>> PostPerformer(Performer performer)
    {

        if (PerformerExists(performer))
        {
            return Conflict("Performer already exists.");
        }

        _context.Performers.Add(performer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPerformer", new { id = performer.Id }, performer);
    }

    // DELETE: api/Performers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerformer(int id)
    {
        var performer = await _context.Performers.FindAsync(id);
        if (performer == null)
        {
            return NotFound();
        }

        _context.Performers.Remove(performer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PerformerExistsById(int id)
    {
        return _context.Performers.Any(e => e.Id == id);
    }

    public bool PerformerExists(Performer performer)
    {
        var wantedPerformer = _context.Performers
            .FirstOrDefault(
            p => p.Name == performer.Name &&
            p.Year == performer.Year);

        if (wantedPerformer != null)
        {
            return true;
        }

        return false;
    }

}
