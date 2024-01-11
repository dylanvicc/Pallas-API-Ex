using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallas_API.Model.Tables
{
    public class RegisteredUser
    {
        [Key, Column("Username", Order = 0)]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}