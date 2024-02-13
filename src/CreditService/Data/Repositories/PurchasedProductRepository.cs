namespace CreditService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

    public class PurchasedProductRepository : IPurchasedProductRepository
    {
        private readonly CreditDbContext _dbContext;

        public PurchasedProductRepository(CreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PurchasedProduct>> GetProductsByPurchaseIdAsync(string purchaseId)
        {
            var products = await _dbContext.PurchaseProducts
                .Where(p => p.PurchaseId.ToString() == purchaseId && !p.IsDeleted)
                .ToArrayAsync();

            return products;
        }

        public async Task<decimal> DeleteProductByIdAsync(string id)
        {
            var product = await _dbContext.PurchaseProducts
                .FirstOrDefaultAsync(p => p.Id.ToString() == id);

            if (product == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            product.IsDeleted = true;

            return product.PurchasePrice * product.BoughtQuantity;
        }
    }
}
