namespace DelayedPaymentService.Data.ViewModels.PurchasedProduct
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.PurchaseProduct;

    public class PurchasedProductCreateModel
    {
        public string Id { get; set; } = null!;

        [Range(BoughtQuantityMinValue, BoughtQuantityMaxValue)]
        public int Quantity { get; set; }

        [Range(typeof(decimal), PurchasePriceMinValue, PurchasePriceMaxValue)]
        public decimal Price { get; set; }
    }
}
