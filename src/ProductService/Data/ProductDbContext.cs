namespace ProductService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.DeliveryPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Product>()
                .Property(p => p.MaxQuantity)
                .HasColumnType("decimal(18,3)");

            modelBuilder.Entity<Product>()
                .Property(p => p.MinQuantity)
                .HasColumnType("decimal(18,3)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
