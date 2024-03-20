namespace DelayedPaymentService.Data.Repositories.Contracts
{
    using Models;

    public interface IClientRepository
    {
        Task SaveChangesAsync();

        IQueryable<Client> GetAllClientsAsync(string userId);

        Task<Client?> GetClientByIdAsync(string id);

        Task AddClientAsync(Client client);

        Task DeleteClientAsync(string id);

        Task<bool> ClientExistsAsync(string id);
    }
}
