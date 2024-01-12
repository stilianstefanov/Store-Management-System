namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using Data.Models;
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

        public async Task CreateAsync(WarehouseCreateModel model)
        {
            var warehouse = _mapper.Map<Warehouse>(model);

            await _repository.AddAsync(warehouse);

            await _repository.SaveChangesAsync();
        }
    }
}
