namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseViewModel>> GetAllAsync();

        Task<WarehouseViewModel> CreateAsync(WarehouseReadModel model);

        Task<WarehouseViewModel> GetByIdAsync(string id);

        Task<WarehouseViewModel> UpdateAsync(string id, WarehouseReadModel model);

        Task DeleteAsync(string id);
    }
}
