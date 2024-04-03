using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
