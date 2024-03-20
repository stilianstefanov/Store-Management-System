namespace DelayedPaymentService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.ViewModels.Client;
    using Data.Repositories.Contracts;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<OperationResult<ClientsAllQueryModel>> GetAllClientsAsync(ClientsAllQueryModel queryModel, string userId)
        {
            var clientsQuery =  _clientRepository.GetAllClientsAsync(userId);

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                var wildCardSearchTerm = $"{queryModel.SearchTerm}%";

                clientsQuery = clientsQuery
                    .Where(c => EF.Functions.Like(c.Name, wildCardSearchTerm) ||
                                      EF.Functions.Like(c.Surname, wildCardSearchTerm) ||
                                      EF.Functions.Like(c.LastName, wildCardSearchTerm));
            }

            var clients = await clientsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.ClientsPerPage)
                .Take(queryModel.ClientsPerPage)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await clientsQuery.CountAsync() / (double)queryModel.ClientsPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Clients = _mapper.Map<IEnumerable<ClientViewModel>>(clients)!;

            return OperationResult<ClientsAllQueryModel>.Success(queryModel);
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

        public async Task<OperationResult<ClientViewModel>> DecreaseClientCreditAsync(string id, decimal amount, string userId)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client == null || client.UserId != userId)
            {
                return OperationResult<ClientViewModel>.Failure(ClientNotFound, ErrorType.NotFound);
            }

            client.CurrentCredit -= amount;

            if (client.CurrentCredit < 0)
            {
                client.CurrentCredit = 0;
            }

            await _clientRepository.SaveChangesAsync();

            return OperationResult<ClientViewModel>.Success(_mapper.Map<ClientViewModel>(client)!);
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

        public async Task IncreaseClientCreditAsync(string id, decimal amount)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            client!.CurrentCredit += amount;

            await _clientRepository.SaveChangesAsync();
        }

        public async Task<bool> ClientHasEnoughCreditAsync(string id, decimal amount)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);

            if (client!.CreditLimit - client.CurrentCredit < amount)
            {
                return false;
            }

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
