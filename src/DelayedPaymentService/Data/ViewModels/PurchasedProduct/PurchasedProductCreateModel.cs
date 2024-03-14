namespace DelayedPaymentService.Data.ViewModels.PurchasedProduct
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.PurchaseProduct;

    public class PurchasedProductCreateModel
    {
        public string ExternalId { get; set; } = null!;

        [Range(BoughtQuantityMinValue, BoughtQuantityMaxValue)]
        public int BoughtQuantity { get; set; }

        [Range(typeof(decimal), PurchasePriceMinValue, PurchasePriceMaxValue)]
        public decimal PurchasePrice { get; set; }
    }
}
