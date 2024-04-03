using RAFS.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface ICashFlowService
    {
        Task<List<CashFlowDTO>> ListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take);
        Task<int> CountListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort);
        Task CreateCashAsync(RequestCreateCashDTO model);
        Task UpdateCashAsync(RequestUpdateCashDTO model);
        Task DeleteCashAsync(int cashId);
    }
}
