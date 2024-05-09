namespace GMVService.Data.ViewModels
{
    public class TransactionDetailsModel
    {
        public string Id { get; set; } = null!;

        public string DateTime { get; set; } = null!;

        public decimal Amount { get; set; }

        public string Type { get; set; } = null!;
    }
}
