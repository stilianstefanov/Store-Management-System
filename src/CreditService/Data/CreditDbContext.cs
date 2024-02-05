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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Borrower>()
                .Property(p => p.CreditLimit)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
