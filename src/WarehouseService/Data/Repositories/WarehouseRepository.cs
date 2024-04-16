namespace WarehouseService.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models;

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

        public IQueryable<Warehouse> GetAllAsync(string userId)
        {
            return _dbContext.Warehouses
                .Where(w => w.UserId == userId);
        }

        public async Task<Warehouse?> GetByIdAsync(string id)
        {
            var warehouse = await _dbContext.Warehouses
                .FirstOrDefaultAsync(w => w.Id.ToString() == id);

            return warehouse;
        }

        public async Task AddAsync(Warehouse warehouse)
        {
            await _dbContext.Warehouses.AddAsync(warehouse);
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            return await _dbContext.Warehouses
                .AnyAsync(w => w.Id.ToString() == id);
        }
    }
}
