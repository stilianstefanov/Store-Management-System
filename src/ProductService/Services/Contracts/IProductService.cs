namespace ProductService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IProductService
    {
        Task<OperationResult<IEnumerable<ProductViewModel>>> GetAllAsync();

        Task<OperationResult<ProductDetailsViewModel>> CreateAsync(ProductCreateModel model);

        Task<OperationResult<ProductDetailsViewModel>> GetByIdAsync(string id);

        Task<OperationResult<ProductDetailsViewModel>> UpdateAsync(string id, ProductUpdateModel model);

        Task<OperationResult<ProductDetailsViewModel>> PartialUpdateAsync(string id, ProductPartialUpdateModel model);

        Task<OperationResult<bool>> DeleteAsync(string id);
    }
}
