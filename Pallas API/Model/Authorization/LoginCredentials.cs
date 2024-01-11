using System.ComponentModel.DataAnnotations;

namespace Pallas_API.Models.Authorization
{
    public class LoginCredentials
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
