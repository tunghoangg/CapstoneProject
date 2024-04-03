using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IStatisticServices
    {
        public Task<List<FarmAdminDTO>> ListFarmByUserId(string userId);

        public Task<StatisticDTO> StatisticByFarmAndYear(string userId, int farmId, int yearStatistic);

    }
}
