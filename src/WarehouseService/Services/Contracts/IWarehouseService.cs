namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;

    public interface IWarehouseService
    {
        Task<IEnumerable<WarehouseViewModel>> GetAllAsync();

        Task CreateAsync(WarehouseCreateModel model);
    }
}
