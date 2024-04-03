using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RAFS.Core.DTO;
using RAFS.Core.Services.IService;
using RAFS.Core.Services.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        protected readonly IMaterialsService _materialsService;

        public MaterialsController(IMaterialsService materialsService)
        {
            _materialsService = materialsService;
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("ListItem")]
        public async Task<IActionResult> ListItem([FromQuery] RequestFilterItem filter)
        {
            try
            {
                var draw = Request.Query["draw"].ToString();
                var start = Request.Query["start"].FirstOrDefault();
                var length = Request.Query["length"].FirstOrDefault();
                var sortColumn = Request.Query["columns[" + Request.Query["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Query["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Query["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                
                int recordsTotal = 0;
                recordsTotal = await _materialsService.CountListItemFilter(filter.FarmId, 0, null, sortColumn, sortColumnDirection);

                recordsTotal = await _materialsService.CountListItemFilter(filter.FarmId, filter.TypeId, searchValue, sortColumn, sortColumnDirection);

                var data = await _materialsService.ListItemFilter(filter.FarmId, filter.TypeId, searchValue, sortColumn, sortColumnDirection, skip, pageSize);

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal , data = data };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem(RequestCreateItemDTO data)
        {
            try
            {
                await _materialsService.CreateItemAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem(RequestUpdateItemDTO data)
        {
            try
            {
                await _materialsService.UpdateItemAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpDelete("DeateSofftItem/{itemId}")]
        public async Task<IActionResult> DeateSofftItem(int itemId)
        {
            try
            {
                await _materialsService.DeleteItemAsync(itemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
    }
}
