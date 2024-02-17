namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IWarehouseService
    {
        Task<OperationResult<IEnumerable<WarehouseViewModel>>> GetAllAsync();

        Task<OperationResult<WarehouseViewModel>> CreateAsync(WarehouseReadModel model);

        Task<OperationResult<WarehouseViewModel>> GetByIdAsync(string id);

        Task<OperationResult<WarehouseViewModel>> UpdateAsync(string id, WarehouseReadModel model);

        Task<bool> ExistsAsync(string id);
    }
}
