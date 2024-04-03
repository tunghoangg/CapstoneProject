using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public int FarmId { get; set; }
        public Farm Farm { get; set; }  
    }
}
