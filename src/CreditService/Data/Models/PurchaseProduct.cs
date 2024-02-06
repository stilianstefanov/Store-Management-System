namespace CreditService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PurchaseProduct
    {
        public PurchaseProduct()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string ExternalId { get; set; } = null!;

        public int BoughtQuantity { get; set; }

        public decimal PurchasePrice { get; set; }


        [ForeignKey(nameof(Purchase))]
        public Guid PurchaseId { get; set; }

        public Purchase Purchase { get; set; } = null!;
    }
}
