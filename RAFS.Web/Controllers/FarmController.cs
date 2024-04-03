using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class FarmController : Controller
    {
        [Authorize(Roles = "Owner")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
