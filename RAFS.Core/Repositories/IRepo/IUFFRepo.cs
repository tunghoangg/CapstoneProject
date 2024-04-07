using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    //UFF => UserFunctionFarm
    public interface IUFFRepo : IGenericRepo<UserFunctionFarm>
    {

        Task<List<UserFunctionFarm>> GetUFFsByFarmId(string userId, int farmId);

        Task<List<UserFunctionFarm>> GetTechUFFsByFarmId(string userId, int farmId);
    }
}
