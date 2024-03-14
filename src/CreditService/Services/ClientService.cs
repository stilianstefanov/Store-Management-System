namespace CreditService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.ViewModels.Client;
    using Data.Repositories.Contracts;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<ClientViewModel>>> GetAllClientsAsync(string userId)
        {
            var clients = await _clientRepository.GetAllClientsAsync(userId);

            return OperationResult<IEnumerable<ClientViewModel>>.Success(
                _mapper.Map<IEnumerable<ClientViewModel>>(clients)!);
        }

        public async Task<OperationResult<ClientViewModel>> GetClientByIdAsync(string id, string userId)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null || client.UserId != userId)
            {
                return OperationResult<ClientViewModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            return OperationResult<ClientViewModel>.Success(_mapper.Map<ClientViewModel>(client)!);
        }

        public async Task<OperationResult<ClientViewModel>> CreateClientAsync(ClientCreateModel model, string userId)
        {
            var newClient = _mapper.Map<Client>(model)!;

            newClient.UserId = userId;

            await _clientRepository.AddClientAsync(newClient);

            await _clientRepository.SaveChangesAsync();

            return OperationResult<ClientViewModel>.Success(_mapper.Map<ClientViewModel>(newClient)!);
        }

        public async Task<OperationResult<ClientViewModel>> UpdateClientAsync(string id, ClientUpdateModel model, string userId)
        {
            var clientToUpdate = await _clientRepository.GetClientByIdAsync(id);

            if (clientToUpdate == null || clientToUpdate.UserId != userId)
            {
                return OperationResult<ClientViewModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            _mapper.Map(model, clientToUpdate);

            await _clientRepository.SaveChangesAsync();

            return OperationResult<ClientViewModel>.Success(_mapper.Map<ClientViewModel>(clientToUpdate)!);
        }

        public async Task<OperationResult<bool>> DeleteClientAsync(string id, string userId)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null || client.UserId != userId)
            {
                return OperationResult<bool>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            await _clientRepository.DeleteClientAsync(id);

            await _clientRepository.SaveChangesAsync();

            return OperationResult<bool>.Success(true);
        }

        public async Task<bool> ClientExistsAsync(string id)
        {
            return await _clientRepository.ClientExistsAsync(id);
        }

        public async Task<bool> IncreaseClientCreditAsync(string id, decimal amount)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client!.CreditLimit - client.CurrentCredit < amount)
            {
                return false;
            }

            client.CurrentCredit += amount;

            await _clientRepository.SaveChangesAsync();

            return true;
        }

        public async Task DecreaseClientCreditAsync(string id, decimal amount)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            client!.CurrentCredit -= amount;

            if (client.CurrentCredit < 0)
            {
                client.CurrentCredit = 0;
            }

            await _clientRepository.SaveChangesAsync();
        }
    }
}
