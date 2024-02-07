namespace CreditService.Data.ViewModels.Borrower
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Borrower;

    public class BorrowerCreateModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [MaxLength(SurnameMaxLength)]
        public string? Surname { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Range(typeof(decimal), CurrentCreditMinValue, CurrentCreditMaxValue)]
        public decimal CurrentCredit { get; set; }

        [Range(typeof(decimal), CreditLimitMinValue, CreditLimitMaxValue)]
        public decimal CreditLimit { get; set; }
    }
}
