using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Function
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<UserFunctionFarm>? UserFunctionFarms { get; set; }

    }
}
