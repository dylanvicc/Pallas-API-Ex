using Microsoft.EntityFrameworkCore;
using Pallas_API.Models.Authorization;
using System.Security.Cryptography;
using System.Text;

namespace Pallas_API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDatabaseContext _context;

        public LoginService(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateLoginAsync(LoginCredentials credentials)
        {
            var user = await _context.RegisteredUsers.FirstOrDefaultAsync(it => it.Username == credentials.Username
                && it.Password == HashPassword(credentials.Password));

            return user != null;
        }

        public string HashPassword(string? password)
        {
            if (password == null)
                return string.Empty;

            using var hash = SHA1.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
