using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public int MilestoneId { get; set; }
        public Milestone? Milestone { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public string AreaUnit { get; set; }
        public int HealthCondition { get; set; }
        public string PlantingMethod { get; set; }
        public string? Image { get; set; }
        public string? QRCode { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }

        public virtual IList<Diary>? Diaries { get; set; }
        public virtual IList<TakeAndSendMaterial>? TakeAndSendMaterials { get; set; }
    }
}
