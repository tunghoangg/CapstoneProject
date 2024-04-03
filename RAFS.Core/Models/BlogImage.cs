using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class BlogImage
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
