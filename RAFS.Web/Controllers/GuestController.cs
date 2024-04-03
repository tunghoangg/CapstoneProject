using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RAFS.Web.Controllers
{
    public class GuestController : Controller
    {
        private readonly ILogger<GuestController> _logger;

        public GuestController(ILogger<GuestController> logger)
        {
            _logger = logger;
        }
        [Authorize(Roles = "Staff")]
        public IActionResult QuestionFormPage()
        {
            return View();
        }
    }
}
