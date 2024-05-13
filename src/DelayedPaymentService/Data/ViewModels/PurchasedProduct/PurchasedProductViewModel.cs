namespace DelayedPaymentService.Data.ViewModels.PurchasedProduct
{
    public class PurchasedProductViewModel
    {
        public string Id { get; set; } = null!;

        public decimal BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public ProductDetailsViewModel ProductDetails { get; set; } = null!;
    }
}
