namespace IdentityService.Services.Contracts
{
    using Data.Models;

    public interface ITokenGeneratorService
    {
        string GenerateToken(ApplicationUser user, IConfiguration configuration, IEnumerable<string> roles);
    }
}
