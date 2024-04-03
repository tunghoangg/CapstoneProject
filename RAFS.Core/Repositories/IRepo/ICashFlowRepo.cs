using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface ICashFlowRepo : IGenericRepo<CashFlow>
    {

        public Task<StatisticCashFlowDTO> StatisticCashFlowAsync(List<int> listFarmId, int farmId, int yearStatistic);

        Task<List<CashFlow>> ListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take);
        Task<int> CountListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort);

    }
}
