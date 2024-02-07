namespace CreditService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Purchase
    {
        public Purchase()
        {
            Id = Guid.NewGuid();
            Products = new HashSet<PurchaseProduct>();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }


        [ForeignKey(nameof(Borrower))]
        public Guid BorrowerId { get; set; }

        public Borrower Borrower { get; set; } = null!;

        public ICollection<PurchaseProduct> Products { get; set; }
    }
}
