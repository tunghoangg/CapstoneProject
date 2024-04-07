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
    public interface IMilestonRepo : IGenericRepo<Milestone>
    {
        public List<Milestone> GetMilestones(int farmid);
        public List<PlantDeleteDTO> GetPlantOfMilestone(int milestoneid);

        public List<MilestoneDTOVer2> GetFarmMilestones(int farmid);
        public List<MilestoneDTOVer2> GetMilestones(int farmid, int page);
        public Milestone GetMilestonesById(int milestoneid);
        public bool CreateMilestone(Milestone milestoneinfor);
        public bool UpdateMilestone(Milestone milestoneinfor);
        public bool DeleteMilestone(int milestoneid);
    }
}
