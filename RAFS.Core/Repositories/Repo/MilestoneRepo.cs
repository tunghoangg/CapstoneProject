using Azure;
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
    public class MilestoneRepo : GenericRepo<Milestone>, IMilestonRepo
    {

        public MilestoneRepo(RAFSContext context) : base(context)
        {
        }

        public bool CreateMilestone(Milestone milestoneinfor)
        {
            try
            {

               
                milestoneinfor.LastUpdate = DateTime.Now;
                milestoneinfor.Status = true;
                _context.Milestones.Add(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMilestone(int milestoneid)
        {
            try {

                Milestone milestones = GetMilestonesById(milestoneid);
                milestones.Status = false;
               _context.Milestones.Update(milestones);
              
                return true;
            }
            catch {
                return false;
            }
        }

        public List<MilestoneDTOVer2> GetFarmMilestones(int farmid)
        {
            List<Milestone> milestones = _context.Milestones.Where(x => x.FarmId == farmid
                                          && x.Status == true).ToList();
            List<Plant> plants = _context.Plants.Where(x =>
                                         x.Status == true).ToList();
            List<MilestoneDTOVer2> milestoneDTOs = new List<MilestoneDTOVer2>();

          
                milestoneDTOs = milestones
                     .Select(c => new MilestoneDTOVer2
                     {
                         Id = c.Id,
                         NumberOfPlants = plants.Where(x => x.MilestoneId == c.Id).ToList().Count,
                         Name = c.Name,
                         Description = c.Description,
                         LastUpdate = c.LastUpdate,
                         totalpage = 0,
                         Status = c.Status
                     })
                     .ToList();
            return milestoneDTOs;
            }

        public List<Milestone> GetMilestones(int farmid)
        {
            List<Milestone> milestones = new List<Milestone>();
            milestones = _context.Milestones.Where(x=>x.Status==true && x.FarmId == farmid).ToList();


            return milestones;
        }

        public List<MilestoneDTOVer2> GetMilestones(int farmid, int page)
        {
            List<Milestone> milestones = _context.Milestones.Where(x => x.FarmId == farmid
                                         && x.Status == true).ToList();
            List<Plant> plants = _context.Plants.Where(x => 
                                         x.Status == true).ToList();
            int totalpage = milestones.Count % 5;
           
            if (milestones.Count<=5)
            {
                totalpage = 1;
            }
            else
            {
                totalpage = milestones.Count / 5;
                totalpage++;
            }
            int start = 0;
            int end = 0;
            if(page == 1)
            {
                 end = 5;
            }
            else
            {
                start = ((page-1)*5);
                end = page*5;
            }

            List<MilestoneDTOVer2> milestoneDTOs = new List<MilestoneDTOVer2>() ;

            if (milestones.Count <= 5)
            {
                 milestoneDTOs = milestones
                      .Select(c => new MilestoneDTOVer2
                      {
                          Id = c.Id,
                          NumberOfPlants = plants.Where(x=>x.MilestoneId == c.Id).ToList().Count,
                          Name = c.Name,
                          Description = c.Description,
                          LastUpdate = c.LastUpdate,
                          totalpage = totalpage,
                          Status = c.Status
                      })
                      .ToList();
            }
            else
            {
                if (milestones.Count % 5 ==0)
                {
                    milestoneDTOs = milestones
                        .Select(c => new MilestoneDTOVer2
                        {
                            Id = c.Id,
                            NumberOfPlants = plants.Where(x => x.MilestoneId == c.Id).ToList().Count,
                            Name = c.Name,
                            Description = c.Description,
                            LastUpdate = c.LastUpdate,
                            totalpage = totalpage,
                            Status = c.Status
                        }).Skip(start) // Skip the first 4 records (indexes start from 0)
                          .Take(end)
                        .ToList();
                }
                else{
                    milestoneDTOs = milestones
                            .Select(c => new MilestoneDTOVer2
                            {
                                Id = c.Id,
                                NumberOfPlants = plants.Where(x => x.MilestoneId == c.Id).ToList().Count,
                                Name = c.Name,
                                Description = c.Description,
                                LastUpdate = c.LastUpdate,
                                totalpage = totalpage,
                                Status = c.Status
                            }).Skip(start)
                             .Take(end)
                            .ToList();

                }
               
            }
           

            return milestoneDTOs;
        }

        public Milestone GetMilestonesById(int milestoneid)
        {
           Milestone milestones = new Milestone();
            milestones = _context.Milestones.FirstOrDefault(x=> x.Id == milestoneid);


            return milestones;
        }

        public List<PlantDeleteDTO> GetPlantOfMilestone(int milestoneid)
        {
            List<Plant> plants = _context.Plants.Where(x => x.MilestoneId == milestoneid && x.Status == true).ToList();
            List<PlantDeleteDTO> reslut = new List<PlantDeleteDTO>();
            foreach(var plant in plants)
            {
                PlantDeleteDTO plantDeleteDTO = new PlantDeleteDTO();
                plantDeleteDTO.Name = plant.Name;
                plantDeleteDTO.PlantingMethod = plant.PlantingMethod;
                plantDeleteDTO.Image = plant.Image;
                reslut.Add(plantDeleteDTO);
            }


           return reslut;
        }

        public bool UpdateMilestone(Milestone milestoneinfor)
        {
            try
            {
                milestoneinfor.LastUpdate = DateTime.Now;
                milestoneinfor.Status = true;

                _context.Milestones.Update(milestoneinfor);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
