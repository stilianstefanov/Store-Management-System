namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public WarehouseService(IWarehouseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<WarehouseViewModel>>> GetAllAsync(string userId)
        {
            var warehouses = _mapper.Map<IEnumerable<WarehouseViewModel>>(await _repository.GetAllAsync(userId));

            return OperationResult<IEnumerable<WarehouseViewModel>>.Success(warehouses);
        }

        public async Task<OperationResult<WarehouseViewModel>> CreateAsync(WarehouseReadModel model, string userId)
        {
            var warehouse = _mapper.Map<Warehouse>(model);

            warehouse.UserId = userId;

            await _repository.AddAsync(warehouse);

            await _repository.SaveChangesAsync();

            return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
        }

        public async Task<OperationResult<WarehouseViewModel>> GetByIdAsync(string id, string userId)
        {
            var warehouse = await _repository.GetByIdAsync(id);

            if (warehouse == null || warehouse.UserId != userId)
            {
                return OperationResult<WarehouseViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
            }

            return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
        }

        public async Task<OperationResult<WarehouseViewModel>> UpdateAsync(string id, WarehouseReadModel model, string userId)
        {
            var warehouseToUpdate = await _repository.GetByIdAsync(id);

            if (warehouseToUpdate == null || warehouseToUpdate.UserId != userId)
            {
                return OperationResult<WarehouseViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
            }

            _mapper.Map(model, warehouseToUpdate);
            
            await _repository.SaveChangesAsync();

            return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouseToUpdate));
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _repository.ExistsByIdAsync(id);
        }
    }
}
