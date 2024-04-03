using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IMaterialsService
    {
        Task<List<ItemDTO>> ListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take);
        Task<int> CountListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort);

        Task CreateItemAsync(RequestCreateItemDTO model);
        Task UpdateItemAsync(RequestUpdateItemDTO model);
        Task DeleteItemAsync(int itemId);
    }
}
