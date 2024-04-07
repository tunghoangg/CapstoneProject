using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        protected readonly IFarmAdminService _farmAdminService;
        protected readonly DriveAPIController _drive;

        public FarmController(IFarmAdminService farmAdminService, DriveAPIController drive)
        {
            _farmAdminService = farmAdminService;
            _drive = drive;
        }
        [Authorize(Roles = "Owner")]        
        
        [HttpGet("ListFarmAdminitrator/{userId}")]
        public async Task<IActionResult> ListFarmAdminitrator(string userId)
        {
            try
            {
                if (userId == string.Empty)
                {
                    return NotFound("User not found!");
                }
                return Ok(await _farmAdminService.GetFarmAdminDTOsAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpGet("GetFarmAdminitratorById/{farmId}")]
        public async Task<IActionResult> GetFarmAdminitratorById(int farmId)
        {
            try
            {
                if (farmId.ToString() == string.Empty)
                {
                    return NotFound("Farm not found!");
                }
                return Ok(await _farmAdminService.GetFarmAdminDTOByFarmId(farmId));
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPost("CreateFarm")]
        public async Task<IActionResult> CreateFarm(AddFarmAdminDTO data)
        {
            try
            {
                await _farmAdminService.CreateFarmAdminDTOAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateFarm")]
        public async Task<IActionResult> UpdateFarm(UpdateFarmAdminDTO data)
        {
            try
            {
                await _farmAdminService.UpdateFarmAdminDTOAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateLogoFarm")]
        public async Task<IActionResult> UpdateLogoFarm(UpdateImageFarmAdminDTO data)
        {
            //string imgLink, IFormFile files
            try
            {
                string serverPath = Path.GetTempFileName();
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    data.ImageFile.CopyTo(stream);
                }
                string logoLink = await _farmAdminService.GetLogoLinkByFarmId(data.FarmId);
                string saveLogoLink = await _drive.CreateDriveFile(serverPath);
                if (!logoLink.Equals("https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik"))
                {
                    await _drive.DeleteDriveFile(logoLink);
                }

                
                await _farmAdminService.UpdateLogoLink(data.FarmId, saveLogoLink);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpGet("ListUFFAdminitrator/{userId}/{farmId}")]
        public async Task<IActionResult> ListUFFAdminitrator(string userId, int farmId)
        {
            try
            {
                if (farmId.ToString() == string.Empty && userId == string.Empty)
                {
                    return BadRequest("Can't load!");
                }
                var list = await _farmAdminService.GetUFFsByFarmId(userId, farmId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateUFF")]
        public async Task<IActionResult> UpdateUFF(UpdateUFFDTO data)
        {
            try
            {
                await _farmAdminService.UpdateUserFunction(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateStatusTechnician/{userId}")]
        public async Task<IActionResult> UpdateStatusTechnician(string userId)
        {
            try
            {
                string newPass = await _farmAdminService.ChangeStatusTechnician(userId);
                return Ok(newPass);
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateStatusFarm/{farmId}")]
        public async Task<IActionResult> UpdateStatusFarm(int farmId)
        {
            try
            {
                await _farmAdminService.SoftDeleteFarm(farmId);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpGet("GetImagesByFarmId/{farmImageId}")]
        public async Task<IActionResult> GetImagesByFarmId(int farmImageId)
        {
            try
            {
                if (farmImageId.ToString() == string.Empty)
                {
                    return NotFound("Farm not found!");
                }
                return Ok(await _farmAdminService.GetImagesFarmDTO(farmImageId));
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpPut("UpdateImagesFarm")]
        public async Task<IActionResult> UpdateImagesFarm(UpdateImagesFarmnDTO data)
        {
            try
            {
                string serverPath = Path.GetTempFileName();
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    data.ImageFile.CopyTo(stream);
                }
                string imageURL = await _farmAdminService.GetImageURLByImageId(data.ImageId);
                string saveImageURL = await _drive.CreateDriveFile(serverPath);
                if (!imageURL.Equals("https://lh3.googleusercontent.com/d/1-cD42GWStpz0_4kJDCSSHOgFOwcDX3ik"))
                {
                    await _drive.DeleteDriveFile(imageURL);
                }


                await _farmAdminService.UpdateImagesFarm(data.ImageId, saveImageURL);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }


    }
}