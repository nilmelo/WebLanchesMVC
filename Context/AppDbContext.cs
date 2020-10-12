using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebLanchesMVC.Models;

namespace WebLanchesMVC.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

       public DbSet<Lunch> Lunches { get; set; }
       public DbSet<Category> Categories { get; set; }
	   public DbSet<CartPurchaseItem> CartPurchaseItens { get; set; }
	   public DbSet<Order> Orders { get; set; }
	   public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
