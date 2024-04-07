using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class CashFlowRepo : GenericRepo<CashFlow>, ICashFlowRepo
    {
        public CashFlowRepo(RAFSContext context) : base(context)
        {
        }

        public async Task<StatisticCashFlowDTO> StatisticCashFlowAsync(List<int> listFarmId, int farmId, int yearStatistic)
        {

            IQueryable<CashFlow> cashFlowsQuery = _context.CashFlows.AsQueryable();

            if (farmId == 0)
            {
                cashFlowsQuery = cashFlowsQuery
                    .Where(x => listFarmId.Contains(x.FarmId) && x.CreatedTime.Year == yearStatistic && x.Status == true);
            }
            else
            {
                cashFlowsQuery = cashFlowsQuery
                    .Where(x => x.FarmId == farmId && x.CreatedTime.Year == yearStatistic && x.Status == true);
            }

            List<CashFlow> cashFlows = await cashFlowsQuery.ToListAsync();

            StatisticCashFlowDTO model = new StatisticCashFlowDTO();

            if (cashFlows.Count > 0)
            {
                double totalInDetail = cashFlows.Where(cf => cf.TypeId >= 9 && cf.TypeId <= 13).Sum(cf => cf.Value);
                double totalOutDetail = cashFlows.Where(cf => cf.TypeId >= 1 && cf.TypeId <= 8).Sum(cf => cf.Value);

                // Tính TotalIn từ Value của cashFlows có TypeId là: 9, 10, 11, 12, 13
                model.TotalIn = totalInDetail;

                // Tính TotalOut từ Value của cashFlows có TypeId là: 1, 2, 3, 4, 5, 6, 7, 8
                model.TotalOut = totalOutDetail;

                // Tính CashFlowIn từ Value của cashFlows có TypeId là: 9, 10, 11, 12, 13 và của 4 quý trong năm yearStatistic
                model.CashFlowIn = new double[4];
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    int currentQuarter = quarter; // Lưu biến để tránh lỗi Closure
                    model.CashFlowIn[quarter - 1] = cashFlows
                        .Where(cf => cf.TypeId >= 9 && cf.TypeId <= 13 && GetQuarter(cf.CreatedTime) == currentQuarter)
                        .Sum(cf => cf.Value);
                }

                // Tính CashFlowOut từ Value của cashFlows có TypeId là: 1, 2, 3, 4, 5, 6, 7, 8 và của 4 quý trong năm yearStatistic
                model.CashFlowOut = new double[4];
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    int currentQuarter = quarter; // Lưu biến để tránh lỗi Closure
                    model.CashFlowOut[quarter - 1] = cashFlows
                        .Where(cf => cf.TypeId >= 1 && cf.TypeId <= 8 && GetQuarter(cf.CreatedTime) == currentQuarter)
                        .Sum(cf => cf.Value);
                }

                // Tính chi tiết tổng thu của các loại 9-13 => %

                model.CashFlowDetailIn = new double[5];
                for (int typeId = 9; typeId <= 13; typeId++)
                {
                    double tmpIn = cashFlows
                        .Where(cf => cf.TypeId == typeId)
                        .Sum(cf => cf.Value) / totalInDetail;
                    model.CashFlowDetailIn[typeId - 9] = Math.Round(tmpIn * 100, 2);
                }

                // Tính chi tiết tổng chi của các loại 1-8 => %
                model.CashFlowDetailOut = new double[8];
                for (int typeId = 1; typeId <= 8; typeId++)
                {
                    double tmpOut = cashFlows
                        .Where(cf => cf.TypeId == typeId)
                        .Sum(cf => cf.Value) / totalOutDetail;
                    model.CashFlowDetailOut[typeId - 1] = Math.Round(tmpOut * 100, 2);
                }
            }
            else
            {
                model.TotalIn = 0;
                model.TotalOut = 0;

                model.CashFlowIn = new double[4];
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    int currentQuarter = quarter; 
                    model.CashFlowIn[quarter - 1] = 0;
                }

                model.CashFlowOut = new double[4];
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    int currentQuarter = quarter; 
                    model.CashFlowOut[quarter - 1] = 0;
                }

                model.CashFlowDetailIn = new double[5];
                for (int typeId = 9; typeId <= 13; typeId++)
                {
                    model.CashFlowDetailIn[typeId - 9] = 0;
                }

                model.CashFlowDetailOut = new double[8];
                for (int typeId = 1; typeId <= 8; typeId++)
                {
                    model.CashFlowDetailOut[typeId - 1] = 0;
                }

            }


            return model;
        }

        // Hàm để lấy quý từ ngày
        private int GetQuarter(DateTime date)
        {
            return (date.Month - 1) / 3 + 1;
        }

        public async Task<List<CashFlow>> ListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take)
        {
            List<CashFlow> list = await _context.CashFlows
                .Include(x => x.Farm)
                .Include(x => x.User)
                .Include(x => x.TypeCashFlow)
                .Where(x => x.FarmId == farmId && x.Status == true)
                .OrderByDescending(x => x.CreatedTime)
                .AsQueryable().ToListAsync();

            if (typeId != 0)
            {
                var filter1 = list.Where(x => x.TypeId == typeId);
                list = filter1.ToList();
            }

            if (!string.IsNullOrEmpty(strSearch))
            {
                var filter2 = list.Where(x => x.Code.ToString().Trim().ToLower().Contains(strSearch.ToString().Trim().ToLower() ?? x.Code.ToString()));
                list = filter2.ToList();
            }

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(typeSort))
            {
                var filter3 = list;
                if (columnName.Trim().ToLower().Equals("Mã".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Code).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Code).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Giá trị".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Value).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Value).ToList();
                    }
                }
                
                //if (columnName.Trim().ToLower().Equals("Loại".Trim().ToLower()))
                //{
                //    if (typeSort.ToLower().Equals("asc"))
                //    {
                //        filter3 = list.OrderBy(x => x.Value).ToList();
                //    }
                //    else
                //    {
                //        filter3 = list.OrderByDescending(x => x.Value).ToList();
                //    }
                //}

                if (columnName.Trim().ToLower().Equals("Ngày tạo".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.CreatedTime).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.CreatedTime).ToList();
                    }
                }

                list = filter3.ToList();
            }

            return list.Skip(skip).Take(take).ToList();
        }

        public async Task<int> CountListCashFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort)
        {
            List<CashFlow> list = await _context.CashFlows
                .Include(x => x.Farm)
                .Include(x => x.TypeCashFlow)
                .Where(x => x.FarmId == farmId && x.Status == true)
                .OrderByDescending(x => x.CreatedTime)
                .AsQueryable().ToListAsync();

            if (typeId != 0)
            {
                var filter1 = list.Where(x => x.TypeId == typeId);
                list = filter1.ToList();
            }

            if (!string.IsNullOrEmpty(strSearch))
            {
                var filter2 = list.Where(x => x.Code.ToString().Trim().ToLower().Contains(strSearch.ToString().Trim().ToLower() ?? x.Code.ToString()));
                list = filter2.ToList();
            }

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(typeSort))
            {
                var filter3 = list;
                if (columnName.Trim().ToLower().Equals("Mã".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Code).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Code).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Giá trị".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Value).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Value).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Ngày tạo".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.CreatedTime).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.CreatedTime).ToList();
                    }
                }

                list = filter3.ToList();
            }

            return list.Count();
        }

        
    }
}
