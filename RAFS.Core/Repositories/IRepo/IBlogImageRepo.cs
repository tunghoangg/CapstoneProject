using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IBlogImageRepo: IGenericRepo<BlogImage>
    {
        public void DeleteBlogImageById(int blogId);
        public List<BlogImage> GetBlogImageById(int blogId);
    }
}
