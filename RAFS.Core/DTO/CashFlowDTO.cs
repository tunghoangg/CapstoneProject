using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.DTO
{
    public class CashFlowDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Value { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TypeId { get; set; }
        public string? FarmCode { get; set; }
        public string? UserName { get; set; }
    }

    public class RequestCreateCashDTO
    {
        public double Value { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public int FarmId { get; set; }
        public string? UserId { get; set; }
    }

    public class RequestUpdateCashDTO
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
    }

    public class RequestFilterCash
    {
        public int FarmId { get; set; }
        public int TypeId { get; set; }
        //public string? SearchString { get; set;}
    }
}
