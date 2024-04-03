using AutoMapper;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.Models;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;
using RAFS.Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserManager<AspUser> _userManager;
        private readonly IMapper _mapper;
        private readonly DriveAPIController _drive;

        public ProfileController(ILogger<AuthenticationController> logger,
                                UserManager<AspUser> userManager,
                                IMapper mapper,
                                DriveAPIController drive)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _drive = drive;
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.userId);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.newPassword);
                if (result.Errors.Any())
                {
                    return BadRequest(result);
                }
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("UpdateInfo")]
        public async Task<IActionResult> UpdateInfo([FromBody] UpdateInfoModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.FullName = model.FullName;
                user.Address = model.Address;
                user.Description = model.Description;
                user.LastUpdated = DateTime.UtcNow;
                var result = await _userManager.UpdateAsync(user);

                if (result.Errors.Any())
                {
                    return BadRequest(result);
                }
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(ChangeAvatarModel data)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(data.UserId);
                if (user != null)
                {
                    string serverPath = Path.GetTempFileName();
                    using (var stream = new FileStream(serverPath, FileMode.Create))
                    {
                        data.Avatar.CopyTo(stream);
                    }
                    string logoLink = user.Avatar;

                    string saveLogoLink = await _drive.CreateDriveFile(serverPath);
                    user.Avatar = saveLogoLink;
                    var res = await _userManager.UpdateAsync(user);
                    if (res == null)
                    {
                        return BadRequest("Cannot upload logo!");
                    }
                    bool result = await _drive.UpdateDriveFile(logoLink, serverPath);
                    return Ok();
                }
                return BadRequest();


            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }

        [HttpGet("GetAvatar")]
        public async Task<IActionResult> GetAvatar()
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    var avatar = user.Avatar;
                    return Ok(avatar);
                }
                return BadRequest("User not found!");


            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }

        [HttpPost("AddPassword")]
        public async Task<IActionResult> AddPassword(AddPasswordModel addPassword)
        {
            AspUser user = await _userManager.FindByIdAsync(addPassword.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.AddPasswordAsync(user, addPassword.newPassword);
            if (result.Errors.Any())
            {
                return BadRequest(result);
            }

            return Ok();
        }
    }
}
