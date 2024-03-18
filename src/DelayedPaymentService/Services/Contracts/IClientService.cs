namespace DelayedPaymentService.Services.Contracts
{
    using Data.ViewModels.Client;
    using Utilities;

    public interface IClientService
    {
        Task<OperationResult<IEnumerable<ClientViewModel>>> GetAllClientsAsync(string userId);

        Task<OperationResult<ClientViewModel>> GetClientByIdAsync(string id, string userId);

        Task<OperationResult<ClientViewModel>> CreateClientAsync(ClientCreateModel client, string userId);

        Task<OperationResult<ClientViewModel>> UpdateClientAsync(string id, ClientUpdateModel client, string userId);

        Task<OperationResult<ClientViewModel>> DecreaseClientCreditAsync(string id, decimal amount, string userId);

        Task DecreaseClientCreditAsync(string id, decimal amount);

        Task<OperationResult<bool>> DeleteClientAsync(string id, string userId);

        Task<bool> ClientExistsAsync(string id);

        Task IncreaseClientCreditAsync(string id, decimal amount);

        Task<bool> ClientHasEnoughCreditAsync(string id, decimal amount);
    }
}
