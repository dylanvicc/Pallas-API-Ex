using Microsoft.EntityFrameworkCore;
using Pallas_API.Model.Tables;

namespace Pallas_API.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDatabaseContext _context;

        public InventoryService(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        public async Task<InventoryItem[]> GetInventoryItemsForMetricAsync(string metric)
        {
            return await _context.Inventory.Where(it => it.Metric == metric).ToArrayAsync();
        }
    }
}
