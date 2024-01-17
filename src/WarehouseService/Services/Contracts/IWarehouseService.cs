namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseViewModel>> GetAllAsync();

        Task<WarehouseViewModel> CreateAsync(WarehouseCreateModel model);

        Task<WarehouseViewModel> GetByIdAsync(string id);


    }
}
