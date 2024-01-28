namespace WarehouseService.Data.Repositories.Contracts
{
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        Task AddProductAsync(Product product);

        Task<bool> ExternalProductExistsAsync(string externalProductId);
    }
}
