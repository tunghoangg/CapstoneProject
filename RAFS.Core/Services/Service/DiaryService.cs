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
    public class DiaryService : IDiaryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DiaryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool CreateDiary(Diary milestone)
        {


            try
            {
                _uow.diaryRepo.Add(milestone);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteDiary(int milestoneId)
        {
            try
            {

                _uow.diaryRepo.DeleteDiary(milestoneId);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<DiaryDTO> GetAllDiary(int farmid)
        {
            List<DiaryDTO> milestones = _uow.diaryRepo.GetDiaries(farmid).ToList();
            
           


            return milestones; 
        }

        public List<DiaryDTO> GetAllPlantDiary(int plantid)
        {
            List<DiaryDTO> milestones = _uow.diaryRepo.GetPlantDiaries(plantid);

            return milestones;
        }


        public List<DiaryDTO> GetAllPlantDiaryByType(int plantid, int type)
        {
            List<DiaryDTO> milestones = _uow.diaryRepo.GetPlantDiariesByType(plantid,type);

        


            return milestones;
        }
        public DiaryDTO GetDiaryById(int milestoneId)
        {
            Diary milestone = _uow.diaryRepo.GetDiaryById(milestoneId);

            DiaryDTO result = new DiaryDTO();

            DiaryDTO diaryDTO = new DiaryDTO();
            diaryDTO.Id = milestone.Id;
            diaryDTO.PlantId = milestone.PlantId;
            diaryDTO.PlantName = _uow.plantRepo.GetPlants().FirstOrDefault(x => x.Id == milestone.PlantId).Name;
            diaryDTO.Title = milestone.Title;
            diaryDTO.Type = milestone.Type;
            diaryDTO.Body = milestone.Body;
            diaryDTO.CreatedDay = DateTime.Now;
            diaryDTO.LastUpdate = milestone.LastUpdate;
            diaryDTO.Image = milestone.Image;
            diaryDTO.Status = milestone.Status;
            result = diaryDTO;

            return result; 
        }
        public Diary GetRawDiaryById(int milestoneId)
        {
            Diary result = _uow.diaryRepo.GetDiaryById(milestoneId);
            result.CreatedDay = DateTime.Now;

            return result;
        }
        public bool UpdateDiary(Diary milestone)
        {
            try
            {
                milestone.Status = true;
                milestone.LastUpdate =  DateTime.Now;
                _uow.diaryRepo.Update(milestone);
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
