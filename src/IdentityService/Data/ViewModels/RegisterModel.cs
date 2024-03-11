namespace IdentityService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;
    using static Common.ExceptionMessages;

    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(CompanyNameMaxLength, MinimumLength = CompanyNameMinLength, ErrorMessage = CompanyNameLengthError)]
        public string CompanyName { get; set; } = null!;

        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = UserNameLengthError)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [Compare(nameof(Password), ErrorMessage = PasswordConfirmationError)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
