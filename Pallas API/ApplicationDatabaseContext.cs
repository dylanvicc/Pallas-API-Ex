using Microsoft.EntityFrameworkCore;
using Pallas_API.Model.Tables;

namespace Pallas_API
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<RegisteredUser> RegisteredUsers { get; set; } = null!;

        public DbSet<InventoryItem> Inventory { get; set; } = null!;
    }
}
