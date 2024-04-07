using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class TakeAndSendMaterialDTO
    {
        public int Id { get; set; }
        public int InventoryId{ get; set; }
        public string InventoryName { get; set; }
        public string Unit { get; set; }

        public int TypeId { get; set; }
        
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public string Quality { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }

    }

}
