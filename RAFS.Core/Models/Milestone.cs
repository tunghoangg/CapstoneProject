using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Milestone
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public Farm? Farm { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Farm>? Farms { get; set; }
        public virtual IList<Plant>? Plants { get; set; }

    }
}
