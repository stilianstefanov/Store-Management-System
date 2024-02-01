namespace WarehouseService.Data.Repositories
{
    using Contracts;
    using Messaging.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

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
            return await _dbContext.Products.AnyAsync(p => p.ExternalId == externalProductId && !p.IsDeleted);
        }

        public async Task UpdateProductAsync(ProductUpdatedDto updatedDto)
        {
            var productToUpdate = await _dbContext.Products.FirstOrDefaultAsync(p => p.ExternalId == updatedDto.Id);

            if (productToUpdate == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            productToUpdate.Name = updatedDto.Name;
            productToUpdate.Quantity = updatedDto.Quantity;
            productToUpdate.MinQuantity = updatedDto.MinQuantity;
            productToUpdate.MaxQuantity = updatedDto.MaxQuantity;
        }

        public async Task DeleteProductAsync(string externalProductId)
        {
            var productToDelete = await _dbContext.Products.FirstOrDefaultAsync(p => p.ExternalId == externalProductId);

            if (productToDelete == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            productToDelete.IsDeleted = true;
        }

        public async Task<Product> GetProductByExternalId(string id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ExternalId == id);

            if (product == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByWarehouseIdAsync(string warehouseId)
        {
            var products = await _dbContext.Products
                .Where(p => p.WarehouseId.ToString() == warehouseId && !p.IsDeleted)
                .ToArrayAsync();

            return products;
        }
    }
}
