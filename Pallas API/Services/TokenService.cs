using Microsoft.IdentityModel.Tokens;
using Pallas_API.Models.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pallas_API
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 90;

        public string BuildToken(string key, string issuer, LoginCredentials login)
        {
            if (login.Username == null || login.Password == null)
                return string.Empty;

            var claims = new[] {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature);
            var descriptor = new JwtSecurityToken(issuer, issuer, claims, expires: DateTime.UtcNow.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(descriptor);
        }

        public bool IsTokenValid(string key, string issuer, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}