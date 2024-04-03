using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IItemRepo : IGenericRepo<Item>
    {
        public Task<StatisticItemDTO> StatisticItemAsync(List<int> listFarmId, int farmId, int yearStatistic);

        Task<List<Item>> ListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take);
        Task<int> CountListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort);

    }
}
