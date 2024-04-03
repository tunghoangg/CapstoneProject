using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IFarmRepo : IGenericRepo<Farm>
    {
        List<Farm> GetFarmsInHomepage(string? search, string? province, string? district, string? ward, string? sort, string? area);
        Farm GetFarmDetailsInHomepage(int farmId);
        public List<FarmImageDTO> GetFarmImg(int farmid);
        public List<FarmDetailPlantDTO> GetFarmPlants(int farmId);
        List<Farm> GetNewestFarms();

    }
}
