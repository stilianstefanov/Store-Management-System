namespace IdentityService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;
    using static Common.ExceptionMessages;
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string CurrentPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string NewPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        [Compare(nameof(NewPassword), ErrorMessage = PasswordConfirmationError)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
