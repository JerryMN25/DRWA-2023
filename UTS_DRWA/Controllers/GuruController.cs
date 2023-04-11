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
    public class GuruController : ControllerBase
    {
        private readonly TodoContext _context;

        public GuruController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Gurus
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guru>>> GetGurus()
    {
        return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{Nip}")]
    public async Task<ActionResult<Guru>> GetGuru(long Nip)
    {
        var Guru = await _context.TodoItems.FindAsync(Nip);

        if (Guru == null)
        {
            return NotFound();
        }

        return ItemToDTO(Guru);
    }

    [HttpPost]
    public async Task<ActionResult<Guru>> CreateGuru(Guru Guru)
    {
        var Gurus = new Guru
        {
            IsComplete = Guru.IsComplete,
            NamaGuru = Guru.NamaGuru
        };

        _context.TodoItems.Add(Gurus);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetGuru),
            new { Nip = Gurus.Nip },
            ItemToDTO(Gurus));
    }

    private bool GuruExists(string Nip) =>
        _context.TodoItems.Any(e => e.Nip == Nip);

    private static Guru ItemToDTO(Guru Guru) =>
        new Guru
        {
            Nip = Guru.Nip,
            NamaGuru = Guru.NamaGuru,
            IsComplete = Guru.IsComplete
        };
    }
}
