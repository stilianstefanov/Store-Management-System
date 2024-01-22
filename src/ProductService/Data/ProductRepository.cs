namespace ProductService.Data
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

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

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.Where(p => !p.IsDeleted).ToArrayAsync();
        }

        public async Task<Product?> GetByIdAsync(string id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id) && !p.IsDeleted);

            return product ?? throw new InvalidOperationException(ProductNotFound);
        }

        public async Task AddAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<Product> UpdateAsync(string id, Product product)
        {
            var productToUpdate = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id) && !p.IsDeleted);

            if (productToUpdate == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }
            
            productToUpdate.Barcode = product.Barcode;
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.MinQuantity = product.MinQuantity;
            productToUpdate.MaxQuantity = product.MaxQuantity;

            return productToUpdate;
        }

        public async Task DeleteAsync(string id)
        {
            var productToDelete = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id) && !p.IsDeleted);

            if (productToDelete == null)
            {
                throw new InvalidOperationException(ProductNotFound);
            }

            productToDelete.IsDeleted = true;
        }
    }
}
