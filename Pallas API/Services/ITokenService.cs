using Pallas_API.Models.Authorization;

namespace Pallas_API
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, LoginCredentials credentials);

        bool IsTokenValid(string key, string issuer, string token);
    }
}
