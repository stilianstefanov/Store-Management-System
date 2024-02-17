namespace CreditService.Services.GrpcServices.Contracts
{
    using Data.ViewModels.PurchasedProduct;

    public interface IProductGrpcClientService
    {
        public Task<IEnumerable<ProductDetailsViewModel>> GetProductsAsync(IEnumerable<string> ids);

        public Task<bool> ProductsExistAsync(IEnumerable<string> ids);
    }
}
