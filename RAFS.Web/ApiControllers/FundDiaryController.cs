using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAFS.Core.DTO;
using RAFS.Core.Services.IService;

namespace RAFS.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundDiaryController : ControllerBase
    {
        protected readonly ICashFlowService _cashFlowService;

        public FundDiaryController(ICashFlowService cashFlowService)
        {
            _cashFlowService = cashFlowService;
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("ListCash")]
        public async Task<IActionResult> ListCash([FromQuery] RequestFilterCash filter)
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
                recordsTotal = await _cashFlowService.CountListCashFilter(filter.FarmId, 0, null, sortColumn, sortColumnDirection);

                recordsTotal = await _cashFlowService.CountListCashFilter(filter.FarmId, filter.TypeId, searchValue, sortColumn, sortColumnDirection);

                var data = await _cashFlowService.ListCashFilter(filter.FarmId, filter.TypeId, searchValue, sortColumn, sortColumnDirection, skip, pageSize);

                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPost("CreateCash")]
        public async Task<IActionResult> CreateCash(RequestCreateCashDTO data)
        {
            try
            {
                await _cashFlowService.CreateCashAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdateCashFlow")]
        public async Task<IActionResult> UpdateCashFlow(RequestUpdateCashDTO data)
        {
            try
            {
                await _cashFlowService.UpdateCashAsync(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpDelete("DeleteSofftCash/{cashId}")]
        public async Task<IActionResult> DeleteSofftCash(int cashId)
        {
            try
            {
                await _cashFlowService.DeleteCashAsync(cashId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Message: " + ex);
            }
        }
    }
}
