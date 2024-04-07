using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.IRepo;
using RAFS.Core.Services.IService;
using RAFS.Web.Models;

namespace RAFS.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<AspUser> _userManager;
        private readonly IFarmAdminService _farmAdminService;
        private readonly IMapper _mapper;

        public ProfileController(ILogger<AuthenticationController> logger,
                                UserManager<AspUser> userManager,
                                IFarmAdminService farmAdminService,
                                IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _farmAdminService = farmAdminService;
            _logger = logger;
        }

        public async Task<IActionResult> ViewProfile()
        {
            AspUser currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Authentication");
            } else
            {
                ViewBag.Profile = currentUser;
                return View();
            }
        }

        
    }
}
