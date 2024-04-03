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
    public class BlogTagRepo: GenericRepo<BlogTag>,IBlogTagRepo
    {
        public BlogTagRepo(RAFSContext context) : base(context)
        {

        }
        public List<BlogTag> GetBlogTagsById (int blogId)
        {
            List<BlogTag> blogtagList = new List<BlogTag>();
            blogtagList = _context.BlogTags.Where(bt =>  bt.BlogId == blogId).ToList(); 
            return blogtagList;
        }
        public void DeleteBlogTagById(int blogId)
        {
            List<BlogTag> blogtagList = GetBlogTagsById(blogId);
            if (blogtagList.Any())
            {
                _context.BlogTags.RemoveRange(blogtagList);
                _context.SaveChanges();
            }
        }
    }
}
