using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class TypeCashFlow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<CashFlow>? CashFlows { get; set; }

    }
}
