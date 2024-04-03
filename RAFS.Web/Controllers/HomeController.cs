using Microsoft.AspNetCore.Mvc;
using RAFS.Web.Models;
using System.Diagnostics;

namespace RAFS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Farm()
        {
            return View();
        }

        public IActionResult FarmDetails(int id)
        {
            ViewData["id"] = id;
            return View();
        }
        public IActionResult Blog() 
        {
            return View();
        }
        public IActionResult BlogDetails(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
