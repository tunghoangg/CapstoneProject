using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using System.Collections.Generic;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        protected readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("GetBlogList")] 
        public IActionResult GetBlogList(string? userId) {
            try
            {
                List<BlogDTO> blogList;
                if (userId == null || userId == "")
                {
                     blogList = _blogService.GetAllBlog();
                }
                else
                {
                    blogList = _blogService.GetBlogByUserId(userId);

                }

                return Ok(blogList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Owner,Staff")]
        [HttpGet("GetWaitingBlogList")]
        public IActionResult GetWaitingBlogList(string ?userId)
        {
            try
            {
                List<BlogDTO> blogList;
                if ( userId == null || userId == ""){
                        blogList = _blogService.GetWaitingBlog();
                    }
                 
                else
                {
                    blogList = _blogService.GetWaitingBlogByUserId(userId);

                }
                return Ok(blogList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpGet("GetBlogByTag")]
        public IActionResult GetBlogByTag(string tag)
        {
            try
            {
                var blogList = _blogService.GetBlogsByTag(tag);
                return Ok(blogList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetBlogByTitle")]
        public IActionResult GetBlogByTitle(string title)
        {
            try
            {
                var blogList = _blogService.SearchBlogByTitle(title);
                return Ok(blogList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Owner,Staff")]
        [HttpPost("CreateBlog")]
        public IActionResult CreateBlog([FromBody] CreateBlogDTO createBlogDTO)
        {
            try
            {
                _blogService.CreateBlog(createBlogDTO);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("CreateTag")]
        public IActionResult CreateTag([FromBody] string tagName)
        {
            try
            {
                
                return Ok(_blogService.CreateTag(tagName));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Staff,Owner")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
               
                return Ok(_blogService.DeleteBlog(id));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Staff,Owner")]
        [HttpPut("Update/{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] string body)
        {
            try
            {
                
                return Ok(_blogService.UpdateBlog(id, body));
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpGet("GetBlogById/{id}")]
        public IActionResult GetBlogById(int id)
        {
            try
            {
                var blog = _blogService.GetBlogDetail(id);
                return Ok(blog);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Staff")]
        [HttpPut("ApproveBlog/{id}")]
        public IActionResult ApproveBlog(int id)
        {
            try
            {
                _blogService.AprroveBlog(id);
                return Ok("đã duyệt bài đăng");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [HttpGet("GetAllTag")]
        public IActionResult GetAllTag()
        {
            try
            {
                var blog = _blogService.GetAllTag();
                return Ok(blog);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Staff")]
        [HttpDelete("DeleteTag/{id}")]
        public IActionResult DeleteTag(int id)
        {
            try
            {
                return Ok(_blogService.DeleteTag(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
