using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        public double Value { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public int TypeId { get; set; }
        public string FarmCode { get; set; }
    }


    public class RequestCreateItemDTO
    {
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        public double Value { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int FarmId { get; set; }
    }

    public class RequestUpdateItemDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        public double Value { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
    }

    public class RequestFilterItem
    {
        public int FarmId { get; set; }
        public int TypeId { get; set; }
        //public string? SearchString { get; set;}
    }
}
