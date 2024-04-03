using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Models;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantMaterialHistoryController : ControllerBase
    {
        protected readonly IPlantMaterialHistoryService _plantMaterialHistoryService;

        public PlantMaterialHistoryController(IPlantMaterialHistoryService plantMaterialHistoryService)
        {
            _plantMaterialHistoryService = plantMaterialHistoryService;
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetInventoryItem")]
        public ActionResult<IEnumerable<Item>> GetInventoryItem(int farmid)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.GetInventoryItems(farmid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetAllPlantMaterialHistory")]
        public ActionResult<IEnumerable<TakeAndSendMaterial>> GetAllPlantMaterialHistory(int plantid)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.GetAllPlantMaterialHistory(plantid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Technician")]
        [HttpGet("GetAllFarmMaterialHistory")]
        public ActionResult<IEnumerable<TakeAndSendMaterial>> GetAllFarmMaterialHistory(int farmid)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.GetAllFarmMaterialHistory(farmid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetAllPlantMaterialHistoryByType")]
        public ActionResult<IEnumerable<TakeAndSendMaterialDTO>> GetAllPlantMaterialHistoryByType(int plantid,int typeId)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.GetAllPlantMaterialHistoryByType(plantid, typeId);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetPlantMaterialRecord")]
        public ActionResult<TakeAndSendMaterialDTO> GetPlantMaterialRecord(int id)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.GetPlantMaterialRecord(id);
                return Ok(milestoneList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdatePlantMaterialRecord")]
        public ActionResult<TakeAndSendMaterial> UpdatePlantMaterialRecord(TakeAndSendMaterial milestoneName)
        {
            try
            {
                TakeAndSendMaterialDTO milestone = _plantMaterialHistoryService.GetPlantMaterialRecord(milestoneName.Id);
                DateTime targetDatetime = milestone.LastUpdate;

                // Add 2 minutes to the target datetime
                DateTime twoMinutesLater = targetDatetime.AddDays(1);

                // Check if the current datetime is greater than 2 minutes after the target datetime
                bool isGreaterThanTwoMinutes = DateTime.Now > twoMinutesLater;
                if(isGreaterThanTwoMinutes == true)
                {
                    return BadRequest();
                }
                else {
                    milestoneName.LastUpdate = milestone.LastUpdate;
                    var milestoneList = _plantMaterialHistoryService.UpdatePlantMaterialRecord(milestoneName);
                    if (milestoneList)
                    {
                        return Ok(milestoneList);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpDelete("DeletePlantMaterialRecord")]
        public ActionResult DeletePlantMaterialRecord(int milestoneId)
        {
            try
            {
                var milestoneList = _plantMaterialHistoryService.DeletePlantMaterialRecord(milestoneId);

                if (milestoneList)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPost("CreatePlantMaterialRecord")]
        public ActionResult<TakeAndSendMaterial> CreatePlantMaterialRecord(TakeAndSendMaterial milestoneName)
        {
            try
            {
  
                milestoneName.LastUpdate = DateTime.Now;
                milestoneName.Status = true;


                var milestoneList = _plantMaterialHistoryService.CreatePlantMaterialRecord(milestoneName);
                if (milestoneList)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest();
                }
               
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }

}
