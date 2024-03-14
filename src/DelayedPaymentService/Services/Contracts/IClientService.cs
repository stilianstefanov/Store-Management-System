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

        Task<OperationResult<bool>> DeleteClientAsync(string id, string userId);

        Task<bool> ClientExistsAsync(string id);

        Task<bool> IncreaseClientCreditAsync(string id, decimal amount);

        Task DecreaseClientCreditAsync(string id, decimal amount);
    }
}
