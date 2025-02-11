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
            return await _context.Companys.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Company> GetByCompanyId(int id)
        {
            return await _context.Companys.FindAsync(id);
        }
    }
}
