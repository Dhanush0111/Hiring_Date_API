using Microsoft.EntityFrameworkCore;

namespace Hiring_Date_API.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Hiring> Hirings { get; set; }
        public DbSet<Company> Companies { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.CompanyId);

                entity.Property(c => c.CompanyName).IsRequired().HasMaxLength(200);

            });

            modelBuilder.Entity<Hiring>(entity => {
                
                entity.HasKey(h => h.HiringId);

                entity.Property(h => h.CompanyName).IsRequired().HasMaxLength(200);

                entity.Property(h => h.Activity).IsRequired().HasMaxLength(200);

                entity.HasOne(h => h.Company_CompanyId).WithMany().HasForeignKey(c => c.CompanyId);


            
            
            });
        }
    }
}
