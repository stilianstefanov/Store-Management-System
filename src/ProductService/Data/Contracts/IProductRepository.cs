namespace ProductService.Data.Contracts
{
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Product>> GetAllAsync(string userId);

        Task<Product?> GetByIdAsync(string id);

        Task<Product?> GetByBarcodeAsync(string barcode);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids);

        Task AddAsync(Product product);

        Task DeleteAsync(string id);
    }
}
