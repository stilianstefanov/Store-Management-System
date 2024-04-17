namespace WarehouseService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IProductService
    {
        Task<OperationResult<ProductsAllQueryModel>> GetProductsByWarehouseIdAsync(string warehouseId, ProductsAllQueryModel queryModel);
    }
}
