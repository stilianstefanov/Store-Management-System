﻿namespace WarehouseService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using static Common.ExceptionMessages;

    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly WarehousesDbContext _dbContext;

        public WarehouseRepository(WarehousesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await _dbContext.Warehouses
                .Include(w => w.Products)
                .ToArrayAsync();
        }

        public async Task<Warehouse?> GetByIdAsync(string id)
        {
            var warehouse = await _dbContext.Warehouses
                .Include(w => w.Products)
                .FirstOrDefaultAsync(w => w.Id == Guid.Parse(id));

            return warehouse ?? throw new InvalidOperationException(WarehouseNotFound);
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            await _dbContext.Warehouses.AddAsync(warehouse);
        }

        public async Task<Warehouse> UpdateAsync(string id, Warehouse warehouse)
        {
            var warehouseToUpdate = await _dbContext.Warehouses
                .FirstOrDefaultAsync(w => w.Id == Guid.Parse(id));

            if (warehouseToUpdate == null)
            {
                throw new InvalidOperationException(WarehouseNotFound);
            }

            warehouseToUpdate.Name = warehouse.Name;
            warehouseToUpdate.Type = warehouse.Type;

            return warehouseToUpdate;
        }
    }
}