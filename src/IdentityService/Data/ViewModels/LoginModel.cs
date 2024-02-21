namespace IdentityService.Data.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
