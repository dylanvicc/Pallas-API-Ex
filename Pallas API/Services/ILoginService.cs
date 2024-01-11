using Pallas_API.Models.Authorization;

namespace Pallas_API.Services
{
    public interface ILoginService
    {
        Task<bool> ValidateLoginAsync(LoginCredentials credentials);

        string HashPassword(string password);
    }
}
