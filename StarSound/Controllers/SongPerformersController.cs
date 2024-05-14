﻿using System;
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
public class SongPerformersController : ControllerBase
{
    private readonly StarSoundContext _context;

    public SongPerformersController(StarSoundContext context)
    {
        _context = context;
    }

    // GET: api/SongPerformers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SongPerformer>>> GetSongPerformers()
    {
        return await _context.SongPerformers.ToListAsync();
    }

    // GET: api/SongPerformers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SongPerformer>> GetSongPerformer(int id)
    {
        var songPerformer = await _context.SongPerformers.FindAsync(id);

        if (songPerformer == null)
        {
            return NotFound();
        }

        return songPerformer;
    }

    // PUT: api/SongPerformers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSongPerformer(int id, SongPerformer songPerformer)
    {
        if (id != songPerformer.Id)
        {
            return BadRequest();
        }

        _context.Entry(songPerformer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SongPerformerExists(id))
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

    // POST: api/SongPerformers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SongPerformer>> PostSongPerformer(SongPerformer songPerformer)
    {
        _context.SongPerformers.Add(songPerformer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSongPerformer", new { id = songPerformer.Id }, songPerformer);
    }

    // DELETE: api/SongPerformers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSongPerformer(int id)
    {
        var songPerformer = await _context.SongPerformers.FindAsync(id);
        if (songPerformer == null)
        {
            return NotFound();
        }

        _context.SongPerformers.Remove(songPerformer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SongPerformerExists(int id)
    {
        return _context.SongPerformers.Any(e => e.Id == id);
    }
}