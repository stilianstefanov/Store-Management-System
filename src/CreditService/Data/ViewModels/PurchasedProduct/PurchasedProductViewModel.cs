namespace CreditService.Data.ViewModels.PurchasedProduct
{
    public class PurchasedProductViewModel
    {
        public string Id { get; set; } = null!;

        public int BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public ProductDetailsViewModel ProductDetails { get; set; } = null!;
    }
}
