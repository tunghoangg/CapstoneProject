using AutoMapper;
using Azure;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Repositories.Repo;
using RAFS.Core.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RAFS.Core.Services.Service
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BlogService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public List<BlogDTO> GetAllBlog()
        {
            List<Blog> bList = new List<Blog>();
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            bList = _uow.blogRepo.GetAllBlog();
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status =blog.Status
                }); ;
            }
            return bDTOList;
        }
        public List<BlogDTO> GetWaitingBlog()
        {
            List<Blog> bList = new List<Blog>();
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            bList = _uow.blogRepo.GetWaitingBlog();
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status = blog.Status
                }); ;
            }
            return bDTOList;
        }
        public List<BlogDTO> GetBlogsByTag(string tag)
        {
            List<Blog> bList = new List<Blog>();
            bList = _uow.blogRepo.GetBlogsByTag(tag);
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status = blog.Status
                });
            }
            return bDTOList;
        }

        public List<BlogDTO> SearchBlogByTitle(string title)
        {
            List<Blog> bList = new List<Blog>();
            bList = _uow.blogRepo.SearchBlogByTitle(title);
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status = blog.Status
                });
            }
            return bDTOList;
        }
        public void CreateBlog(CreateBlogDTO createBlogDTO)
        {
            Blog blog = new Blog();
            blog.Title = createBlogDTO.Title;
            blog.Body = createBlogDTO.Body;
            blog.AuthorId = createBlogDTO.AuthorId;
            blog.CreatedDate = DateTime.Now;
            blog.LastUpdated = DateTime.Now;
            blog.Status = false;
           _uow.blogRepo.Add(blog);
            _uow.Save();



            foreach (string tagName in createBlogDTO.ListTag)
            {
                BlogTag blogTag = new BlogTag();
                Tag tag = new Tag();
                    tag =_uow.tagRepo.getTagByName(tagName);
                    blogTag.TagId = tag.Id;
                    blogTag.BlogId = blog.Id;
                    _uow.blogTagRepo.Add(blogTag);
                    _uow.Save();
            }
           foreach(string url in createBlogDTO.UrlImage)
            {
                BlogImage img = new BlogImage();
                img.BlogId = blog.Id;   
                img.URL = url;  
                _uow.blogImageRepo.Add(img);
                _uow.Save();
            }

            
            
        }
        public bool DeleteBlog(int blogId)
        {
            bool status = _uow.blogRepo.GetBlogById(blogId).Status;
            _uow.blogTagRepo.DeleteBlogTagById(blogId);
            _uow.blogImageRepo.DeleteBlogImageById(blogId);
            _uow.blogRepo.DeleteById(blogId);
            _uow.Save();
            return status;
        }
        public List<String> GetAllTagName()
        {
            List<Tag> tags = _uow.tagRepo.GetAll();
            List<String>listTags = new List<String>();
            foreach (Tag tag in tags) {
            string name = tag.Name;
                listTags.Add(name);
            }
            return listTags;    
        }
        public List<Tag> GetAllTag()
        {
            List<Tag> tags = _uow.tagRepo.GetAll();
            return tags;
        }
        public bool DeleteTag(int id)
        {
            bool exist = false;
            List<BlogTag> tags = _uow.blogTagRepo.GetAll();
            foreach (BlogTag tag in tags) { 
                if(tag.TagId == id)
                {
                    exist=true;
                }
            }
            if (exist==false)
            {
                _uow.tagRepo.DeleteById(id);
                _uow.Save();
                return true;
            }
            return false;
        }
        public bool UpdateBlog(int id,string body)
        {
            var blog = _uow.blogRepo.GetBlogById(id);
            blog.Body= body;
            // Mã HTML chứa các đường dẫn URL của hình ảnh
            string htmlContent = body;

            // Tạo một danh sách để lưu trữ các đường dẫn URL
            List<string> imageUrls = new List<string>();

            // Sử dụng HtmlAgilityPack để phân tích mã HTML
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Lấy tất cả các phần tử img từ mã HTML
            HtmlNodeCollection imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");

            // Nếu có các phần tử img, trích xuất đường dẫn URL và thêm vào danh sách
            if (imgNodes != null)
            {
                foreach (HtmlNode imgNode in imgNodes)
                {
                    string imageUrl = imgNode.GetAttributeValue("src", "");
                    imageUrls.Add(imageUrl);
                }
            }

            blog.LastUpdated= DateTime.Now;
            var listImage = _uow.blogImageRepo.GetBlogImageById(id);
            foreach (string url in imageUrls)
            {
                BlogImage img = new BlogImage();
                img.BlogId = blog.Id;
                img.URL = url;
                foreach(BlogImage image in listImage)
                {
                    if (!body.Contains(image.URL))
                    {
                        _uow.blogImageRepo.Delete(image);
                    }
                    if (!image.URL.Contains(url))
                    {
                        _uow.blogImageRepo.Add(img);
                    }
                }
            }
            _uow.blogRepo.Update(blog);
            _uow.Save();
            return blog.Status;
        }
        public BlogDTO GetBlogDetail(int id)
        {
            var blog = _uow.blogRepo.GetBlogById(id);
            BlogDTO blogDetail = new BlogDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Body = blog.Body,
                AuthorName = blog.Author != null ? blog.Author.UserName : null,
                CreateDate = blog.CreatedDate,
                LastUpdated = blog.LastUpdated,
                Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                Status = blog.Status
            };
            return blogDetail;
        }
        public void AprroveBlog(int id)
        {
            var blog = _uow.blogRepo.GetBlogById(id);
            blog.Status = true;
            _uow.blogRepo.Update(blog);
            _uow.Save();
        }

        public Tag CreateTag(string tagName)
        {
            Tag tag = new Tag();
            Tag lastTag = _uow.tagRepo.getLastTag();
            if (_uow.tagRepo.tagCheck(tagName) == false)
            {
                if (lastTag != null)
                {
                    tag.Id = lastTag.Id+1;
                }
                else
                {
                    tag.Id = 1;
                }
                tag.Name = tagName;
                _uow.tagRepo.Add(tag);
                _uow.Save();
                return tag;
            }
            else
            {
                return null;
            }
            
        }

        public List<BlogDTO> GetBlogByUserId(string userId)
        {
            List<Blog> bList = new List<Blog>();
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            bList = _uow.blogRepo.GetBlogByUserId(userId);
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status = blog.Status
                }); ;
            }
            return bDTOList;
        }

        public List<BlogDTO> GetWaitingBlogByUserId(string userId)
        {
            List<Blog> bList = new List<Blog>();
            List<BlogDTO> bDTOList = new List<BlogDTO>();
            bList = _uow.blogRepo.GetWaitingBlogByUserId(userId);
            foreach (Blog blog in bList)
            {

                bDTOList.Add(new BlogDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Body = blog.Body,
                    AuthorName = blog.Author != null ? blog.Author.UserName : null,
                    CreateDate = blog.CreatedDate,
                    LastUpdated = blog.LastUpdated,
                    Tag = _uow.blogRepo.GetAllTagNameInBlog(blog),
                    ImageURL = _uow.blogRepo.GetAllImageURLInBlog(blog),
                    Status = blog.Status
                }); ;
            }
            return bDTOList;
        }
    }
}
