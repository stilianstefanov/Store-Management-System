namespace CreditService.Data.ViewModels.PurchaseProduct
{
    public class PurchaseProductCreateModel
    {
        public string ExternalId { get; set; } = null!;

        public int BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}
