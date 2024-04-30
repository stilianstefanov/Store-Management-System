namespace GMVService.Data.ViewModels
{
    public class TransactionsMonthlyTotal
    {
        public int Month { get; set; }

        public decimal TotalGmv { get; set; }

        public decimal TotalRegularGmv { get; set; }

        public decimal TotalDelayedGmv { get; set; }
    }
}
