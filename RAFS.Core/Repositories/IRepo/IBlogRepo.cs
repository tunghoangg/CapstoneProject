using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.IRepo
{
    public interface IBlogRepo:IGenericRepo<Blog>
    {
        public List<Blog> GetAllBlog();
        public List<Blog> GetBlogByUserId(string userId);
        public List<String> GetAllTagNameInBlog(Blog blog);
        public List<Blog> GetBlogsByTag(string tag);
        public List<Blog> SearchBlogByTitle(string title);
        public List<String> GetAllImageURLInBlog(Blog blog);
        public AspUser GetUserById(string authorId);
        public List<Blog> GetWaitingBlog();
        public Blog GetBlogById(int id);
        public List<Blog> GetWaitingBlogByUserId(string userId);
    }
}
