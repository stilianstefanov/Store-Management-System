namespace CreditService.Data.ViewModels.PurchaseProduct
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.PurchaseProduct;

    public class PurchaseProductCreateModel
    {
        public string ExternalId { get; set; } = null!;

        [Range(BoughtQuantityMinValue, BoughtQuantityMaxValue)]
        public int BoughtQuantity { get; set; }

        [Range(typeof(decimal), PurchasePriceMinValue, PurchasePriceMaxValue)]
        public decimal PurchasePrice { get; set; }
    }
}
