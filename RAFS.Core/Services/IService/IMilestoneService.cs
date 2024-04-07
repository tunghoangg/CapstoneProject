using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IMilestoneService
    {
        public List<Milestone> GetAllMilestone(int farmid);
        public List<PlantDeleteDTO> GetAllPlantofMilestone(int milestoneid);
        List<MilestoneDTOVer2> GetAllFarmFilteredMilestones(int farmid);
        List<MilestoneDTOVer2> GetAllFarmMilestones(int farmid, int page);
        public Milestone GetMilestoneById(int milestoneId);
        public bool CreateMilestone(Milestone milestone);
        public bool UpdateMilestone(Milestone milestone);
        public bool DeleteMilestone(int milestoneId);
       
    }
}
