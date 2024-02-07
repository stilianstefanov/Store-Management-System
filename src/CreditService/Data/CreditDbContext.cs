namespace CreditService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CreditDbContext : DbContext
    {
        public CreditDbContext(DbContextOptions<CreditDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Borrower> Borrowers { get; set; } = null!;

        public DbSet<Purchase> Purchases { get; set; } = null!;

        public DbSet<PurchaseProduct> PurchaseProducts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrower>()
                .Property(p => p.CreditLimit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Borrower>()
                .Property(p => p.CurrentCredit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<PurchaseProduct>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Purchase>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
