using Hiring_Date_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hiring_Date_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Company>> GetCompany()
        {
            return await _context.Companies.ToListAsync();
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Getbyid(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return Ok(await _context.Companies.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<Company>> CompanyPostDetails(Company company)
        {
            if (company == null)
            {
                return BadRequest("Invalid company detail");
            }
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Getbyid), new { id = company.CompanyId }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updating(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest("Invalid Input");
            }
            var user = await _context.Companies.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.CompanyName = company.CompanyName;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Companies.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Companies.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
