using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class TypeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Item>? Items { get; set; }

    }
}
