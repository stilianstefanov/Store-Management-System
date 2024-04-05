namespace DelayedPaymentService.Data.ViewModels.Purchase
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.EntityValidationConstants.Purchase;
    using static Common.ApplicationConstants;

    public class PurchasesAllQueryModel
    {
        public PurchasesAllQueryModel()
        {
            CurrentPage = DefaultPage;
            Purchases = new HashSet<PurchaseViewModel>();
        }

        public DateTime Date { get; set; }

        [Range(PurchaseSortingMinValue, PurchaseSortingMaxValue)]
        public PurchaseSorting Sorting { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<PurchaseViewModel> Purchases { get; set; }
    }
}
