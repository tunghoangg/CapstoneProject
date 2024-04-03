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
    public class GuestController : ControllerBase
    {
        protected readonly IQuestionFormService _questionFormService;

        public GuestController(IQuestionFormService questionFormService)
        {
            _questionFormService = questionFormService;
        }

        [HttpPost("SendQuestionForm")]
        public IActionResult SendQuestionForm([FromBody] QuestionFormDTO form)
        {
            try
            {
                _questionFormService.SendQuestionForm(form);
                return Ok();
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ xảy ra
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        [Authorize(Roles = "Staff")]
        [HttpGet("GetFormList/{page}")]
        public IActionResult GetFormList(string? sort, int page = 1)
        {
            try
            {
                var pageResults = 10f;
                var forms = _questionFormService.GetForms(sort);
                var pageCount = (int)Math.Ceiling(forms.Count() / pageResults);

                var formsOnPage = forms
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                var response = new PaginateFormListDTO
                {
                    Forms = formsOnPage,
                    CurrentPage = page,
                    Pages = pageCount
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound("Exception");
            }
        }
        [Authorize(Roles = "Staff")]

        [HttpGet("GetQuestionContent/{id:int}")]
        public IActionResult GetQuestionContent(int id)
        {
            var content = _questionFormService.GetQuestionCotent(id);
            if (content == null)
            {
                // Return a 404 Not Found response if the product is not found
                return NotFound();
            }

            // Return a 200 OK response with the product if it's found
            return Ok(content);
        }
        [Authorize(Roles = "Staff")]

        [HttpPut("UpdateStatus/{id}")]
        public IActionResult UpdateBlog(int id, [FromBody] bool status)
        {
            try
            {
                _questionFormService.ChangeFormStatus(id, status);
                return Ok("Cập nhật thành công");
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
