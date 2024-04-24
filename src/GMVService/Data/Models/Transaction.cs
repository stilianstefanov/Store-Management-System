namespace GMVService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }
    }
}
