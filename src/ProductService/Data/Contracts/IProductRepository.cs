namespace ProductService.Data.Contracts
{
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        IQueryable<Product> GetAllAsync(string userId);

        Task<Product?> GetByIdAsync(string id);

        Task<Product?> GetByBarcodeAsync(string barcode);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids);

        Task AddAsync(Product product);

        Task DeleteAsync(string id);
    }
}
