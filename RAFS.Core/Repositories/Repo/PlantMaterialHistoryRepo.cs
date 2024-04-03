using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RAFS.Core.Repositories.Repo
{
    public class PlantMaterialHistoryRepo : GenericRepo<Item>, IPlantMaterialHistoryRepo
    {

        public PlantMaterialHistoryRepo(RAFSContext context) : base(context)
        {
        }

        public bool CreatePlantMaterialRecord(TakeAndSendMaterial milestoneinfor)
        {
            try
            {


                _context.TakeAndSendMaterials.Add(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePlantMaterialRecord(int milestoneid)
        {
            try {

                TakeAndSendMaterial milestones = GetPlantMaterialRecord(milestoneid);
                milestones.Status = false;
                _context.TakeAndSendMaterials.Update(milestones);

                return true;
            }
            catch {
                return false;
            }
        }

        public List<TakeAndSendMaterialDTO> GetFarmMaterialHistory(int farmid)
        {
            List<Plant> plants = new List<Plant>();
            List<Item> items = _context.Inventories.ToList();
            plants = _context.Farms
            .Where(farm => farm.Id == farmid && farm.Status == true)
            .SelectMany(farm => farm.Milestones.SelectMany(milestone => milestone.Plants).Where(x => x.Status == true))
            .ToList();
            List<TakeAndSendMaterialDTO> takeAndSendMaterialDTOs = new List<TakeAndSendMaterialDTO>();

            foreach(var plant in plants) { 
            List<TakeAndSendMaterial> diaries = _context.TakeAndSendMaterials.Where(x => x.PlantId == plant.Id && x.Status == true).ToList();

            if (diaries.Count > 0)
            {
                foreach (var diary in diaries)
                {
                    TakeAndSendMaterialDTO takeAndSendMaterialDTO = new TakeAndSendMaterialDTO();
                    takeAndSendMaterialDTO.Id = diary.Id;
                    takeAndSendMaterialDTO.InventoryId = diary.InventoryId;
                    takeAndSendMaterialDTO.PlantId = diary.PlantId;
                    takeAndSendMaterialDTO.InventoryName = items.FirstOrDefault(x => x.Id == diary.InventoryId).ItemName;
                    takeAndSendMaterialDTO.PlantName = plants.FirstOrDefault(x => x.Id == diary.PlantId).Name;
                    takeAndSendMaterialDTO.TypeId = items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId;
                    takeAndSendMaterialDTO.Quality = diary.Quality;
                    takeAndSendMaterialDTO.LastUpdate = diary.LastUpdate;
                    takeAndSendMaterialDTO.Status = diary.Status;
                    takeAndSendMaterialDTOs.Add(takeAndSendMaterialDTO);
                }
            }
            }
            return takeAndSendMaterialDTOs;
        }

        public List<TakeAndSendMaterialDTO> GetPlantMaterialHistory(int plantid)
        {

            List<Item> items = _context.Inventories.ToList();
            List<Plant> plants = _context.Plants.ToList();
            List<TakeAndSendMaterialDTO> takeAndSendMaterialDTOs = new List<TakeAndSendMaterialDTO>();

            
                List<TakeAndSendMaterial> diaries = _context.TakeAndSendMaterials.Where(x => x.PlantId == plantid && x.Status == true).ToList();

                if (diaries.Count > 0)
                {
                    foreach (var diary in diaries)
                    {
                        TakeAndSendMaterialDTO takeAndSendMaterialDTO = new TakeAndSendMaterialDTO();
                        takeAndSendMaterialDTO.Id = diary.Id;
                        takeAndSendMaterialDTO.InventoryId = diary.InventoryId;
                        takeAndSendMaterialDTO.PlantId = diary.PlantId;
                        takeAndSendMaterialDTO.InventoryName = items.FirstOrDefault(x => x.Id == diary.InventoryId).ItemName;
                        takeAndSendMaterialDTO.PlantName = plants.FirstOrDefault(x => x.Id == diary.PlantId).Name;
                        takeAndSendMaterialDTO.TypeId = items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId;
                        takeAndSendMaterialDTO.Quality = diary.Quality;
                        takeAndSendMaterialDTO.LastUpdate = diary.LastUpdate;
                        takeAndSendMaterialDTO.Status = diary.Status;
                        takeAndSendMaterialDTOs.Add(takeAndSendMaterialDTO);
                    }
                }

            


            return takeAndSendMaterialDTOs;
        }

        public List<TakeAndSendMaterialDTO> GetPlantMaterialHistoryByType(int plantid,int typeid)
        {
           
            List<Item> items = _context.Inventories.Where(x => x.TypeId == typeid).ToList();
            List<Plant> plants = _context.Plants.ToList();
          
            List<TakeAndSendMaterialDTO> takeAndSendMaterialDTOs = new List<TakeAndSendMaterialDTO>();

            foreach (var item in items)
            {
                List<TakeAndSendMaterial> diaries = _context.TakeAndSendMaterials.Where(x => x.PlantId == plantid && x.InventoryId== item.Id && x.Status == true).ToList();
               
                if (diaries.Count > 0)
                {
                    foreach (var diary in diaries)
                    {
                        TakeAndSendMaterialDTO takeAndSendMaterialDTO = new TakeAndSendMaterialDTO();
                        takeAndSendMaterialDTO.Id = diary.Id;
                        takeAndSendMaterialDTO.InventoryId = diary.InventoryId;
                        takeAndSendMaterialDTO.PlantId = diary.PlantId;
                        takeAndSendMaterialDTO.InventoryName = items.FirstOrDefault(x => x.Id == diary.InventoryId).ItemName;
                        takeAndSendMaterialDTO.PlantName = plants.FirstOrDefault(x => x.Id == diary.PlantId).Name;
                        takeAndSendMaterialDTO.TypeId = items.FirstOrDefault(x => x.Id == diary.InventoryId).TypeId;
                        takeAndSendMaterialDTO.Quality = diary.Quality;
                        takeAndSendMaterialDTO.LastUpdate = diary.LastUpdate;
                        takeAndSendMaterialDTO.Status = diary.Status;
                        takeAndSendMaterialDTOs.Add(takeAndSendMaterialDTO);
                    }
                }

            }

            return takeAndSendMaterialDTOs.ToList();
        }

            public TakeAndSendMaterial GetPlantMaterialRecord(int milestoneid)
        {
            TakeAndSendMaterial diary2 = _context.TakeAndSendMaterials.FirstOrDefault(x=>x.Id == milestoneid);
            
            return diary2;
        }

        public bool UpdatePlantMaterialRecord(TakeAndSendMaterial milestoneinfor)
        {
            try
            {

              
                _context.TakeAndSendMaterials.Update(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }


      
    }
}
