using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        protected readonly IStatisticServices _statisticServices;
        protected readonly DriveAPIController _drive;

        public StatisticsController(IStatisticServices statisticServices, DriveAPIController drive)
        {
            _statisticServices = statisticServices;
            _drive = drive;
        }
        [Authorize(Roles = "Owner")]
        [HttpGet("ListFarmStatistic/{userId}")]
        public async Task<IActionResult> ListFarmStatistic(string userId)
        {
            try
            {
                if (userId == string.Empty)
                {
                    return NotFound("User not found!");
                }
                return Ok(await _statisticServices.ListFarmByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpGet("StatisticFarmAdmin/{userId}/{farmIdSelected}/{yearSelected}")]
        public async Task<IActionResult> StatisticFarmAdmin(string userId, int farmIdSelected, int yearSelected)
        {
            
            try
            {
                var model = await _statisticServices.StatisticByFarmAndYear(userId , farmIdSelected, yearSelected);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
    }
}
