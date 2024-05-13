namespace WarehouseService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class WarehousesDbContext : DbContext
    {
        public WarehousesDbContext(DbContextOptions<WarehousesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Warehouse> Warehouses { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Product>()
                .Property(p => p.MaxQuantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Product>()
                .Property(p => p.MinQuantity)
                .HasColumnType("decimal(18,3)");
        }
    }
}
