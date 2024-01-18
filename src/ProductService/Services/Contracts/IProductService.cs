namespace ProductService.Services.Contracts
{
    using Data.ViewModels;

    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync();

        Task<ProductDetailsViewModel> CreateAsync(ProductCreateModel model);
    }
}
