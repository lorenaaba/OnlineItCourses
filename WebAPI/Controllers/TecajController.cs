using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineITCourses.Data;
using OnlineITCourses.Models;

namespace OnlineITCourses.Controllers;

[ApiController]
[Route("api/tecaj")]
public class TecajController : ControllerBase
{
    private readonly OnlineITCoursesDbContext _context;

    public TecajController(OnlineITCoursesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTecajevi()
    {
        var tecajevi = await _context.Tecajevi.Include(t => t.Grupa).ToListAsync();
        LogsController.LogAction("Dohvaćeni su svi tečajevi");
        return Ok(tecajevi);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTecajById(int id)
    {
        var tecaj = await _context.Tecajevi.Include(t => t.Grupa).FirstOrDefaultAsync(t => t.Id == id);
        if (tecaj == null)
        {
            LogsController.LogAction($"Tecaj {id} nije pronaden.", "Error");
            return NotFound(new { message = "Tecaj nije pronaden." });
        }
        LogsController.LogAction($"Dohvaćen tečaj s Id-om {id}.");
        return Ok(tecaj);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTecaj(Tecaj tecaj)
    {
        try
        {
            _context.Tecajevi.Add(tecaj);
            await _context.SaveChangesAsync();
            LogsController.LogAction($"Stvoren tecaj s Id-om {tecaj.Id}.");
            return CreatedAtAction(nameof(GetTecajById), new {id = tecaj.Id}, tecaj);
        }
        catch (Exception ex)
        {
            LogsController.LogAction("Pogreška prilikom stvaranja tečaja.", "Error");
            return StatusCode(500, new { message = "Pogreška prilikom stvaranja tečaja.", Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTecaj(int id, Tecaj tecaj)
    {
        if (id != tecaj.Id)
        {
            LogsController.LogAction($"Tecaj {id} nije pronaden.", "Error");
            return BadRequest(new {Message = "Tecaj nije pronaden." });
        }

        try
        {
            _context.Entry(tecaj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            LogsController.LogAction($"Azurirani tecaj s Id-om {id}.");
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TecajExists(id))
            {
                LogsController.LogAction($"Tečaj s ID-om {id} nije pronađen.", "Error");
                return NotFound(new { Message = $"Tečaj s ID-om {id} nije pronađen." });
            }
            else
            {
                throw;
            }
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTecaj(int id)
    {
        var tecaj = await _context.Tecajevi.FindAsync(id);
        if (tecaj == null)
        {
            LogsController.LogAction($"Tecaj {id} nije pronaden.", "Error");
            return NotFound(new { message = "Tecaj nije pronaden." });
        }
        _context.Tecajevi.Remove(tecaj);
        await _context.SaveChangesAsync();
        LogsController.LogAction($"Obrisan tecaj s Id-om {id}.");
        return NoContent();
    }

    private bool TecajExists(int id)
    {
        return _context.Tecajevi.Any(e => e.Id == id);
    }
}