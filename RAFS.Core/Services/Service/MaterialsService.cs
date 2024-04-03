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
    public class MaterialsService : IMaterialsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MaterialsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<List<ItemDTO>> ListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take)
        {
            var list = await _uow.itemRepo.ListItemFilter(farmId, typeId, strSearch, columnName, typeSort, skip, take);

            List<ItemDTO> listDTO = new List<ItemDTO>();

            foreach (var item in list) { 
                ItemDTO itemDTO = new ItemDTO();
                itemDTO.Id = item.Id;
                itemDTO.ItemName = item.ItemName;
                itemDTO.Quantity = item.Quantity;
                itemDTO.UnitId = item.UnitId;
                itemDTO.Value = item.Value;
                itemDTO.CreatedTime = item.CreatedTime;
                itemDTO.Description = item.Description;
                itemDTO.LastUpdate = item.LastUpdate;
                itemDTO.TypeId = item.TypeId;
                itemDTO.FarmCode = item.Farm.Code;
                listDTO.Add(itemDTO);
            };

            return listDTO;
        }

        public async Task<int> CountListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort)
        {
            int count = await _uow.itemRepo.CountListItemFilter(farmId, typeId, strSearch, columnName, typeSort);

            return count;
        }

        public async Task CreateItemAsync(RequestCreateItemDTO model)
        {
            Item item = new Item();
            item.ItemName = model.ItemName;
            item.Quantity = model.Quantity; 
            item.UnitId = model.UnitId;
            item.Value = model.Value;
            item.CreatedTime = model.CreatedTime;
            item.Description = model.Description;
            item.TypeId = model.TypeId;
            item.FarmId = model.FarmId;
            item.LastUpdate = DateTime.UtcNow;
            item.Status = true;

            await _uow.itemRepo.AddAsync(item);
            await _uow.SaveAsync();
        }

        public async Task UpdateItemAsync(RequestUpdateItemDTO model)
        {
            var item = await _uow.itemRepo.GetByIdAsync(model.Id);

            if (item != null)
            {
                item.ItemName = model.ItemName;
                item.Quantity = model.Quantity;
                item.UnitId = model.UnitId;
                item.Value = model.Value;
                item.CreatedTime = model.CreatedTime;
                item.Description = model.Description;
                item.TypeId = model.TypeId;
                item.LastUpdate = DateTime.UtcNow;

                await _uow.itemRepo.UpdateAsync(item);
                await _uow.SaveAsync();
            }
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var item = await _uow.itemRepo.GetByIdAsync(itemId);

            if (item != null)
            {
                item.Status = false;
                await _uow.itemRepo.UpdateAsync(item);
                await _uow.SaveAsync();
            }
        }

    }
}
