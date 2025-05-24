using Microsoft.EntityFrameworkCore;

namespace ComputerShopAPIWebApp.Models
{
    public class ShopAPIContext : DbContext
    {
        public ShopAPIContext(DbContextOptions<ShopAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Computer> Computers { get; set; } = null!;
        public DbSet<Processor> Processors { get; set; } = null!;
        public DbSet<GPU> GPUs { get; set; } = null!;
        public DbSet<RAM> RAMs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Computer>()
                .HasOne(c => c.Processor)
                .WithMany(p => p.Computers)
                .HasForeignKey(c => c.ProcessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Computer>()
                .HasOne(c => c.GPU)
                .WithMany(g => g.Computers)
                .HasForeignKey(c => c.GPUID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Computer>()
                .HasOne(c => c.RAM)
                .WithMany(r => r.Computers)
                .HasForeignKey(c => c.RAMId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
