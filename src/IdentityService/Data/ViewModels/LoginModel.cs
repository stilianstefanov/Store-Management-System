namespace IdentityService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;

    public class LoginModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength)]
        public string Password { get; set; } = null!;
    }
}
