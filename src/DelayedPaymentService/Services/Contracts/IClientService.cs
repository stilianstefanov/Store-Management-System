namespace DelayedPaymentService.Services.Contracts
{
    using Data.ViewModels.Client;
    using Utilities;

    public interface IClientService
    {
        Task<OperationResult<ClientsAllQueryModel>> GetAllClientsAsync(ClientsAllQueryModel queryModel, string userId);

        Task<OperationResult<ClientViewModel>> GetClientByIdAsync(string id, string userId);

        Task<OperationResult<ClientViewModel>> CreateClientAsync(ClientCreateModel client, string userId);

        Task<OperationResult<ClientViewModel>> UpdateClientAsync(string id, ClientUpdateModel client, string userId);

        Task<OperationResult<ClientViewModel>> PartialUpdateClientAsync(string id, ClientPartialUpdateModel client, string userId);

        Task<OperationResult<ClientViewModel>> DecreaseClientCreditAsync(string id, decimal amount, string userId);

        Task<OperationResult<bool>> DeleteClientAsync(string id, string userId);

        Task DecreaseClientCreditAsync(string id, decimal amount);

        Task<bool> ClientExistsAsync(string id);

        Task IncreaseClientCreditAsync(string id, decimal amount);

        Task<bool> ClientHasEnoughCreditAsync(string id, decimal amount);
    }
}
