using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapelController : ControllerBase
    {
        private readonly TodoContext2 _context;

        public MapelController(TodoContext2 context)
        {
            _context = context;
        }

        // GET: api/Mapels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mapel>>> GetMapels()
    {
        return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Mapel>> GetMapel(long id)
    {
        var Mapel = await _context.TodoItems.FindAsync(id);

        if (Mapel == null)
        {
            return NotFound();
        }

        return ItemToDTO(Mapel);
    }

    [HttpPost]
    public async Task<ActionResult<Mapel>> CreateMapel(Mapel Mapel)
    {
        var Mapels = new Mapel
        {
            IsComplete = Mapel.IsComplete,
            NamaMapel = Mapel.NamaMapel
        };

        _context.TodoItems.Add(Mapels);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetMapel),
            new { id = Mapels.id },
            ItemToDTO(Mapels));
    }

    private bool MapelExists(string id) =>
        _context.TodoItems.Any(e => e.id == id);

    private static Mapel ItemToDTO(Mapel Mapel) =>
        new Mapel
        {
            id = Mapel.id,
            NamaMapel = Mapel.NamaMapel,
            IsComplete = Mapel.IsComplete
        };
    }
}
