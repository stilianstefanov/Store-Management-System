namespace GMVService.Data.ViewModels
{
    public class TransactionsDailyTotal
    {
        public string Date { get; set; } = null!;

        public decimal TotalGmv { get; set; }

        public decimal TotalRegularGmv { get; set; }

        public decimal TotalDelayedGmv { get; set; }
    }
}
