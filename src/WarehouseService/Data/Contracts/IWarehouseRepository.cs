﻿namespace WarehouseService.Data.Contracts
{
    using Models;

    public interface IWarehouseRepository
    {
        Task SaveChangesAsync();

        Task<IEnumerable<Warehouse>> GetAllAsync();

        Task<Warehouse?> GetByIdAsync(string id);

        Task AddAsync(Warehouse warehouse);

        Task<Warehouse> UpdateAsync(string id, Warehouse warehouse);

        Task DeleteAsync(string id);
    }
}
