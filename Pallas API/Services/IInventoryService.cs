using Pallas_API.Model.Tables;

namespace Pallas_API.Services
{
    public interface IInventoryService
    {
        Task<InventoryItem[]> GetInventoryItemsForMetricAsync(string metric);
    }
}
