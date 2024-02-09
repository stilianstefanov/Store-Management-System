namespace CreditService.Data.ViewModels.PurchaseProduct
{
    public class PurchaseProductViewModel
    {
        public string Id { get; set; } = null!;

        public int BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public ProductDetailsViewModel ProductDetails { get; set; } = null!;
    }
}
