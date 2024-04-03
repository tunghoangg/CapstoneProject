using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public AspUser Author { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Status { get; set; }

        public virtual IList<BlogTag>? BlogTags { get; set; }
        public virtual IList<BlogImage>? BlogImages { get; set; }
    }
}
