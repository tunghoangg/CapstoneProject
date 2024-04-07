using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;

namespace RAFS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public AdminController()
        {
        }
        
        [HttpGet]
        public IActionResult UserManagement()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StaffManagement()
        {
            return View();
        }
    }
}
