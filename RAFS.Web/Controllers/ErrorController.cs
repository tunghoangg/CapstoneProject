using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("[action]")]
    public class ErrorController : Controller
    {
        [HttpGet]
        
        public IActionResult Error404()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View();
        }
        [HttpGet]
        public IActionResult Error505()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
