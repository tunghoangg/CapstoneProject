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
    public class MilestoneService : IMilestoneService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MilestoneService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public bool CreateMilestone(Milestone milestone)
        {


            try
            {
                _uow.milestonRepo.CreateMilestone(milestone);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteMilestone(int milestoneId)
        {
            try
            {
                _uow.milestonRepo.DeleteMilestone(milestoneId);
                _uow.Save();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<MilestoneDTOVer2> GetAllFarmMilestones(int farmid, int page)
        {
            List<MilestoneDTOVer2> milestones = _uow.milestonRepo.GetMilestones(farmid,page);
            return milestones;
        }

        public List<Milestone> GetAllMilestone(int farmid)
        {
            List<Milestone> milestones = _uow.milestonRepo.GetMilestones(farmid);

            return milestones; 
        }

        public List<PlantDeleteDTO> GetAllPlantofMilestone(int milestoneid)
        {
            List<PlantDeleteDTO> plantDeleteDTOs = _uow.milestonRepo.GetPlantOfMilestone(milestoneid);
            int count   = plantDeleteDTOs.Count;
           return plantDeleteDTOs;
        }

        public Milestone GetMilestoneById(int milestoneId)
        {
            Milestone milestones = _uow.milestonRepo.GetById(milestoneId);
            

            return milestones; 
        }

        public bool UpdateMilestone(Milestone milestone)
        {
            try
            {
                milestone.Status = true;
                _uow.milestonRepo.UpdateMilestone(milestone);
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
