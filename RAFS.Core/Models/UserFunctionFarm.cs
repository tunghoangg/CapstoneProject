using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class UserFunctionFarm
    {
        public string AspUserId { get; set; }
        public AspUser AspUser { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
        public int FunctionId { get; set; }
        public Function Function { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
