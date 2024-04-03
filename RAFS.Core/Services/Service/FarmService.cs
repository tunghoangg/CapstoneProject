using AutoMapper;
using Azure;
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
    public class FarmService : IFarmService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FarmService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public HomepageFarmDetailsDTO GetFarmDetailsInHomepage(int farmId)
        {
            var result = _uow.farmRepo.GetFarmDetailsInHomepage(farmId);
            var details = new HomepageFarmDetailsDTO
            {
                Id = result.Id,
                Code = result.Code,
                Address = result.Address,
                Phone = result.Phone,
                Name = result.Name,
                EstablishedDate = result.EstablishedDate,
                PageLink = result.PageLink,
                Area = result.Area,
                Description = result.Description,
                ImageURL = _uow.farmRepo.GetFarmImg(farmId),
                Plants = _uow.farmRepo.GetFarmPlants(farmId)
        };
            return details;
        }

        public List<FarmDTO> GetHomepageFarmList(string? search, string? province, string? district, string? ward, string? sort, string? area)
        {
            var listFarm = _uow.farmRepo.GetFarmsInHomepage(search, province, district, ward, sort, area);

            var fDTO = _mapper.Map<List<FarmDTO>>(listFarm);

            return fDTO;
        }

        public List<FarmDTO> GetNewsFarm()
        {
            var listFarm = _uow.farmRepo.GetNewestFarms();
            var fDTO = _mapper.Map<List<FarmDTO>>(listFarm);

            return fDTO;
        }
    }
}
