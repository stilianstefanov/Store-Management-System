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

        public async Task<OperationResult<IEnumerable<WarehouseViewModel>>> GetAllAsync()
        {
            try
            {
                var warehouses = _mapper.Map<IEnumerable<WarehouseViewModel>>(await _repository.GetAllAsync());

                return OperationResult<IEnumerable<WarehouseViewModel>>.Success(warehouses);
            }
            catch (Exception ex)
            {
                return OperationResult<IEnumerable<WarehouseViewModel>>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<WarehouseViewModel>> CreateAsync(WarehouseReadModel model)
        {
            var warehouse = _mapper.Map<Warehouse>(model);

            try
            {
                await _repository.AddAsync(warehouse);

                await _repository.SaveChangesAsync();

                return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
            }
            catch (Exception ex)
            {
                return OperationResult<WarehouseViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<WarehouseViewModel>> GetByIdAsync(string id)
        {
            try
            {
                var warehouse = await _repository.GetByIdAsync(id);

                return warehouse == null 
                    ? OperationResult<WarehouseViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound) 
                    : OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
            }
            catch (Exception ex)
            {
                return OperationResult<WarehouseViewModel>.Failure(ex.Message);
            }
        }

        public async Task<OperationResult<WarehouseViewModel>> UpdateAsync(string id, WarehouseReadModel model)
        {
            try
            {
                var updatedWarehouse = await _repository.UpdateAsync(id, _mapper.Map<Warehouse>(model));

                await _repository.SaveChangesAsync();

                return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(updatedWarehouse));
            }
            catch (KeyNotFoundException ex)
            {
                return OperationResult<WarehouseViewModel>.Failure(ex.Message, ErrorType.NotFound);
            }
            catch (Exception ex)
            {
                return OperationResult<WarehouseViewModel>.Failure(ex.Message);
            }
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _repository.ExistsByIdAsync(id);
        }
    }
}
