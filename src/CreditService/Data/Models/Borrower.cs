namespace CreditService.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Borrower
    {
        public Borrower()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Surname { get; set; }

        public string LastName { get; set; } = null!;

        public decimal CreditLimit { get; set; }
    }
}
