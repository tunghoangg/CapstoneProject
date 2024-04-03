using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class MilestoneDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FarmId { get; set; }
        public string FarmName { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }

    }

    public class MilestoneDTOVer2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int NumberOfPlants { get; set; }
        public int totalpage { get; set; }
        public bool Status { get; set; }

    }
}
