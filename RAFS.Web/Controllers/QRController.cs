using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class QRController : Controller
    {
        public IActionResult Index()
        {
            return View("QRScreen");
        }
    }
}
