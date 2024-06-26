﻿namespace ProductService.Data
{
    using Microsoft.EntityFrameworkCore;
    using Contracts;
    using Models;

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetAllAsync(string userId)
        {
            return _dbContext.Products
                .Where(p => !p.IsDeleted && p.UserId == userId);
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            return product;
        }

        public async Task<Product?> GetByBarcodeAsync(string barcode)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Barcode == barcode && !p.IsDeleted);

            return product;
        }

        public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids)
        {
            var products = await _dbContext.Products
                .Where(p => ids.Contains(p.Id.ToString()))
                .ToArrayAsync();

            return products;
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            var productToDelete = await _dbContext.Products
                .FirstAsync(p => p.Id.ToString() == id && !p.IsDeleted);

            productToDelete.IsDeleted = true;
        }
    }
}
