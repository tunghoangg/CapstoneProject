using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IPlantMaterialHistoryService
    {
        public List<Item> GetInventoryItems(int farmid);
        public List<TakeAndSendMaterialDTO> GetAllPlantMaterialHistory(int planid);
        public List<TakeAndSendMaterialDTO> GetAllFarmMaterialHistory(int farmid);
        public List<TakeAndSendMaterialDTO> GetAllPlantMaterialHistoryByType(int plantid,int type);
        public TakeAndSendMaterialDTO GetPlantMaterialRecord(int milestoneId);
        public bool CreatePlantMaterialRecord(TakeAndSendMaterial milestone);
        public bool UpdatePlantMaterialRecord(TakeAndSendMaterial milestone);
        public bool DeletePlantMaterialRecord(int milestoneId);
       
    }
}
