using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class ItemRepo : GenericRepo<Item>, IItemRepo
    {
        public ItemRepo(RAFSContext context) : base(context)
        {
        }
        public async Task<StatisticItemDTO> StatisticItemAsync(List<int> listFarmId, int farmId, int yearStatistic)
        {
            IQueryable<Item> itemsQuery = _context.Inventories.AsQueryable();

            if (farmId == 0)
            {
                itemsQuery = itemsQuery
                    .Where(x => listFarmId.Contains(x.FarmId) && x.CreatedTime.Year == yearStatistic && x.Status == true);
            }
            else
            {
                itemsQuery = itemsQuery
                    .Where(x => x.FarmId == farmId && x.CreatedTime.Year == yearStatistic && x.Status == true);
            }

            List<Item> items = await itemsQuery.ToListAsync();


            StatisticItemDTO model = new StatisticItemDTO();

            if (items.Count > 0)
            {
                var totalItemValue = items.Sum(x => x.Value);

                // Tính TotalMaterial từ số lượng bản ghi của items
                model.TotalMaterial = items.Count;

                // Tính TotalMaterialValue từ tổng Value của items
                model.TotalMaterialValue = totalItemValue;

                // Group items by TypeId and sum their values
                var groupedItems = items.GroupBy(x => x.TypeId)
                                        .Select(g => new { TypeId = g.Key, TotalValue = g.Sum(x => x.Value) })
                                        .OrderBy(x => x.TypeId)
                                        .ToList();

                // Initialize StatisticItemDTO

                model.ItemValue = new double[6];

                // Populate ItemValue array with sums of values for each TypeId
                foreach (var groupedItem in groupedItems)
                {
                    switch (groupedItem.TypeId)
                    {
                        case 10:
                            model.ItemValue[0] = groupedItem.TotalValue;
                            break;
                        case 12:
                            model.ItemValue[1] = groupedItem.TotalValue;
                            break;
                        case 15:
                            model.ItemValue[2] = groupedItem.TotalValue;
                            break;
                        case 16:
                            model.ItemValue[3] = groupedItem.TotalValue;
                            break;
                        case 17:
                            model.ItemValue[4] = groupedItem.TotalValue;
                            break;
                        case 18:
                            model.ItemValue[5] = groupedItem.TotalValue;
                            break;
                    }
                }

                // Calculate TopThreeValue as percentage of the three highest ItemValues
                double totalSum = model.ItemValue.Sum();
                var sortedItemValues = model.ItemValue.OrderByDescending(x => x).Take(3).ToList();
                model.TopThreeValue = sortedItemValues.Select(value => Math.Round((value / totalSum) * 100, 2)).ToArray();

                var typeNameMapping = new Dictionary<int, string>
                    {
                        { 10, "Thuốc hóa học" },
                        { 12, "Phân bón hóa học" },
                        { 15, "Máy móc" },
                        { 16, "Công cụ" },
                        { 17, "Chế phẩm sinh học" },
                        { 18, "Sản phẩm nông sản" }
                    };

                model.TopThreeName = sortedItemValues.Select(value => typeNameMapping.FirstOrDefault(x => x.Key == groupedItems.FirstOrDefault(item => item.TotalValue == value)?.TypeId).Value).ToArray();

            }
            else
            {
                model.TotalMaterial = 0;

                model.TotalMaterialValue = 0;

                model.ItemValue = new double[6];
                for (int quarter = 1; quarter <= 6; quarter++)
                {
                    int currentQuarter = quarter;
                    model.ItemValue[quarter - 1] = 0;
                }
                model.TopThreeValue = new double[3];
                for (int quarter = 1; quarter <= 3; quarter++)
                {
                    int currentQuarter = quarter;
                    model.TopThreeValue[quarter - 1] = 0;
                }

                model.TopThreeName = ["Không có", "Không có", "Không có"];

            }



            return model;
        }

        public async Task<List<Item>> ListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort, int skip, int take)
        {

            List<Item> list = await _context.Inventories
                .Include(x => x.Farm)
                .Include(x => x.Unit)
                .Include(x => x.TypeItem)
                .Where(x => x.FarmId == farmId && x.Status == true)
                .OrderByDescending(x => x.LastUpdate)
                .AsQueryable().ToListAsync();

            if (typeId != 0)
            {
                var filter1 = list.Where(x => x.TypeId == typeId);
                list = filter1.ToList();
            }

            if (!string.IsNullOrEmpty(strSearch))
            {
                var filter2 = list.Where(x => x.ItemName.Trim().ToLower().Contains(strSearch.Trim().ToLower() ?? x.ItemName));
                list = filter2.ToList();
            }

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(typeSort))
            {
                var filter3 = list;
                if (columnName.Trim().ToLower().Equals("Tên".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.ItemName).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.ItemName).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Số lượng".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Quantity).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Quantity).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Tổng giá trị".Trim().ToLower()))
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

                if (columnName.Trim().ToLower().Equals("Ngày nhập kho".Trim().ToLower()))
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

        public async Task<int> CountListItemFilter(int farmId, int typeId, string? strSearch, string? columnName, string? typeSort)
        {
            List<Item> list = await _context.Inventories
                .Include(x => x.Farm)
                .Include(x => x.Unit)
                .Include(x => x.TypeItem)
                .Where(x => x.FarmId == farmId && x.Status == true)
                .OrderByDescending(x => x.LastUpdate)
                .AsQueryable().ToListAsync();

            if (typeId != 0)
            {
                var filter1 = list.Where(x => x.TypeId == typeId);
                list = filter1.ToList();
            }

            if (!string.IsNullOrEmpty(strSearch))
            {
                var filter2 = list.Where(x => x.ItemName.Trim().ToLower().Contains(strSearch.Trim().ToLower() ?? x.ItemName));
                list = filter2.ToList();
            }

            if (!string.IsNullOrEmpty(columnName) && !string.IsNullOrEmpty(typeSort))
            {
                var filter3 = list;
                if (columnName.Trim().ToLower().Equals("Tên".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.ItemName).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.ItemName).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Số lượng".Trim().ToLower()))
                {
                    if (typeSort.ToLower().Equals("asc"))
                    {
                        filter3 = list.OrderBy(x => x.Quantity).ToList();
                    }
                    else
                    {
                        filter3 = list.OrderByDescending(x => x.Quantity).ToList();
                    }
                }

                if (columnName.Trim().ToLower().Equals("Tổng giá trị".Trim().ToLower()))
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

                if (columnName.Trim().ToLower().Equals("Ngày nhập kho".Trim().ToLower()))
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
