using Microsoft.AspNetCore.Http;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class DiaryDTO
    {
        public int Id { get; set; }

        public string PlantName { get; set; }
        public int PlantId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDay { get; set; }
        public string Image { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
    }
    public class DiaryImageDTO
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public Plant? Plant { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDay { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
    }
    public class DiaryOnlyImageDTO
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; }


    }
}
