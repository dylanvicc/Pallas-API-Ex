using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallas_API.Model.Tables
{
    public class InventoryItem
    {
        [Key, Column("Index", Order = 0)]
        public string Index { get; set; } = null!;

        public string Metric { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
