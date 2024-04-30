namespace GMVService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.ApplicationConstants;
    using static Common.EntityValidationConstants;

    public class TransactionsQueryModel
    {
        public TransactionsQueryModel()
        {
            CurrentPage = DefaultPage;
            ItemsPerPage = DefaultItemsPerPage;

            Transactions = new HashSet<TransactionDetailsModel>();
            TransactionDailyTotals = new HashSet<TransactionsDailyTotal>();
            TransactionMonthlyTotals = new HashSet<TransactionsMonthlyTotal>();
        }

        [Range(PeriodMinValue, PerPageMaxValue)]
        public Period Period { get; set; }

        public DateTime Date { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; }

        public decimal TotalGmv { get; set; }

        public decimal TotalRegularGmv { get; set; }

        public decimal TotalDelayedGmv { get; set; }

        public ICollection<TransactionDetailsModel> Transactions { get; set; }

        public ICollection<TransactionsDailyTotal> TransactionDailyTotals { get; set; }

        public ICollection<TransactionsMonthlyTotal> TransactionMonthlyTotals { get; set; }
    }
}
