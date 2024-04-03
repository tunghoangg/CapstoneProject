using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class SystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
