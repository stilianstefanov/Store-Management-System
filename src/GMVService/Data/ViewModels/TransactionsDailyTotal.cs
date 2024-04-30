namespace GMVService.Data.ViewModels
{
    public class TransactionsDailyTotal
    {
        public DateTime Day { get; set; }

        public decimal TotalGmv { get; set; }

        public decimal TotalRegularGmv { get; set; }

        public decimal TotalDelayedGmv { get; set; }
    }
}
