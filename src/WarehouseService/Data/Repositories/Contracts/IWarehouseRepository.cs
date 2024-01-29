namespace WarehouseService.Data.Repositories.Contracts
{
    using Models;

    public interface IWarehouseRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Warehouse>> GetAllAsync();

        Task<Warehouse?> GetByIdAsync(string id);

        Task AddAsync(Warehouse warehouse);

        Task<Warehouse> UpdateAsync(string id, Warehouse warehouse);
    }
}
