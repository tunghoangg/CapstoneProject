using Google.Apis.Drive.v3.Data;
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
    public class PlantController : ControllerBase
    {
        protected readonly IMilestoneService _milestoneService;
        protected readonly IPlantService _plantService;
        protected readonly DriveAPIController _drive;

      
        public PlantController(IMilestoneService milestoneService, IPlantService plantService, DriveAPIController drive)
        {
            _milestoneService = milestoneService;
            _plantService = plantService;
            _drive = drive;
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("CheckMilestoneName")]
        public ActionResult<bool> CheckMilestoneName(int farmid,string name)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllMilestone(farmid);
                var milestone = milestoneList.FirstOrDefault(x=>x.Name.ToLower().Equals(name.ToLower()));
                if(milestone != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }

                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("CheckMilestoneUpdateName")]
        public ActionResult<bool> CheckMilestoneUpdateName(int farmid, string name, int id)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllMilestone(farmid);
                var milestone = milestoneList.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
                if (milestone != null)
                {
                    if(milestone.Id != id)
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return Ok(false);
                    }
                   
                }
                else
                {
                    return Ok(false);
                }


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetSearchDateMilestone")]
        public ActionResult<IEnumerable<Milestone>> GetSearchDateMilestone(int farmid, string search)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmFilteredMilestones(farmid).Where(x => x.Name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.LastUpdate).ToList();
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetSearchNumMilestone")]
        public ActionResult<IEnumerable<Milestone>> GetSearchNumMilestone(int farmid, string search)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmFilteredMilestones(farmid).Where(x => x.Name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.NumberOfPlants).ToList();
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetSearchMilestone")]
        public ActionResult<IEnumerable<Milestone>> GetSearchMilestone(int farmid, string search)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmFilteredMilestones(farmid).Where(x=>x.Name.ToLower().Contains(search.ToLower())).ToList();
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetMilestoneOdByLastUpdate")]
        public ActionResult<IEnumerable<Milestone>> GetMilestoneOdByLastUpdate(int farmid)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmFilteredMilestones(farmid).OrderByDescending(x=>x.LastUpdate).ToList();
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetMilestoneOdByNumberOfPlant")]
        public ActionResult<IEnumerable<Milestone>> GetMilestoneOdByNumberOfPlant(int farmid)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmFilteredMilestones(farmid).OrderByDescending(x =>x.NumberOfPlants).ToList();
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetMilestoneList")]
        public ActionResult<IEnumerable<Milestone>> GetmilestoneList(int farmid)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllMilestone(farmid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetMilestonesPlantList")]
        public ActionResult<IEnumerable<PlantDeleteDTO>> GetMilestonesPlantList(int milestone)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllPlantofMilestone(milestone);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetListAllFarmMilestones")]
        public ActionResult<IEnumerable<Milestone>> GetListAllFarmMilestones(int farmid, int page)
        {
            try
            {
                var milestoneList = _milestoneService.GetAllFarmMilestones(farmid,page);
                return Ok(milestoneList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetMilestoneById")]
        public ActionResult<Milestone> GetMilestoneById(int id)
        {
            try
            {
                var milestoneList = _milestoneService.GetMilestoneById(id);
                return Ok(milestoneList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdateMilestone")]
        public ActionResult<Milestone> UpdateMilestone(Milestone milestone)
        {
            try
            {
                Milestone milestone1 = _milestoneService.GetMilestoneById(milestone.Id);

                milestone1.Id = milestone.Id;
                milestone1.Name = milestone.Name;
                milestone1.Description = milestone.Description;
                milestone1.LastUpdate = milestone.LastUpdate;
                milestone1.Status = milestone.Status;


                var milestoneList = _milestoneService.UpdateMilestone(milestone1);
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
        [HttpDelete("DeleteMilestone")]
        public ActionResult DeleteMilestone(int milestoneId)
        {
            try
            {
                var milestoneList = _milestoneService.DeleteMilestone(milestoneId);

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
        [HttpPost("CreateMilestone")]
        public ActionResult<Milestone> CreateMilestone(Milestone milestone)
        {
            try
            {
               
                

                var milestoneList = _milestoneService.CreateMilestone(milestone);

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
        [HttpGet("GetPlantList")]
        public ActionResult<IEnumerable<Plant>> GetPlantList(int farmid)
        {
            try
            {
                var milestoneList = _plantService.GetAllFarmPlant(farmid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetPlantById")]
        public ActionResult<Plant> GetPlantById(int id)
        {
            try
            {
                var milestoneList = _plantService.GetPlantById(id);
                return Ok(milestoneList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdatePlantHeal")]
        public ActionResult<Plant> UpdatePlantHeal(int plantid, int typeHeal)
        {
            try
            {
                var milestoneList = _plantService.GetPlantById(plantid);
                milestoneList.HealthCondition = typeHeal;
                var result = _plantService.UpdatePlant(milestoneList);

                if (result)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdatePlanttoMilestone")]
        public ActionResult<Plant> UpdatePlanttoMilestone(int plantid,int milestoneid)
        {
            try
            {
                var milestoneList = _plantService.GetPlantById(plantid);
                milestoneList.MilestoneId = milestoneid;
                 var result = _plantService.UpdatePlant(milestoneList);

                if (result)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdatePlantImage")]
        public async Task<ActionResult<Plant>> UpdatePlantImage(PlantDTO milestoneName)
        {
            try
            {
                string serverPath = Path.GetTempFileName();
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    milestoneName.Image.CopyTo(stream);
                }
                string saveLogoLink = await _drive.CreateDriveFile(serverPath);


                Plant milestoneList = _plantService.GetPlantById(milestoneName.Id);
                await _drive.DeleteDriveFile(milestoneList.Image);

                milestoneList.Image = saveLogoLink;
                var result = _plantService.UpdatePlant(milestoneList);

                if (result)
                {
                    return Ok(milestoneName);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdatePlant")]
        public ActionResult<Plant> UpdatePlant(Plant milestoneName)
        {
            try
            {
                var milestoneList = _plantService.GetPlantById(milestoneName.Id);
               
                milestoneList.Name = milestoneName.Name;    
                milestoneList.MilestoneId = milestoneName.MilestoneId;
                milestoneList.Type = milestoneList.Type;
                milestoneList.Description = milestoneName.Description;
                milestoneList.Area = milestoneName.Area;
                milestoneList.AreaUnit = milestoneName.AreaUnit;
                milestoneList.HealthCondition = milestoneName.HealthCondition;
                milestoneList.PlantingMethod = milestoneName.PlantingMethod;

                var result = _plantService.UpdatePlant(milestoneList);

                if (result)
                {
                    return Ok(milestoneName);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpDelete("DeletePlant")]
        public ActionResult DeletePlant(int milestoneId)
        {
            try
            {
                var milestoneList = _plantService.DeletePlant(milestoneId);

                if (milestoneList)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest("Chưa cập nhật thành công");
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPost("CreatePlant")]
        public async Task<ActionResult<Plant>> CreatePlant(PlantDTO2 milestoneName)
        {
            try
            {
                string saveLogoLink = "";
                
                    string serverPath = Path.GetTempFileName();
                    using (var stream = new FileStream(serverPath, FileMode.Create))
                    {
                        milestoneName.Image.CopyTo(stream);
                    }
                    saveLogoLink = await _drive.CreateDriveFile(serverPath);
               


                Plant milestoneList = new Plant();
                milestoneList.Name = milestoneName.Name;
                milestoneList.MilestoneId = milestoneName.MilestoneId;
                milestoneList.Type = milestoneName.Type;
                milestoneList.Description = milestoneName.Description;
                milestoneList.Area = milestoneName.Area;
                milestoneList.AreaUnit = milestoneName.AreaUnit;
                milestoneList.HealthCondition = milestoneName.HealthCondition;
                milestoneList.PlantingMethod = milestoneName.PlantingMethod;

                milestoneList.Image = saveLogoLink;




                milestoneList.LastUpdate = DateTime.Now;
                milestoneList.Status = true;

                var result = _plantService.CreatePlant(milestoneList);

                if (result)
                {
                    return Ok(milestoneList);
                }
                else
                {
                    return BadRequest("Chưa tạo thành công");
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }

}
