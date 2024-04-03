using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public double Value { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public int TypeId { get; set; }
        public TypeItem TypeItem { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public virtual IList<TakeAndSendMaterial>? TakeAndSendMaterials { get; set; }
        
    }
}
