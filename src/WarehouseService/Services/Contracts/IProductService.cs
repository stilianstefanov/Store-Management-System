namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;

    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsByWarehouseIdAsync(string warehouseId);
    }
}
