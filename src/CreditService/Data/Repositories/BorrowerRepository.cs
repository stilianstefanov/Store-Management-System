namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

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

        public async Task<IEnumerable<Borrower>> GetAllBorrowersAsync()
        {
            return await _dbContext.Borrowers.Where(b => !b.IsDeleted).ToArrayAsync();
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

        public async Task<Borrower> UpdateBorrowerAsync(string id, Borrower borrower)
        {
            var borrowerToUpdate = await _dbContext.Borrowers
                .FirstOrDefaultAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            if (borrowerToUpdate == null)
            {
                throw new KeyNotFoundException(BorrowerNotFound);
            }

            borrowerToUpdate.Name = borrower.Name;
            borrowerToUpdate.Surname = borrower.Surname;
            borrowerToUpdate.LastName = borrower.LastName;
            borrowerToUpdate.CurrentCredit = borrower.CurrentCredit;
            borrowerToUpdate.CreditLimit = borrower.CreditLimit;

            return borrowerToUpdate;
        }

        public async Task DeleteBorrowerAsync(string id)
        {
            var borrowerToDelete = await _dbContext.Borrowers
                .Include(b => b.Purchases)
                .ThenInclude(p => p.Products)
                .FirstOrDefaultAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            if (borrowerToDelete == null)
            {
                throw new KeyNotFoundException(BorrowerNotFound);
            }

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
