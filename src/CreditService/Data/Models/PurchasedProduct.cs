namespace CreditService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PurchasedProduct
    {
        public PurchasedProduct()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string ExternalId { get; set; } = null!;

        public int BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public bool IsDeleted { get; set; }


        [ForeignKey(nameof(Purchase))]
        public Guid PurchaseId { get; set; }

        public Purchase Purchase { get; set; } = null!;
    }
}
