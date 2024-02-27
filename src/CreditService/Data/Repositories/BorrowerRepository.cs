namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly CreditDbContext _dbContext;

        public BorrowerRepository(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Borrower>> GetAllBorrowersAsync(string userId)
        {
            return await _dbContext.Borrowers
                .Where(b => !b.IsDeleted && b.UserId == userId)
                .ToArrayAsync();
        }

        public async Task<Borrower?> GetBorrowerByIdAsync(string id)
        {
            var borrower = await _dbContext.Borrowers
                .FirstOrDefaultAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            return borrower;
        }

        public async Task AddBorrowerAsync(Borrower borrower)
        {
            await _dbContext.Borrowers.AddAsync(borrower);
        }

        public async Task DeleteBorrowerAsync(string id)
        {
            var borrowerToDelete = await _dbContext.Borrowers
                .Include(b => b.Purchases)
                .ThenInclude(p => p.Products)
                .FirstAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            borrowerToDelete.IsDeleted = true;

            foreach (var purchase in borrowerToDelete.Purchases)
            {
                purchase.IsDeleted = true;

                foreach (var product in purchase.Products)
                {
                    product.IsDeleted = true;
                }
            }
        }

        public async Task<bool> BorrowerExistsAsync(string id)
        {
            return await _dbContext.Borrowers.AnyAsync(b => b.Id.ToString() == id && !b.IsDeleted);
        }
    }
}
