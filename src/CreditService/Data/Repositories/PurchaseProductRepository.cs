namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

    public class PurchaseProductRepository : IPurchaseProductRepository
    {
        private readonly CreditDbContext _dbContext;

        public PurchaseProductRepository(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PurchaseProduct>> GetProductsByPurchaseIdAsync(string purchaseId)
        {
            var products = await _dbContext.PurchaseProducts
                .Where(p => p.PurchaseId.ToString() == purchaseId)
                .ToArrayAsync();

            return products;
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            var product = await _dbContext.PurchaseProducts
                .FirstOrDefaultAsync(p => p.Id.ToString() == id);

            if (product == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            product.IsDeleted = true;
        }
    }
}
