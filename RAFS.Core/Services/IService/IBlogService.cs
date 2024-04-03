using RAFS.Core.DTO;
using RAFS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Services.IService
{
    public interface IBlogService
    {
        public List<BlogDTO> GetAllBlog();
        public List<BlogDTO> GetBlogsByTag(string tag);
        public List<BlogDTO> SearchBlogByTitle(string title);
        public void CreateBlog(CreateBlogDTO createBlogDTO);
        public bool DeleteBlog(int blogId);
        public List<BlogDTO> GetWaitingBlog();
        public List<String> GetAllTagName();
        public bool UpdateBlog(int id,string body);
        public BlogDTO GetBlogDetail(int id);
        public void AprroveBlog(int id);
        public List<Tag> GetAllTag();
        public bool DeleteTag(int id);
        public Tag CreateTag(string tagName);
        public List<BlogDTO> GetBlogByUserId(string userId);
        public List<BlogDTO> GetWaitingBlogByUserId(string userId);
    }
}
