namespace GMVService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;
    using static Common.EntityValidationConstants;

    public class TransactionsQueryModel
    {
        public TransactionsQueryModel()
        {
            CurrentPage = DefaultPage;
            ItemsPerPage = DefaultItemsPerPage;

            Transactions = new HashSet<object>();
        }

        public string Period { get; set; } = null!;

        public DateTime Date { get; set; }

        public int CurrentPage { get; set; }

        [Range(PerPageMinValue, PerPageMaxValue)]
        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; }

        public decimal TotalGmv { get; set; }

        public decimal TotalRegularGmv { get; set; }

        public decimal TotalDelayedGmv { get; set; }

        public IEnumerable<object> Transactions { get; set; }
    }
}
