using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class CashFlow
    {
        public int Id { get; set; }
        //bỏ
        public Guid Code { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedTime { get; set; }
        public int TypeId { get; set; }
        public TypeCashFlow TypeCashFlow { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public string UserId { get; set; }
        public AspUser User { get; set; }
    }
}
