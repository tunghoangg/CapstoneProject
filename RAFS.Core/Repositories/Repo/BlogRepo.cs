using Microsoft.EntityFrameworkCore;
using RAFS.Core.Context;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAFS.Core.Repositories.Repo
{
    public class BlogRepo : GenericRepo<Blog>, IBlogRepo
    {
        public BlogRepo(RAFSContext context) : base(context)
        {

        }

        public Blog GetBlogById(int id)
        {
            var blog = _context.Blogs.Include(blog => blog.Author)
            .Include(blog => blog.BlogTags)
            .Include(blog => blog.BlogImages).FirstOrDefault(x=>x.Id==id);
            return blog;
        }
        public List<Blog> GetAllBlog()
        {
            var listBlog = _context.Blogs
            .Include(blog => blog.Author)
            .Include(blog => blog.BlogTags)
            .Include(blog => blog.BlogImages)
            .Where(blog=>blog.Status==true)
            .ToList();
            listBlog = listBlog.OrderByDescending(f => f.LastUpdated).ToList();
            return listBlog;
        }
        public List<Blog> GetWaitingBlog()
        {
            var listBlog = _context.Blogs
            .Include(blog => blog.Author)
            .Include(blog => blog.BlogTags)
            .Include(blog => blog.BlogImages)
            .Where(blog => blog.Status == false)
            .ToList();
            return listBlog;
        }
        public List<Blog> GetBlogsByTag(string tag)
        {
            var listBlogs = new List<Blog>();
            try
            {

                listBlogs = _context.Blogs.Include(blog => blog.Author)
                 .Include(blog => blog.BlogTags)
                 .Include(blog => blog.BlogImages)
               .Where(b => b.BlogTags.Any(bt => bt.Tag.Name == tag) && b.Status==true)
               .ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return listBlogs;
        }

        public List<Blog> SearchBlogByTitle(string title)
        {
            var listBlogs = _context.Blogs.Include(blog => blog.Author)
                  .Include(blog => blog.BlogTags)
                  .Include(blog => blog.BlogImages).Where(b => b.Title.ToLower().Contains(title.ToLower()) && b.Status == true).ToList();
            return listBlogs;
        }
        public List<String> GetAllTagNameInBlog(Blog blog)
        {
            return _context.BlogTags
                                 .Where(bt => bt.BlogId == blog.Id)
                                 .Select(bt => bt.Tag.Name)
                                 .ToList();
        }

        public List<String> GetAllImageURLInBlog(Blog blog)
        {
           List<String> url = _context.BlogImages
                             .Where(bt => bt.BlogId == blog.Id)
                             .Select(bt => bt.URL)
                             .ToList();
            return url;
        }
        public AspUser GetUserById(string authorId)
        {   
            AspUser author = new AspUser();
            author = _context.AspUsers.FirstOrDefault(a => a.Id == authorId);
            return author;
        }

        public List<Blog> GetBlogByUserId(string userId)
        {
            var listBlog = _context.Blogs
            .Include(blog => blog.Author)
            .Include(blog => blog.BlogTags)
            .Include(blog => blog.BlogImages)
            .Where(blog => blog.Status == true && blog.AuthorId==userId)
            .ToList();
            return listBlog;
        }

        public List<Blog> GetWaitingBlogByUserId(string userId)
        {
            var listBlog = _context.Blogs
           .Include(blog => blog.Author)
           .Include(blog => blog.BlogTags)
           .Include(blog => blog.BlogImages)
           .Where(blog => blog.Status == false && blog.AuthorId == userId)
           .ToList();
            return listBlog;
        }
    }
}
