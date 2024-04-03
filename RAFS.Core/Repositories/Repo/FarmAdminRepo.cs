using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class FarmAdminRepo : GenericRepo<Farm>, IFarmAdminRepo
    {
        public FarmAdminRepo(RAFSContext context) : base(context)
        {
        }

        public async Task<List<Farm>> GetFarmAdminDTOsAsync(string userId)
        {
            var userFarmsQuery = _context.UserFunctionFarms.AsQueryable();

            var farmsId = await userFarmsQuery.Where(x => x.AspUserId.Equals(userId)).Select(y => y.FarmId).ToListAsync();

            var listFarm = await _context.Farms.Where(x => farmsId.Contains(x.Id) && x.Status == true).Take(5).ToListAsync();

            return listFarm;
        }

        public Farm GetFarmByUserIdAsync(string userId)
        {
            int farmsId = _context.UserFunctionFarms.Where(x => x.AspUserId.Equals(userId)).Select(y => y.FarmId).FirstOrDefault();

            Farm farm = _context.Farms.FirstOrDefault(x => farmsId.Equals(x.Id));

            return farm;
        }
    }
}