﻿namespace WarehouseService.Services
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
            var warehouses = _mapper.Map<IEnumerable<WarehouseViewModel>>(await _repository.GetAllAsync());

            return OperationResult<IEnumerable<WarehouseViewModel>>.Success(warehouses);
        }

        public async Task<OperationResult<WarehouseViewModel>> CreateAsync(WarehouseReadModel model)
        {
            var warehouse = _mapper.Map<Warehouse>(model);

            await _repository.AddAsync(warehouse);

            await _repository.SaveChangesAsync();

            return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
        }

        public async Task<OperationResult<WarehouseViewModel>> GetByIdAsync(string id)
        {
            var warehouse = await _repository.GetByIdAsync(id);

            return warehouse == null 
                ? OperationResult<WarehouseViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound) 
                : OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(warehouse));
        }

        public async Task<OperationResult<WarehouseViewModel>> UpdateAsync(string id, WarehouseReadModel model)
        {
            var updatedWarehouse = await _repository.UpdateAsync(id, _mapper.Map<Warehouse>(model));

            if (updatedWarehouse == null)
            {
                return OperationResult<WarehouseViewModel>.Failure(WarehouseNotFound, ErrorType.NotFound);
            }
            
            await _repository.SaveChangesAsync();

            return OperationResult<WarehouseViewModel>.Success(_mapper.Map<WarehouseViewModel>(updatedWarehouse));
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _repository.ExistsByIdAsync(id);
        }
    }
}
