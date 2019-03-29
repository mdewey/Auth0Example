using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Auth0Example.Models;
using content;
using Microsoft.AspNetCore.Authorization;

namespace content.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class MonstersController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public MonstersController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Monsters
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Monsters>>> GetMonsters()
    {
      return await _context.Monsters.ToListAsync();
    }

    // GET: api/Monsters/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Monsters>> GetMonsters(int id)
    {
      var monsters = await _context.Monsters.FindAsync(id);

      if (monsters == null)
      {
        return NotFound();
      }

      return monsters;
    }

    // PUT: api/Monsters/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMonsters(int id, Monsters monsters)
    {
      if (id != monsters.Id)
      {
        return BadRequest();
      }

      _context.Entry(monsters).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MonstersExists(id))
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

    // POST: api/Monsters
    [HttpPost]
    public async Task<ActionResult<Monsters>> PostMonsters(Monsters monsters)
    {
      _context.Monsters.Add(monsters);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetMonsters", new { id = monsters.Id }, monsters);
    }

    // DELETE: api/Monsters/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Monsters>> DeleteMonsters(int id)
    {
      var monsters = await _context.Monsters.FindAsync(id);
      if (monsters == null)
      {
        return NotFound();
      }

      _context.Monsters.Remove(monsters);
      await _context.SaveChangesAsync();

      return monsters;
    }

    private bool MonstersExists(int id)
    {
      return _context.Monsters.Any(e => e.Id == id);
    }
  }
}
