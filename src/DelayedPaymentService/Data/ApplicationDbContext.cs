namespace DelayedPaymentService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Purchase> Purchases { get; set; } = null!;

        public DbSet<PurchasedProduct> PurchasedProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(p => p.CreditLimit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Client>()
                .Property(p => p.CurrentCredit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchasedProduct>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchasedProduct>()
                .Property(p => p.BoughtQuantity)
                .HasColumnType("decimal(18,3)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
