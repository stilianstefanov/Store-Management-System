namespace DelayedPaymentService.Data.Repositories
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Client> GetAllClientsAsync(string userId)
        {
            return _dbContext.Clients
                .Where(b => !b.IsDeleted && b.UserId == userId);
        }

        public async Task<Client?> GetClientByIdAsync(string id)
        {
            var client = await _dbContext.Clients
                .FirstOrDefaultAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            return client;
        }

        public async Task AddClientAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
        }

        public async Task DeleteClientAsync(string id)
        {
            var clientToDelete = await _dbContext.Clients
                .Include(b => b.Purchases)
                .ThenInclude(p => p.Products)
                .FirstAsync(b => b.Id.ToString() == id && !b.IsDeleted);

            clientToDelete.IsDeleted = true;

            foreach (var purchase in clientToDelete.Purchases)
            {
                purchase.IsDeleted = true;

                foreach (var product in purchase.Products)
                {
                    product.IsDeleted = true;
                }
            }
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            return await _dbContext.Clients.AnyAsync(b => b.Id.ToString() == id && !b.IsDeleted);
        }
    }
}
