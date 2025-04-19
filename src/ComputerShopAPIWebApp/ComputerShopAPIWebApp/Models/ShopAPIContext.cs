using Microsoft.EntityFrameworkCore;

namespace ComputerShopAPIWebApp.Models
{
    public class ShopAPIContext : DbContext
    {
        public virtual DbSet<Computer> Computers { get; set; }

        public ShopAPIContext(DbContextOptions<ShopAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
