using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
