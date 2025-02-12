using AutoMapper;
using Hiring_Date_API.DTOs;
using Hiring_Date_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hiring_Date_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public HiringController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

     
        [HttpGet]

        public async Task<IActionResult> GetHirings()
        {
            var hirings = await _context.Hirings
                 .Include(h => h.Company_CompanyId) // Inner join with Company
                 .Select(h => new
                 {
                     h.HiringId,
                     h.Activity,
                     h.HiringDate,
        
                     CompanyName = h.Company_CompanyId != null ? h.Company_CompanyId.CompanyName : "Unknown"
                 })
                 .ToListAsync();
            return Ok(hirings);
        }
  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(int? id)
        {
            var hiring = await _context.Hirings
                .Include(h => h.Company_CompanyId)
                .FirstOrDefaultAsync(h => h.HiringId == id);

            if (hiring == null)
            {
                return NotFound();
            }

            // Use AutoMapper to convert to DTO
            var hiringDto = _mapper.Map<HiringDto>(hiring);

            return Ok(hiringDto);
        }


        [HttpPost]
        public async Task<ActionResult<HiringDto>> Create(HiringCreateDto hiringCreateDto)
        {
            if (hiringCreateDto == null)
            {
                return BadRequest("Invalid input");
            }

            // Check if CompanyId exists
            var existingCompany = await _context.Companies.FindAsync(hiringCreateDto.CompanyId);
            if (existingCompany == null)
            {
                return BadRequest($"Company with ID {hiringCreateDto.CompanyId} does not exist.");
            }

            // Map DTO to Entity
            var hiring = _mapper.Map<Hiring>(hiringCreateDto);

            _context.Hirings.Add(hiring);
            await _context.SaveChangesAsync();

            // Retrieve the saved entity with included company details
            var createdHiring = await _context.Hirings
                .Include(h => h.Company_CompanyId)
                .FirstOrDefaultAsync(h => h.HiringId == hiring.HiringId);

            // Map entity to DTO for response
            var hiringDto = _mapper.Map<HiringDto>(createdHiring);

            return CreatedAtAction(nameof(GetbyId), new { id = hiringDto.HiringId }, hiringDto);
        }


      

         [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HiringUpdateDto hiringUpdateDto)
        {
            // Find the existing hiring entry
            var hiring = await _context.Hirings.Include(h => h.Company_CompanyId).FirstOrDefaultAsync(h => h.HiringId == id);
            if (hiring == null)
            {
                return NotFound();
            }

            // Validate if CompanyId exists
            var existingCompany = await _context.Companies.FindAsync(hiringUpdateDto.CompanyId);
            if (existingCompany == null)
            {
                return BadRequest($"Company with ID {hiringUpdateDto.CompanyId} does not exist.");
            }

            // Use AutoMapper to update the entity
            _mapper.Map(hiringUpdateDto, hiring);

            await _context.SaveChangesAsync();

            // Map the updated entity to a DTO for response
            var hiringDto = _mapper.Map<HiringDto>(hiring);

            return Ok(hiringDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletebyId(int id)
        {
            var user = await _context.Hirings.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Hirings.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}

















//[HttpPut("{id}")]

//public async Task<IActionResult> Update(int id, Hiring hiring)
//{
//    if (id != hiring.HiringId)
//    {
//        return BadRequest("Invalid Input");
//    }
//    var user = await _context.Hirings.FindAsync(id);
//    if (user == null)
//    {
//        return NotFound();
//    }
//    // Validate if CompanyId exists
//    var existingCompany = await _context.Companies.FindAsync(hiring.CompanyId);
//    if (existingCompany == null)
//    {
//        return BadRequest($"Company with ID {hiring.CompanyId} does not exist.");
//    }
//    user.CompanyId = hiring.CompanyId;
//    user.Activity = hiring.Activity;
//    await _context.SaveChangesAsync();
//    var response = new
//    {
//        user.HiringId,
//        user.Activity,
//        CompanyName = existingCompany.CompanyName
//    };

//    return Ok(response);
//}



//use automapper
//[HttpPost]
//public async Task<ActionResult<Hiring>> Create(Hiring hiring)
//{
//    if (hiring == null)
//    {
//        return BadRequest("Invalid input");
//    }

//    // Check if CompanyId exists
//    var existingCompany = await _context.Companies.FindAsync(hiring.CompanyId);
//    if (existingCompany == null)
//    {
//        return BadRequest($"Company with ID {hiring.CompanyId} does not exist.");
//    }

//    _context.Hirings.Add(hiring);
//    await _context.SaveChangesAsync();

//    // Return a DTO instead of the full entity
//    var response = new
//    {
//        hiring.HiringId,
//        hiring.Activity,
//        hiring.CompanyId,
//        CompanyName = existingCompany.CompanyName
//    };

//    return CreatedAtAction(nameof(GetbyId), new { id = hiring.HiringId }, response);

//}

//[HttpGet("{id}")]

//public async Task<IActionResult> GetbyId(int? id)
//{
//    var hiring = await _context.Hirings
// .Include(h => h.Company_CompanyId) // Include Company details
// .Where(h => h.HiringId == id)
// .Select(h => new
// {
//     h.HiringId,
//     h.Activity,
//     CompanyName = h.Company_CompanyId != null ? h.Company_CompanyId.CompanyName : "Unknown"
// })
// .FirstOrDefaultAsync();

//    if (hiring == null)
//    {
//        return NotFound();
//    }


//    return Ok(hiring);
//}

//[HttpGet]

//public async Task<List<Hiring>> GetHirings()
//{
//    return await _context.Hirings.ToListAsync();
//}