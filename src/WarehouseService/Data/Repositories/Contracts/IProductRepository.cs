namespace WarehouseService.Data.Repositories.Contracts
{
    using Messaging.Models;
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        IQueryable<Product> GetProductsByWarehouseIdAsync(string warehouseId);

        Task AddProductAsync(Product product);

        Task<bool> ExternalProductExistsAsync(string externalProductId);

        Task UpdateProductAsync(ProductUpdatedDto updatedDto);

        Task DeleteProductAsync(string externalProductId);

        Task<Product?> GetProductByExternalId(string id);
    }
}
