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
    public interface IPlantRepo : IGenericRepo<Plant>
    {
        List<Plant> GetPlants();
        List<PlantRecordDTO> GetPlantDiaries(int plantid);
        Plant GetPlantById(int milestoneid);
        public int CreatePlant(Plant milestoneinfor);
        public bool UpdatePlant(Plant milestoneinfor);
        public bool DeletePlant(int milestoneid);

    }
}
