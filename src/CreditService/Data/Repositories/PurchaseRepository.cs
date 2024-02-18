namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly CreditDbContext _dbContext;

        public PurchaseRepository(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByBorrowerIdAsync(string borrowerId)
        {
            var purchases = await _dbContext.Purchases
                .Where(p => p.BorrowerId.ToString() == borrowerId && !p.IsDeleted)
                .Include(p => p.Products)
                .ToArrayAsync();

            return purchases;
        }

        public async Task<Purchase?> GetPurchaseByIdAsync(string id)
        {
            var purchase = await _dbContext.Purchases
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            return purchase;
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            await _dbContext.Purchases.AddAsync(purchase);
        }

        public async Task<decimal> DeletePurchaseAsync(string id)
        {
            var purchase = await _dbContext.Purchases
                .Include(p => p.Products)
                .FirstAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            var purchaseAmount = purchase.Products
                .Where(p => !p.IsDeleted)
                .Sum(p => p.PurchasePrice * p.BoughtQuantity);

            purchase.IsDeleted = true;

            foreach (var product in purchase.Products)
            {
                product.IsDeleted = true;
            }

            return purchaseAmount;
        }

        public async Task<bool> PurchaseExistsAsync(string id)
        {
            var purchaseExists = await _dbContext.Purchases
                .AnyAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            return purchaseExists;
        }
    }
}
