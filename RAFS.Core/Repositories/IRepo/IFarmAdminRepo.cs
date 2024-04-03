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
    public interface IFarmAdminRepo : IGenericRepo<Farm>
    {
        Task<List<Farm>> GetFarmAdminDTOsAsync(string userId);

        Farm GetFarmByUserIdAsync(string userId);
    }
}