using Microsoft.EntityFrameworkCore;

namespace Hiring_Date_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
    }
}
