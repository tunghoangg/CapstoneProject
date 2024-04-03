using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IFarmService
    {
        List<FarmDTO> GetHomepageFarmList(string? search, string? province, string? district, string? ward, string? sort, string? area);
        HomepageFarmDetailsDTO GetFarmDetailsInHomepage(int farmId);
        List<FarmDTO> GetNewsFarm();

    }
}
