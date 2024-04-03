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
    public class QRController : ControllerBase
    {
        protected readonly IPlantService _plantService;


        public QRController( IPlantService plantService)
        {
          

            _plantService = plantService;
        }
   
        [HttpGet("GetPlantRecord")]
        public ActionResult<IEnumerable<PlantRecordDTO>> GetPlantRecord(int plantid)
        {
            try
            {
               Plant plant = _plantService.GetPlantById(plantid);
               
                var milestoneList = _plantService.GetAllPlantRecord(plantid);
                var response = new PlantRecordResponseDTO
                {
                    PlantRecords = milestoneList,
                    Name = plant.Name,
                    Type = plant.Type,
                    Description = plant.Description,
                    Area = plant.Area,
                    AreaUnit = plant.AreaUnit,
                    PlantingMethod = plant.PlantingMethod,
                    Status = (plant.Status == true) ? "Vẫn trồng" : "Đã hủy",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

   
      

    }

}
