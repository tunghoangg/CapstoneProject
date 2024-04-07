using AutoMapper;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IFarmAdminService
    {
        Task<List<FarmAdminDTO>> GetFarmAdminDTOsAsync(string userId);
        Task CreateFarmAdminDTOAsync(AddFarmAdminDTO model);
        Task UpdateFarmAdminDTOAsync(UpdateFarmAdminDTO model);
        Task<FarmAdminDTO> GetFarmAdminDTOByFarmId(int farmId);
        Task<string> GetLogoLinkByFarmId(int farmId);
        Task UpdateLogoLink(int farmId , string logoLink);
        Task<List<UserFarmAdminDTO>> GetUFFsByFarmId(string userId, int farmId);

        Task UpdateUserFunction(UpdateUFFDTO model);
        Task<string> ChangeStatusTechnician(string userId);
        Task SoftDeleteFarm(int farmId);

        Task<Farm> GetFarmByUserId(string userId);

        Task<List<ImagesFarmDTO>> GetImagesFarmDTO(int farmId);
        Task UpdateImagesFarm(int imageId, string imageURL);

        Task<string> GetImageURLByImageId(int imageId);
    }
}