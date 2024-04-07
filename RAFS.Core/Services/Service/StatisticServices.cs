using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class StatisticServices : IStatisticServices
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StatisticServices(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<FarmAdminDTO>> ListFarmByUserId(string userId)
        {
            List<Farm> listFarm = await _uow.farmAdminRepo.GetFarmAdminDTOsAsync(userId);
            var listFarmDTO = _mapper.Map<List<FarmAdminDTO>>(listFarm);
            return listFarmDTO;
        }

        public async Task<StatisticDTO> StatisticByFarmAndYear(string userId, int farmId, int yearStatistic)
        {
            StatisticDTO model = new StatisticDTO();
            Farm farm = await _uow.farmAdminRepo.GetByIdAsync(farmId);
            List<Farm> listFarm = await _uow.farmAdminRepo.GetFarmAdminDTOsAsync(userId);
            var listFarmDTO = _mapper.Map<List<FarmAdminDTO>>(listFarm);
            List<int> dtos = listFarmDTO.Select(x => x.Id).ToList();
            List<double> listArea = listFarmDTO.Select(x => x.Area).ToList();
            double sumArea = 0;
            if (listArea.Count >= 1)
            {
                foreach (var item in listArea)
                {
                    sumArea += item;
                }
                model.TotalArea = sumArea;
            }
            else
            {
                model.TotalArea = 0;
            }


            StatisticCashFlowDTO itemCashFlow = await _uow.cashFlowRepo.StatisticCashFlowAsync(dtos, farmId, yearStatistic);
            StatisticItemDTO itemItem = await _uow.itemRepo.StatisticItemAsync(dtos, farmId, yearStatistic);


            if (farm != null)
            {
                model.FarmId = farm.Id;
                model.FarmName = farm.Name;
                model.FarmArea = farm.Area;
                model.FarmLogo = farm.Logo;

                
            }

            if (itemCashFlow != null && itemItem != null)
            {
                model.TotalIn = itemCashFlow.TotalIn;
                model.TotalOut = itemCashFlow.TotalOut;

                model.TotalMaterial = itemItem.TotalMaterial;
                model.TotalMaterialValue = itemItem.TotalMaterialValue;

                model.CashFlowIn = itemCashFlow.CashFlowIn;
                model.CashFlowOut = itemCashFlow.CashFlowOut;
                model.CashFlowDetailIn = itemCashFlow.CashFlowDetailIn;
                model.CashFlowDetailOut = itemCashFlow.CashFlowDetailOut;

                model.ItemValue = itemItem.ItemValue;
                model.TopThreeValue = itemItem.TopThreeValue;
                model.TopThreeName = itemItem.TopThreeName;
            }
            

            return model;
        }
    }
}
