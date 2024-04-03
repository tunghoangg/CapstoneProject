using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
