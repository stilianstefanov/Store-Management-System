﻿namespace CreditService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Borrower;

    public class Borrower
    {
        public Borrower()
        {
            Id = Guid.NewGuid();
            Purchases = new HashSet<Purchase>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [MaxLength(SurnameMaxLength)]
        public string? Surname { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public decimal CurrentCredit { get; set; }

        public decimal CreditLimit { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
    }
}