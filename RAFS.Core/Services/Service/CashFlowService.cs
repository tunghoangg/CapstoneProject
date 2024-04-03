using AutoMapper;
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
    public class CashFlowService : ICashFlowService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CashFlowService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<CashFlowDTO>> ListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take)
        {
            var list = await _uow.cashFlowRepo.ListCashFilter(farmId, typeId, strSearch, columnName, typeSort, skip, take);

            List<CashFlowDTO> listDTO = new List<CashFlowDTO>();

            foreach (var item in list)
            {
                CashFlowDTO itemDTO = new CashFlowDTO();
                itemDTO.Id = item.Id;
                itemDTO.Code = item.Code.ToString();
                itemDTO.Value = item.Value;
                itemDTO.CreatedTime = item.CreatedTime;
                itemDTO.Description = item.Description;
                itemDTO.TypeId = item.TypeId;
                itemDTO.FarmCode = item.Farm.Code;
                itemDTO.UserName = item.User.UserName;
                listDTO.Add(itemDTO);
            };

            return listDTO;
        }

        public async Task<int> CountListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort)
        {
            int count = await _uow.cashFlowRepo.CountListCashFilter(farmId, typeId, strSearch, columnName, typeSort);

            return count;
        }

        public async Task CreateCashAsync(RequestCreateCashDTO model)
        {
            CashFlow item = new CashFlow();
            item.Code = Guid.NewGuid();
            item.Value = model.Value;
            item.Description = model.Description; 
            item.TypeId = model.TypeId;
            item.FarmId = model.FarmId;
            item.UserId = model.UserId;
            item.Status = true;
            item.CreatedTime = DateTime.Now;


            await _uow.cashFlowRepo.AddAsync(item);
            await _uow.SaveAsync();
        }

        public async Task DeleteCashAsync(int cashId)
        {
            var item = await _uow.cashFlowRepo.GetByIdAsync(cashId);

            if (item != null)
            {
                item.Status = false;
                await _uow.cashFlowRepo.UpdateAsync(item);
                await _uow.SaveAsync();
            }
        }

        public async Task UpdateCashAsync(RequestUpdateCashDTO model)
        {
            var item = await _uow.cashFlowRepo.GetByIdAsync(model.Id);

            if (item != null)
            {
                item.Value = model.Value;
                item.Description = model.Description;
                item.TypeId = model.TypeId;

                await _uow.cashFlowRepo.UpdateAsync(item);
                await _uow.SaveAsync();
            }
        }
    }
}
