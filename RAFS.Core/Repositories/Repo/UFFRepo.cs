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
    public class UFFRepo : GenericRepo<UserFunctionFarm>, IUFFRepo
    {
        public UFFRepo(RAFSContext context) : base(context)
        {

        }

        public async Task<List<UserFunctionFarm>> GetUFFsByFarmId(string userId, int farmId)
        {
            return await _context.UserFunctionFarms.Include(x => x.AspUser).Where(x => x.FarmId == farmId && x.AspUserId != userId).ToListAsync();
        }

        public async Task<List<UserFunctionFarm>> GetTechUFFsByFarmId(string userId, int farmId)
        {
            return await _context.UserFunctionFarms.Include(x => x.AspUser).Where(x => x.FarmId == farmId && x.AspUserId == userId).ToListAsync();
        }
    }
}
