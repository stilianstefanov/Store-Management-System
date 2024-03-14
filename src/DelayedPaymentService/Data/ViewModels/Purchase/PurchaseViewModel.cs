namespace DelayedPaymentService.Data.ViewModels.Purchase
{
    public class PurchaseViewModel
    {
        public string Id { get; set; } = null!;

        public string Date { get; set; } = null!;

        public decimal Amount { get; set; }
    }
}
