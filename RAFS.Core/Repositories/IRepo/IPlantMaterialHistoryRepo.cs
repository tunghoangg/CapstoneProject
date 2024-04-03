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
    public interface IPlantMaterialHistoryRepo : IGenericRepo<Item>
    {
        List<TakeAndSendMaterialDTO> GetFarmMaterialHistory(int farmid);
        List<TakeAndSendMaterialDTO> GetPlantMaterialHistory(int plantid);
        List<TakeAndSendMaterialDTO> GetPlantMaterialHistoryByType(int plantid,int type);
        TakeAndSendMaterial GetPlantMaterialRecord(int milestoneid);
        public bool CreatePlantMaterialRecord(TakeAndSendMaterial milestoneinfor);
        public bool UpdatePlantMaterialRecord(TakeAndSendMaterial milestoneinfor);
        public bool DeletePlantMaterialRecord(int milestoneidfarmid);

    }
}
