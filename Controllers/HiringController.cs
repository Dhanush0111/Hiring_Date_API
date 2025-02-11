using Hiring_Date_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hiring_Date_API.Controllers
{
    public class HiringController : Controller
    {
       

        private readonly ApplicationDbContext _context;

        public HiringController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<List<Hiring>> GetHirings()
        {
            return await _context.Hirings.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetbyId(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Ok(await _context.Hirings.FindAsync(id));
            
        }

        [HttpPost]
        public async Task<ActionResult<Hiring>> Create(Hiring hiring)
        {
            if (hiring == null)
            {
                return BadRequest("Invalid input");
            }
            _context.Hirings.Add(hiring);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetbyId), new { id = hiring.HiringId },hiring);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Hiring hiring)
        {
            if (id != hiring.HiringId)
            {
                return BadRequest("Invalid Input");
            }
            var user = await _context.Hirings.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.CompanyName = hiring.CompanyName;
            user.CompanyId = hiring.CompanyId;
            user.Activity = hiring.Activity;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletebyId(int id)
        {
            var user = await _context.Hirings.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            _context.Hirings.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
