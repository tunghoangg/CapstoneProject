using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface ITagRepo : IGenericRepo<Tag>
    {
        public bool tagCheck(string tagName);
        public Tag getTagByName(string tagName);
        public Tag getLastTag();
    }
}
