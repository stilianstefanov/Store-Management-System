namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IProductService
    {
        Task<OperationResult<IEnumerable<ProductViewModel>>> GetProductsByWarehouseIdAsync(string warehouseId);
    }
}
