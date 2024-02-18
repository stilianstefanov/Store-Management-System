namespace ProductService.Data.Contracts
{
    using Models;

    public interface IProductRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(string id);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids);

        Task AddAsync(Product product);

        Task<Product?> UpdateAsync(string id, Product product);

        Task DeleteAsync(string id);

        Task<bool> ProductExistsAsync(string id);

        Task<bool> ProductsExistAsync(IEnumerable<string> ids);
    }
}
