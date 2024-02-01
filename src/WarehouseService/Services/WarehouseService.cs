﻿namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels;

    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;
        private readonly IMapper _mapper;

        public WarehouseService(IWarehouseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WarehouseViewModel>> GetAllAsync()
        {
            var warehouses = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<WarehouseViewModel>>(warehouses);
        }

        public async Task<WarehouseViewModel> CreateAsync(WarehouseReadModel model)
        {
            var warehouse = _mapper.Map<Warehouse>(model);

            await _repository.AddAsync(warehouse);

            await _repository.SaveChangesAsync();

            return _mapper.Map<WarehouseViewModel>(warehouse);
        }

        public async Task<WarehouseViewModel> GetByIdAsync(string id)
        {
            var warehouse = await _repository.GetByIdAsync(id);

            return _mapper.Map<WarehouseViewModel>(warehouse);
        }

        public async Task<WarehouseViewModel> UpdateAsync(string id, WarehouseReadModel model)
        {
            var updatedWarehouse = await _repository.UpdateAsync(id, _mapper.Map<Warehouse>(model));

            await _repository.SaveChangesAsync();

            return _mapper.Map<WarehouseViewModel>(updatedWarehouse);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _repository.ExistsByIdAsync(id);
        }
    }
}
