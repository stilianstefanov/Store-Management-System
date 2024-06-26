﻿namespace WarehouseService.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Messaging.Models;
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

        public IQueryable<Product> GetProductsByWarehouseIdAsync(string warehouseId)
        {
            var products = _dbContext.Products
                .Where(p => p.WarehouseId.ToString() == warehouseId && !p.IsDeleted);

            return products;
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
            var productToUpdate = await _dbContext.Products.FirstAsync(p => p.ExternalId == updatedDto.Id);

            productToUpdate.Name = updatedDto.Name;
            productToUpdate.Quantity = updatedDto.Quantity;
            productToUpdate.MinQuantity = updatedDto.MinQuantity;
            productToUpdate.MaxQuantity = updatedDto.MaxQuantity;
        }

        public async Task DeleteProductAsync(string externalProductId)
        {
            var productToDelete = await _dbContext.Products.FirstAsync(p => p.ExternalId == externalProductId);

            productToDelete.IsDeleted = true;
        }

        public async Task<Product?> GetProductByExternalId(string id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ExternalId == id);

            return product;
        }
    }
}
