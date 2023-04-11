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
    public class JadwalController : ControllerBase
    {
        private readonly TodoContext3 _context;

        public JadwalController(TodoContext3 context)
        {
            _context = context;
        }

        // GET: api/Jadwals
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Jadwal>>> GetJadwals()
    {
        return await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    [HttpGet("{nip}")]
    public async Task<ActionResult<Jadwal>> GetJadwalNIP(long nip)
    {
        var Jadwal = await _context.TodoItems.FindAsync(nip);

        if (Jadwal == null)
        {
            return NotFound();
        }

        return ItemToDTO(Jadwal);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Jadwal>> GetJadwalId(long id)
    {
        var Jadwal = await _context.TodoItems.FindAsync(id);

        if (Jadwal == null)
        {
            return NotFound();
        }

        return ItemToDTO(Jadwal);
    }

    [HttpPost]
    public async Task<ActionResult<Jadwal>> CreateJadwal(Jadwal Jadwal)
    {
        var Jadwals = new Jadwal
        {
            IsComplete = Jadwal.IsComplete,
            JadwalGuru = Jadwal.JadwalGuru
        };

        _context.TodoItems.Add(Jadwals);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetJadwals),
            new { idJad = Jadwals.idJad },
            ItemToDTO(Jadwals));
    }

    private bool JadwalExists(string idJad) =>
        _context.TodoItems.Any(e => e.idJad == idJad);

    private static Jadwal ItemToDTO(Jadwal Jadwal) =>
        new Jadwal
        {
            idJad = Jadwal.idJad,
            JadwalGuru = Jadwal.JadwalGuru,
            IsComplete = Jadwal.IsComplete
        };
    }
}
