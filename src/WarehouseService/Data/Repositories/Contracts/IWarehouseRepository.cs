namespace WarehouseService.Data.Repositories.Contracts
{
    using Models;

    public interface IWarehouseRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Warehouse>> GetAllAsync(string userId);

        Task<Warehouse?> GetByIdAsync(string id);

        Task AddAsync(Warehouse warehouse);

        Task<bool> ExistsByIdAsync(string id);
    }
}
