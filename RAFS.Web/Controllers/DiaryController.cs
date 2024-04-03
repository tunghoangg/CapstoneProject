using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class DiaryController : Controller
    {
        [Authorize(Roles = "Technician")]
        public IActionResult Index()
        {
            return View("DiaryManagement");
        }
    }
}
