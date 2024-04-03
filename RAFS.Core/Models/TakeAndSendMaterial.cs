using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class TakeAndSendMaterial
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public Item? Inventory { get; set; }
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }
        public string Quality { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
    }
}
