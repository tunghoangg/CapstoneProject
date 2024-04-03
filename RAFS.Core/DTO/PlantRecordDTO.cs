using Microsoft.AspNetCore.Http;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    
    public class PlantRecordDTO
    {
        public string Description { get; set; }
        public DateTime createdate { get; set; }
        public int type { get; set; }

    }

    public class PlantDeleteDTO
    {
        public string Name { get; set; }
        public string PlantingMethod { get; set; }
        public string Image { get; set; }

    }
    public class PlantRecordResponseDTO
    {
        public IEnumerable<PlantRecordDTO> PlantRecords { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Area { get; set; }
        public string AreaUnit { get; set; }
        public string PlantingMethod { get; set; }
        public string Status { get; set; }
    }

}
 