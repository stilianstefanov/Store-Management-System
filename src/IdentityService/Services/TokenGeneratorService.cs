namespace IdentityService.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Contracts;
    using Data.Models;
    using Microsoft.IdentityModel.Tokens;
    using static Common.ApplicationConstants;

    public class TokenGeneratorService : ITokenGeneratorService
    {
        public string GenerateToken(ApplicationUser user, IConfiguration configuration, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(configuration["JWT:Secret"]!);
            var validAudiences = configuration.GetSection("JWT:ValidAudiences").Get<string[]>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("CompanyName", user.CompanyName),
                new Claim("JwtId", Guid.NewGuid().ToString())
            };

            foreach (var audience in validAudiences!)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TokenExpirationInHours),
                Issuer = configuration["JWT:ValidIssuer"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
