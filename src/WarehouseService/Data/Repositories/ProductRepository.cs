namespace WarehouseService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ProductRepository : IProductRepository
    {
        private readonly WarehousesDbContext _dbContext;

        public ProductRepository(WarehousesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<bool> ExternalProductExistsAsync(string externalProductId)
        {
            return await _dbContext.Products.AnyAsync(p => p.ExternalId == externalProductId);
        }
    }
}
