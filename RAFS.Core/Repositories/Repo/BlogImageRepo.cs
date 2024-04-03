using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class BlogImageRepo : GenericRepo<BlogImage>, IBlogImageRepo
    {
        public BlogImageRepo(RAFSContext context) : base(context)
        {

        }
        public List<BlogImage> GetBlogImageById(int blogId)
        {
            List<BlogImage> blogimgList = new List<BlogImage>();
            blogimgList = _context.BlogImages.Where(bt => bt.BlogId == blogId).ToList();
            return blogimgList;
        }
        public void DeleteBlogImageById(int blogId)
        {
            List<BlogImage> blogimgList = GetBlogImageById(blogId);
            if (blogimgList.Any())
            {
                _context.BlogImages.RemoveRange(blogimgList);
                _context.SaveChanges();
            }
        }
    }
}
