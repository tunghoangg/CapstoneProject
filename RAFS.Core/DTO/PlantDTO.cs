using Microsoft.AspNetCore.Http;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class PlantDTO
    {
        public int Id { get; set; }
      
        public IFormFile Image { get; set; }


    }
    public class PlantDTO2
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
        public IFormFile? Image { get; set; }
        public string? QRCode { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }


    }

}
 