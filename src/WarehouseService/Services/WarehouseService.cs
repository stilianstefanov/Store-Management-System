namespace WarehouseService.Services
{
    using AutoMapper;
    using Contracts;
    using Data.Models;
    using Data.Repositories.Contracts;
    using Data.ViewModels;
    using Data.ViewModels.Enums;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<OperationResult<WarehouseAllQueryModel>> GetAllAsync(string userId, WarehouseAllQueryModel queryModel)
        {
            var warehousesQuery = _repository.GetAllAsync(userId);

            if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
            {
                var wildCardSearchTerm = $"{queryModel.SearchTerm}%";

                warehousesQuery = warehousesQuery
                    .Where(w => EF.Functions.Like(w.Name, wildCardSearchTerm) ||
                                EF.Functions.Like(w.Type, wildCardSearchTerm));
            }

            warehousesQuery = queryModel.Sorting switch
            {
                WarehouseSorting.NameAscending => warehousesQuery.OrderBy(w => w.Name),
                WarehouseSorting.NameDescending => warehousesQuery.OrderByDescending(w => w.Name),
                WarehouseSorting.ProductsCountAscending => warehousesQuery.OrderBy(w => w.Products.Count),
                WarehouseSorting.ProductsCountDescending => warehousesQuery.OrderByDescending(w => w.Products.Count),
                _ => warehousesQuery.OrderBy(w => w.Name)
            };

            var warehouses = await warehousesQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.WarehousesPerPage)
                .Take(queryModel.WarehousesPerPage)
                .Include(w => w.Products)
                .ToArrayAsync();

            var totalPages = (int)Math.Ceiling(await warehousesQuery.CountAsync() / (double)queryModel.WarehousesPerPage);

            queryModel.TotalPages = totalPages;
            queryModel.Warehouses = _mapper.Map<IEnumerable<WarehouseViewModel>>(warehouses);

            return OperationResult<WarehouseAllQueryModel>.Success(queryModel);
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
