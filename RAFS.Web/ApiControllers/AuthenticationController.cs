using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;
using System.Security.Claims;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<AspUser> _signInManager;
        private readonly UserManager<AspUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ISendMailService _sendMailService;
        private readonly IFarmAdminService _farmAdminService;
        private readonly IUnitOfWork _uow;

        public AuthenticationController(
            IMapper mapper,
            ILogger<AuthenticationController> logger,
            SignInManager<AspUser> signInManager,
            UserManager<AspUser> userManager,
            ISendMailService sendMailService,
            IFarmAdminService farmAdminService,
            IUnitOfWork uow)
        {
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _sendMailService = sendMailService;
            _farmAdminService = farmAdminService;
            _uow = uow;
        }

        [HttpGet("GetUserId")]
        public IActionResult getCurrentUserId()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetUserFarmId")]
        public async Task<IActionResult> GetUserFarmIdAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                int farmId = 0;

                if (user != null)
                {
                    //Get current user roles
                    var roles = await _userManager.GetRolesAsync(user);

                    foreach (var role in roles)
                    {
                        //Check for Technician
                        if (role.Equals("Technician"))
                        {
                            Farm farm = await _farmAdminService.GetFarmByUserId(user.Id);
                            if (farm != null)
                            {
                                if (!farm.Status)
                                {
                                    return BadRequest("Trang trại chủ quản không hoạt động.");
                                }

                                //Get farmId
                                farmId = farm.Id;
                                return Ok(farmId);
                            }
                            if (farm == null)
                            {
                                return BadRequest("Tài khoản nhân viên không thuộc vào trang trại nào.");
                            }
                        }
                    }

                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRolesAsync()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return Ok(roles);
                }
                return BadRequest("User not found");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetUserFuncs")]
        public async Task<IActionResult> GetUserFuncsAsync()
        {
            try
            {
                //Get current logged in user
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Farm farm = await _farmAdminService.GetFarmByUserId(user.Id);

                if (user != null && farm != null)
                {
                    //Get UserFunctionFarm
                    var uff = await _uow.uffRepo.GetTechUFFsByFarmId(user.Id, farm.Id);
                    //Get FunctionId
                    var funcs = uff.Select(uff => uff.FunctionId).ToList();
                    return Ok(funcs);
                }
                return BadRequest("Something went wrong when fetching functions");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
