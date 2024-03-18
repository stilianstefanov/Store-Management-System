namespace DelayedPaymentService.Services.GrpcServices.Contracts
{
    using Data.ViewModels.PurchasedProduct;

    public interface IProductGrpcClientService
    {
        public Task<IEnumerable<ProductDetailsViewModel>> GetProductsAsync(IEnumerable<string> ids);

        public Task DecreaseProductsStocksAsync(IEnumerable<PurchasedProductCreateModel> purchasedProducts);
    }
}
