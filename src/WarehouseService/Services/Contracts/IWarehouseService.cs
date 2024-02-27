namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IWarehouseService
    {
        Task<OperationResult<IEnumerable<WarehouseViewModel>>> GetAllAsync(string userId);

        Task<OperationResult<WarehouseViewModel>> CreateAsync(WarehouseReadModel model, string userId);

        Task<OperationResult<WarehouseViewModel>> GetByIdAsync(string id, string userId);

        Task<OperationResult<WarehouseViewModel>> UpdateAsync(string id, WarehouseReadModel model, string userId);

        Task<bool> ExistsAsync(string id);
    }
}
