using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;

namespace RAFS.Web.Controllers
{
    public class BlogController : Controller
    {
        
        private readonly IWebHostEnvironment _env;
        IBlogService _blog;
        DriveAPIController _drive;
        public BlogController(IBlogService blog, IWebHostEnvironment env, DriveAPIController drive)
        {
          _blog = blog;
          _env = env;
          _drive = drive;
        }
        [Authorize(Roles = "Owner,Staff")]
        public IActionResult BlogList()
        {
            ViewData["blogList"] = _blog.GetAllBlog();
            ViewData["waitingBlogList"] = _blog.GetWaitingBlog();
            ViewData["listAllTags"] = _blog.GetAllTagName();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(List<IFormFile> files)
        {
            var fileUrl = "";
            var filePath = "";
            foreach(IFormFile file in Request.Form.Files)
            {
                string serverPath = Path.GetTempFileName();
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                    fileUrl = await _drive.CreateDriveFile(serverPath);
            }
            return Json(new { url = fileUrl });
        }

    }
}
