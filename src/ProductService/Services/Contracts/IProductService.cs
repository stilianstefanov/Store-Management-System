namespace ProductService.Services.Contracts
{
    using Data.Models;
    using Data.ViewModels;
    using Messaging.Models.Enums;
    using Utilities;

    public interface IProductService
    {
        Task<OperationResult<ProductsAllQueryModel>> GetAllAsync(string userId, ProductsAllQueryModel queryModel);

        Task<OperationResult<ProductDetailsViewModel>> CreateAsync(ProductCreateModel model, string userId);

        Task<OperationResult<ProductDetailsViewModel>> GetByIdAsync(string id, string userId);

        Task<OperationResult<ProductDashViewModel>> GetByBarcodeAsync(string barcode, string userId);

        Task<OperationResult<ProductDetailsViewModel>> UpdateAsync(string id, ProductUpdateModel model, string userId);

        Task<OperationResult<ProductDetailsViewModel>> PartialUpdateAsync(string id, ProductPartialUpdateModel model, string userId);

        Task<OperationResult<bool>> DecreaseStocksAsync(IEnumerable<ProductStockUpdateModel> models, string userId, TransactionType transactionType = TransactionType.Regular);

        Task<OperationResult<bool>> DeleteAsync(string id, string userId);

        Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<string> ids);
    }
}
