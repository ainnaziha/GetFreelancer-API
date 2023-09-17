using GetFreelancer_API.Models;
using Microsoft.EntityFrameworkCore;

namespace GetFreelancer_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Freelancer> Freelancers { get; set; }
    }
}
