using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class FarmDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
    }

    public class FarmResponses()
    {
        public List<FarmDTO> Farms { get; set; } = new List<FarmDTO>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class HomepageFarmDetailsDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string PageLink { get; set; }
        public double Area { get; set; }
        public virtual ICollection<FarmImageDTO>? ImageURL { get; set; } = null!;

        public virtual ICollection<FarmDetailPlantDTO>? Plants { get; set; } = null!;
    }

    public class FarmDetailPlantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
    }

    public class FarmImageDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
    }
}
