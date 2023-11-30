using Microsoft.EntityFrameworkCore;
using TutorialProjectAPI.Models;

namespace TutorialProjectAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<Product> Products { get; set; }
    }
}
