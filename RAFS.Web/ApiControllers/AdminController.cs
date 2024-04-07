using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Repositories.Generics;
using RAFS.Web.Models;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AspUser> _userManager;
        private readonly IMapper _mapper;


        public AdminController(IUnitOfWork unitOfWork, 
            UserManager<AspUser> userManager,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("UpdateAccountStatus")]
        public async Task<IActionResult> UpdateAccountStatusAsync(UpdateUserStatusModel model)
        {
            AspUser user = await _userManager.FindByIdAsync(model.UserId);
            if (user != null)
            {
                user.Status = model.Status;
                await _unitOfWork.aspUserRepo.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            return BadRequest("User not found");
        }

        [HttpGet("GetAllUserAccount")]
        public async Task<IActionResult> GetAllUserAccountAsync()
        {
            List<AspUser> userList = new List<AspUser>() {};

            try
            {
                //Get owners
                var owners = await _userManager.GetUsersInRoleAsync("Owner");
                //Get technician
                var techs = await _userManager.GetUsersInRoleAsync("Technician");
                HashSet<AspUser> tempList = new HashSet<AspUser>(owners);

                foreach (var tech in techs)
                {
                    if (!tempList.Contains(tech))
                    {
                        tempList.Add(tech);
                    }
                }

                userList = tempList.ToList();
                return Ok(userList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllStaffAccount")]
        public async Task<IActionResult> GetAllStaffAccount()
        {
            List<AspUser> userList = new List<AspUser>() { };
            try
            {
                var staffs = await _userManager.GetUsersInRoleAsync("Staff");
                userList.AddRange(staffs);
                return Ok(userList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddStaffAccount")]
        public async Task<IActionResult> AddStaffAccount(RegistrationDTO registration)
        {
            var user = _mapper.Map<AspUser>(registration);
            user.Status = true;
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, registration.Password);

            if (result.Errors.Any())
            {
                return BadRequest(result);
            }

            await _userManager.AddToRoleAsync(user, "Staff");
            return Ok();
        }

        //[HttpPost("UpdateStaffAccount")]
        //public async Task<IActionResult> UpdateStaffAccount()
        //{
        //    var user = _mapper.Map<AspUser>(registration);
        //    user.Status = true;
        //    user.EmailConfirmed = true;

        //    var result = await _userManager.CreateAsync(user, registration.Password);

        //    if (result.Errors.Any())
        //    {
        //        return BadRequest(result);
        //    }

        //    await _userManager.AddToRoleAsync(user, "Staff");
        //    return Ok();
        //}
    }
}
