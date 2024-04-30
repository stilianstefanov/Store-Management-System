namespace GMVService.Data.ViewModels
{
    public class TransactionDetailsModel
    {
        public string Id { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; } = null!;
    }
}
