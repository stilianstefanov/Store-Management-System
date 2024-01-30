namespace ProductService.Services.Contracts
{
    using Data.ViewModels;

    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductDetailsViewModel> CreateAsync(ProductCreateModel model);

        Task<ProductDetailsViewModel> GetByIdAsync(string id);

        Task<ProductDetailsViewModel> UpdateAsync(string id, ProductUpdateModel model);

        Task<ProductDetailsViewModel> PartialUpdateAsync(string id, ProductPartialUpdateModel model);

        Task DeleteAsync(string id);
    }
}
