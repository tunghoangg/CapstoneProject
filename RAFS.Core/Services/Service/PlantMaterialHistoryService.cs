using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.Service
{
    public class PlantMaterialHistoryService : IPlantMaterialHistoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PlantMaterialHistoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool CreatePlantMaterialRecord(TakeAndSendMaterial milestone)
        {


            try
            {
                if(_uow.plantRepo.GetPlantById(milestone.PlantId).Status==false)
                {
                    return false;
                }
                else { 

                milestone.LastUpdate = DateTime.UtcNow;
                milestone.Status = true;

                Item item = _uow.plantMaterialHistoryRepo.GetById(milestone.InventoryId);
                if (item.TypeId == 18)
                {
                    item.Quantity += Int32.Parse(milestone.Quality);
                    _uow.plantMaterialHistoryRepo.CreatePlantMaterialRecord(milestone);

                    _uow.plantMaterialHistoryRepo.Update(item);
                        _uow.Save();
                        return true;
                    }
                else
                {
                    if (Int32.Parse(milestone.Quality) > item.Quantity)
                    {
                        return false;
                    }
                    else
                    {
                         item.Quantity -= Int32.Parse(milestone.Quality);
                        _uow.plantMaterialHistoryRepo.CreatePlantMaterialRecord(milestone);
                        _uow.plantMaterialHistoryRepo.Update(item);
                        _uow.Save();
                         return true;
                        }
                }
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePlantMaterialRecord(int milestoneId)
        {
            try
            {
                
                var record = _uow.plantMaterialHistoryRepo.GetPlantMaterialRecord(milestoneId);

                Item item = _uow.plantMaterialHistoryRepo.GetById(record.InventoryId);

                if (item.TypeId == 18)
                {
                    if(Int32.Parse(record.Quality) > item.Quantity)
                    {
                        return false;
                    }
                    else
                    {
                        item.Quantity -= Int32.Parse(record.Quality);
                    }
                   
                }
                else { 
                item.Quantity += Int32.Parse(record.Quality);
                }
                _uow.plantMaterialHistoryRepo.Update(item);

                _uow.plantMaterialHistoryRepo.DeletePlantMaterialRecord(milestoneId);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<TakeAndSendMaterialDTO> GetAllPlantMaterialHistoryByType(int plantid, int farmid)
        {
            List<TakeAndSendMaterialDTO> milestones = _uow.plantMaterialHistoryRepo.GetPlantMaterialHistoryByType(plantid,farmid);
           

            return milestones; 
        }
        public List<TakeAndSendMaterialDTO> GetAllFarmMaterialHistory(int farmid)
        {
            List<TakeAndSendMaterialDTO> milestones = _uow.plantMaterialHistoryRepo.GetFarmMaterialHistory(farmid);

        
            return milestones;
        }
        public List<TakeAndSendMaterialDTO> GetAllPlantMaterialHistory(int planid)
        {
            List<TakeAndSendMaterialDTO> milestones = _uow.plantMaterialHistoryRepo.GetPlantMaterialHistory(planid);
          

            return milestones;
        }

        public TakeAndSendMaterialDTO GetPlantMaterialRecord(int milestoneId)
        {
            TakeAndSendMaterial milestones = _uow.plantMaterialHistoryRepo.GetPlantMaterialRecord(milestoneId);
            List<Plant> plants = _uow.plantRepo.GetPlants();
            List<Item> items = _uow.plantMaterialHistoryRepo.GetAll();
            TakeAndSendMaterialDTO takeAndSendMaterialDTO = new TakeAndSendMaterialDTO();

                takeAndSendMaterialDTO.Id = milestones.Id;
            takeAndSendMaterialDTO.InventoryId = milestones.InventoryId;
            takeAndSendMaterialDTO.PlantId = milestones.PlantId;
            takeAndSendMaterialDTO.InventoryName = items.FirstOrDefault(x => x.Id == milestones.InventoryId).ItemName;
                takeAndSendMaterialDTO.PlantName = plants.FirstOrDefault(x => x.Id == milestones.PlantId).Name;
            takeAndSendMaterialDTO.TypeId = items.FirstOrDefault(x => x.Id == milestones.InventoryId).TypeId;

            takeAndSendMaterialDTO.Quality = milestones.Quality;
                takeAndSendMaterialDTO.LastUpdate = milestones.LastUpdate;
                takeAndSendMaterialDTO.Status = milestones.Status;
            

            return takeAndSendMaterialDTO; 
        }


        public bool UpdatePlantMaterialRecord(TakeAndSendMaterial newrecord)
        {
            newrecord.Status = true;
            try
            {
                var oldrecord = _uow.plantMaterialHistoryRepo.GetPlantMaterialRecord(newrecord.Id);

                if (oldrecord.PlantId != newrecord.PlantId || oldrecord.InventoryId != newrecord.InventoryId ||
                     oldrecord.PlantId != newrecord.PlantId && oldrecord.InventoryId != newrecord.InventoryId ||
                     oldrecord.PlantId != newrecord.PlantId && oldrecord.InventoryId != newrecord.InventoryId && oldrecord.Quality.Equals(newrecord.Quality))
                {
                    TakeAndSendMaterial createrecord = new TakeAndSendMaterial();
                    createrecord.PlantId = newrecord.PlantId;
                    createrecord.InventoryId = newrecord.InventoryId;
                    createrecord.Quality = newrecord.Quality;
                    this.CreatePlantMaterialRecord(createrecord);
                    this.DeletePlantMaterialRecord(oldrecord.Id);
                    _uow.Save();
                }
                else { 
                Item item = _uow.plantMaterialHistoryRepo.GetById(newrecord.InventoryId);

                    if (item.TypeId == 18)
                    {
                        if (Int32.Parse(oldrecord.Quality) > Int32.Parse(newrecord.Quality))
                        {
                            int amount = Int32.Parse(oldrecord.Quality) - Int32.Parse(newrecord.Quality);
                            item.Quantity -= amount;
                            _uow.plantMaterialHistoryRepo.Update(item);
                            _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                            _uow.Save();
                            return true;
                        }
                        else if (Int32.Parse(oldrecord.Quality) == Int32.Parse(newrecord.Quality))
                        {
                            _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                            _uow.Save();
                            return true;
                        }
                        else if (Int32.Parse(oldrecord.Quality) < Int32.Parse(newrecord.Quality))
                        {
                            int amount = Int32.Parse(newrecord.Quality) - Int32.Parse(oldrecord.Quality);
                            if (amount > item.Quantity)
                            {
                                return false;
                            }
                            else
                            {
                                item.Quantity += amount;
                                _uow.plantMaterialHistoryRepo.Update(item);
                                _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                                _uow.Save();
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (Int32.Parse(oldrecord.Quality) > Int32.Parse(newrecord.Quality))
                        {
                            int amount = Int32.Parse(oldrecord.Quality) - Int32.Parse(newrecord.Quality);
                            item.Quantity += amount;
                            _uow.plantMaterialHistoryRepo.Update(item);
                            _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                            _uow.Save();
                            return true;
                        }
                        else if(Int32.Parse(oldrecord.Quality) == Int32.Parse(newrecord.Quality))
                        {
                            _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                            _uow.Save();
                            return true;
                        }
                        else if(Int32.Parse(oldrecord.Quality) < Int32.Parse(newrecord.Quality))
                        {
                            int amount = Int32.Parse(newrecord.Quality) - Int32.Parse(oldrecord.Quality);
                            if(amount > item.Quantity)
                            {
                                return false;
                            }
                            else
                            {
                                item.Quantity -= amount;
                                _uow.plantMaterialHistoryRepo.Update(item);
                                _uow.plantMaterialHistoryRepo.UpdatePlantMaterialRecord(newrecord);
                                _uow.Save();
                                return true;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Item> GetInventoryItems(int farmid)
        {
            return _uow.plantMaterialHistoryRepo.GetAll().Where(x=>x.FarmId == farmid).ToList();
        }
    }
}
