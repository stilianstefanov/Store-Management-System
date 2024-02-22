namespace IdentityService.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using static Common.ApplicationConstants;

    public class ApplicationUser : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(CompanyNameMaxLength)]
        public string CompanyName { get; set; } = null!;
    }
}
