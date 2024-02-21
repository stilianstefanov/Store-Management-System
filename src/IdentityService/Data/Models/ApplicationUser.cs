namespace IdentityService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        public string CompanyName { get; set; } = null!;
    }
}
