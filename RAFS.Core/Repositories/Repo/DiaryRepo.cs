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

namespace RAFS.Core.Repositories.Repo
{
    public class DiaryRepo : GenericRepo<Diary>, IDiaryRepo
    {

        public DiaryRepo(RAFSContext context) : base(context)
        {
        }

        public bool CreateDiary(Diary milestoneinfor)
        {
            try
            {

                milestoneinfor.CreatedDay = DateTime.Now;
                milestoneinfor.LastUpdate = DateTime.Now;
                milestoneinfor.Status = true;
                _context.Diaries.Add(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDiary(int milestoneid)
        {
            try {

                
                Diary milestones = _context.Diaries.FirstOrDefault(x=>x.Id ==milestoneid);
               _context.Diaries.Remove(milestones);
              
                return true;
            }
            catch {
                return false;
            }
        }

        public List<DiaryDTO> GetDiaries(int farmid)
        {
           
            List<Plant> plants = new List<Plant>();
            plants = _context.Farms
            .Where(farm => farm.Id == farmid && farm.Status == true)
            .SelectMany(farm => farm.Milestones.SelectMany(milestone => milestone.Plants).Where(x=>x.Status==true))
            .ToList();

            List<DiaryDTO> result = new List<DiaryDTO>();


            foreach (var plant in plants)
            {
                List<Diary> diaries = _context.Diaries.Where(x => x.PlantId == plant.Id && x.Status ==true).ToList();
                if (diaries.Count > 0)
                {
                    foreach (var diary in diaries)
                    {
                        DiaryDTO diaryDTO = new DiaryDTO();
                        diaryDTO.Id = diary.Id;
                        diaryDTO.PlantId = diary.PlantId;
                        diaryDTO.CreatedDay = DateTime.Now;
                        diaryDTO.PlantName = plants.FirstOrDefault(x => x.Id == diary.PlantId).Name;
                        diaryDTO.Title = diary.Title;
                        diaryDTO.Type = diary.Type;
                        diaryDTO.Body = diary.Body;
                        diaryDTO.LastUpdate = diary.LastUpdate;
                        diaryDTO.Image = diary.Image;
                        diaryDTO.Status = diary.Status;
                        result.Add(diaryDTO);
                    }
                }

            }
            
            


            return result;
        }
        public List<DiaryDTO> GetPlantDiariesByType(int plantid, int type)
        {
            
            
            List<DiaryDTO> result = new List<DiaryDTO>();

            List<Plant> plants = _context.Plants.ToList();
            List<Diary> diaries = _context.Diaries.Where(x => x.PlantId == plantid && x.Type==type && x.Status == true).ToList();
                if (diaries.Count > 0)
                {
                    foreach (var diary in diaries)
                    {
                        DiaryDTO diaryDTO = new DiaryDTO();
                        diaryDTO.Id = diary.Id;
                        diaryDTO.PlantId = diary.PlantId;
                        diaryDTO.CreatedDay = DateTime.Now;
                        diaryDTO.PlantName = plants.FirstOrDefault(x=> x.Id == plantid).Name;
                        diaryDTO.Title = diary.Title;
                        diaryDTO.Type = diary.Type;
                        diaryDTO.Body = diary.Body;
                        diaryDTO.LastUpdate = diary.LastUpdate;
                        diaryDTO.Image = diary.Image;
                        diaryDTO.Status = diary.Status;
                        result.Add(diaryDTO);
                    }
                }

            
         
            return result;
        }

        public Diary GetDiaryById(int milestoneid)
        {
            Diary diary2 = _context.Diaries.FirstOrDefault(x => x.Id == milestoneid);
            return diary2;
        }
        public List<DiaryDTO> GetPlantDiaries(int plantid)
        {

            List<DiaryDTO> result = new List<DiaryDTO>();


            List<Diary> diaries = _context.Diaries.Where(x => x.PlantId == plantid && x.Status == true).ToList();
            List<Plant> plants = _context.Plants.ToList();
            if (diaries.Count > 0)
            {
                foreach (var diary in diaries)
                {
                    DiaryDTO diaryDTO = new DiaryDTO();
                    diaryDTO.Id = diary.Id;
                    diaryDTO.PlantId = diary.PlantId;
                    diaryDTO.CreatedDay = DateTime.Now;
                    diaryDTO.PlantName = plants.FirstOrDefault(x => x.Id == plantid).Name;
                    diaryDTO.Title = diary.Title;
                    diaryDTO.Type = diary.Type;
                    diaryDTO.Body = diary.Body;
                    diaryDTO.LastUpdate = diary.LastUpdate;
                    diaryDTO.Image = diary.Image;
                    diaryDTO.Status = diary.Status;
                    result.Add(diaryDTO);
                }


            }

            return result;
        }

        public bool UpdateDiary(Diary milestoneinfor)
        {
            try
            {

                milestoneinfor.Status = true;
                milestoneinfor.LastUpdate = DateTime.Now;
                _context.Diaries.Update(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        

    }
}
