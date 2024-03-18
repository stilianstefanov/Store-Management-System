namespace ProductService.Services.Contracts
{
    using Data.Models;
    using Data.ViewModels;
    using Utilities;

    public interface IProductService
    {
        Task<OperationResult<IEnumerable<ProductViewModel>>> GetAllAsync(string userId);

        Task<OperationResult<ProductDetailsViewModel>> CreateAsync(ProductCreateModel model, string userId);

        Task<OperationResult<ProductDetailsViewModel>> GetByIdAsync(string id, string userId);

        Task<OperationResult<ProductDashViewModel>> GetByBarcodeAsync(string barcode, string userId);

        Task<OperationResult<ProductDetailsViewModel>> UpdateAsync(string id, ProductUpdateModel model, string userId);

        Task<OperationResult<ProductDetailsViewModel>> PartialUpdateAsync(string id, ProductPartialUpdateModel model, string userId);

        Task<OperationResult<bool>> DecreaseStocksAsync(IEnumerable<ProductStockUpdateModel> models, string userId);

        Task<OperationResult<bool>> DeleteAsync(string id, string userId);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids);
    }
}
