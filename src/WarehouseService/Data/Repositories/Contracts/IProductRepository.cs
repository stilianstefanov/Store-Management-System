namespace WarehouseService.Data.Repositories.Contracts
{
    using Messaging.Models;
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        Task AddProductAsync(Product product);

        Task<bool> ExternalProductExistsAsync(string externalProductId);

        Task UpdateProductAsync(ProductUpdatedDto updatedDto);

        Task DeleteProductAsync(string externalProductId);

        Task<Product> GetProductByExternalId(string id);

        Task<IEnumerable<Product>> GetProductsByWarehouseIdAsync(string warehouseId);
    }
}
