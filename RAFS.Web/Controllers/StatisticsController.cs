using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class StatisticsController : Controller
    {
        [Authorize(Roles = "Owner")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
