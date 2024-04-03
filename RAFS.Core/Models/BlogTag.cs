using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class BlogTag
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
