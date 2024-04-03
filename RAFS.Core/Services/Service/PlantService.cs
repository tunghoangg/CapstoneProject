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
    public class PlantService : IPlantService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PlantService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool CreatePlant(Plant milestone)
        {


            try
            {
                

                int plantid = _uow.plantRepo.CreatePlant(milestone);
                _uow.Save();


                Diary diary = new Diary();
                diary.PlantId = plantid;
                diary.Title = milestone.Name + " đã được tạo";
                diary.Type = 4;
                diary.Body = "Trồng cây";
                diary.CreatedDay = DateTime.Now;
                diary.LastUpdate = DateTime.Now;
                diary.Image = "no much";
                diary.Status = true;
                _uow.diaryRepo.Add(diary);
                _uow.Save();

                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeletePlant(int milestoneId)
        {
            try
            {
                Plant plant = this.GetPlantById(milestoneId);
                Diary diary = new Diary();
                diary.PlantId = milestoneId;
                diary.Title = plant.Name + " đã bị hủy";
                diary.Type = 5;
                diary.Body = "Hủy cây";
                diary.CreatedDay = DateTime.Now;
                diary.LastUpdate = DateTime.Now;
                diary.Image = "no much";
                diary.Status = true;
                _uow.diaryRepo.Add(diary);
                _uow.Save();

                _uow.plantRepo.DeletePlant(milestoneId);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Plant> GetAllFarmPlant(int farmid)
        {
            List<Milestone> milestones1 = _uow.milestonRepo.GetMilestones(farmid);
            List<Plant> milestones = _uow.plantRepo.GetPlants();
            List<Plant> result = new List<Plant>();
            foreach(Milestone milestone in milestones1)
            {
                List<Plant> temp = milestones.Where(x => x.MilestoneId == milestone.Id).ToList();
                foreach(var plant in temp)
                {
                    result.Add(plant);
                }
            }



            return result; 
        }

        public List<PlantRecordDTO> GetAllPlantRecord(int plantid)
        {
            List<PlantRecordDTO> milestones = _uow.plantRepo.GetPlantDiaries(plantid);


            return milestones;
        }

     

        public Plant GetPlantById(int milestoneId)
        {
            Plant milestones = _uow.plantRepo.GetPlantById(milestoneId);
            

            return milestones; 
        }

        public bool UpdatePlant(Plant milestone)
        {
            try
            {
                
                _uow.plantRepo.UpdatePlant(milestone);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
