using Microsoft.EntityFrameworkCore;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Context
{
    public class AppDbContext : DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

       public DbSet<Lunch> Lunches { get; set; }
       public DbSet<Category> Categories { get; set; }
    }
}