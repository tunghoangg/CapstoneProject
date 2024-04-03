using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        protected readonly IFarmService _farmService;
        protected readonly IBlogService _blogService;

        public HomePageController(IFarmService farmService, IBlogService blogService)
        {
            _farmService = farmService;
            _blogService = blogService;

        }
        [HttpGet("GetTagInHomePage")]
        public IActionResult GetTagInHomePage()
        {
            try
            {
                var random = new Random();
                var tags = _blogService.GetAllTagName().ToList();
                return Ok(tags);
            }
            catch (Exception)
            {

                return NotFound("Exception");
            }
        }

        [HttpGet("GetFilteredBlog/{page}")]
        public IActionResult GetHomepageBlogList(string? tag, string? title, string? sort, int page = 1, float pageResults = 6f)
        {
            try
            {
                List<BlogDTO> blogs;
                 if (tag != null)
                {
                    blogs = _blogService.GetBlogsByTag(tag);
                }
                else if (title != null)
                {
                    blogs = _blogService.SearchBlogByTitle(title);

                }
                
                else
                        {
                              blogs = _blogService.GetAllBlog();
                        }

                var pageCount = (int)Math.Ceiling(blogs.Count() / pageResults);

                var blogsOnPage = blogs
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                var response = new BlogResponses
                {
                    blogs = blogsOnPage,
                    CurrentPage = page,
                    Pages = pageCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound("Exception");
            }
        }

        [HttpGet("GetFilteredFarm/{page}")]
        public IActionResult GetHomepageFarmList(string? search, string? province, string? district, string? ward, string? sort, string? area, int page = 1)
        {
            try
            {
                var pageResults = 6f;
                var farms = _farmService.GetHomepageFarmList(search, province, district, ward, sort, area);
                var pageCount = (int)Math.Ceiling(farms.Count() / pageResults);

                var farmsOnPage = farms
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                var response = new FarmResponses
                {
                    Farms = farmsOnPage,
                    CurrentPage = page,
                    Pages = pageCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound("Exception");
            }
        }

        [HttpGet("GetFarmDetails/{id:int}")]
        public IActionResult SearchProductById(int id)
        {
            var product = _farmService.GetFarmDetailsInHomepage(id);
            if (product == null)
            {
                // Return a 404 Not Found response if the product is not found
                return NotFound();
            }

            // Return a 200 OK response with the product if it's found
            return Ok(product);
        }

        [HttpGet("GetNewestFarm")]
        public IActionResult GetNewestFarm()
        {
            try
            {
                var news = _farmService.GetNewsFarm();
                return Ok(news);
            }
            catch (Exception)
            {
                return NotFound("Exception");
            }
        }
    }
}
