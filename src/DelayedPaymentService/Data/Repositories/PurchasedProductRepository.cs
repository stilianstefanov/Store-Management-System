namespace DelayedPaymentService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PurchasedProductRepository : IPurchasedProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PurchasedProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<PurchasedProduct>> GetProductsByPurchaseIdAsync(string purchaseId)
        {
            var products = await _dbContext.PurchasedProducts
                .Where(p => p.PurchaseId.ToString() == purchaseId && !p.IsDeleted)
                .ToArrayAsync();

            return products;
        }

        public async Task<decimal> DeleteProductByIdAsync(string id)
        {
            var product = await _dbContext.PurchasedProducts
                .FirstAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            product.IsDeleted = true;

            return product.PurchasePrice * product.BoughtQuantity;
        }

        public async Task<bool> ProductExistsAsync(string id)
        {
            return await _dbContext.PurchasedProducts.AnyAsync(p => p.Id.ToString() == id && !p.IsDeleted);
        }
    }
}
