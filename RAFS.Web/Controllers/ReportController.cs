using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
