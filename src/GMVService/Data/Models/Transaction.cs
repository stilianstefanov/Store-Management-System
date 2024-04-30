namespace GMVService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;
    using static Common.EntityValidationConstants;

    public class Transaction
    {
        public Transaction()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [MaxLength(UserIdMaxLength)]
        public string UserId { get; set; } = null!;
    }
}
