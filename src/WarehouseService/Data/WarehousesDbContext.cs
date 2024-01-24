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
    }
}
