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
    public class DiaryController : ControllerBase
    {
        protected readonly IDiaryService _diaryService;
        protected readonly DriveAPIController _drive;
        public DiaryController(IDiaryService diaryService, DriveAPIController drive)
        {
            _diaryService = diaryService;
            _drive = drive;
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetDiaryList")]
        public ActionResult<IEnumerable<DiaryDTO>> GetDiaryList(int farmid)
        {
            try
            {
                var milestoneList = _diaryService.GetAllDiary(farmid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetPlantDiaryList")]
        public ActionResult<IEnumerable<DiaryDTO>> GetPlantDiaryList(int plantid)
        {
            try
            {
                var milestoneList = _diaryService.GetAllPlantDiary(plantid);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetPlantDiaryListByType")]
        public ActionResult<IEnumerable<DiaryDTO>> GetPlantDiaryListByType(int plantid, int type)
        {
            try
            {
                var milestoneList = _diaryService.GetAllPlantDiaryByType(plantid,type);
                return Ok(milestoneList);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpGet("GetDiaryById")]
        public ActionResult<DiaryDTO> GetDiaryById(int id)
        {
            try
            {
                var milestoneList = _diaryService.GetDiaryById(id);
                return Ok(milestoneList);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize(Roles = "Technician")]
        [HttpPut("UpdateDiaryImage")]
        public async Task<ActionResult<Diary>> UpdatePlantImage(DiaryOnlyImageDTO milestoneName)
        {
            try
            {
                string serverPath = Path.GetTempFileName();
                using (var stream = new FileStream(serverPath, FileMode.Create))
                {
                    milestoneName.Image.CopyTo(stream);
                }
                string saveLogoLink = await _drive.CreateDriveFile(serverPath);


                Diary milestoneList = _diaryService.GetRawDiaryById(milestoneName.Id);
                await _drive.DeleteDriveFile(milestoneList.Image);

                milestoneList.Image = saveLogoLink;
                var result = _diaryService.UpdateDiary(milestoneList);

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
        [HttpPut("UpdateDiary")]
        public ActionResult<Diary> UpdateDiary(Diary milestoneName)
        {
            try
            {
                Diary diary = _diaryService.GetRawDiaryById(milestoneName.Id);
                diary.PlantId = milestoneName.PlantId;
                diary.Type = milestoneName.Type;
                diary.Title = milestoneName.Title;
                diary.Body = milestoneName.Body;
                diary.LastUpdate = DateTime.Now;


                var milestoneList = _diaryService.UpdateDiary(diary);

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
        [HttpDelete("DeleteDiary")]
        public ActionResult DeleteDiary(int milestoneId)
        {
            try
            {
                var milestoneList = _diaryService.DeleteDiary(milestoneId);

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
        [HttpPost("CreateDiary")]
        public async Task<ActionResult<Diary>>  CreateDiary(DiaryImageDTO milestoneName)
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
                
               
                Diary diary = new Diary();
                diary.PlantId= milestoneName.PlantId;
                diary.Title = milestoneName.Title;  
                diary.Type = milestoneName.Type;
                diary.Body = milestoneName.Body;
                diary.Image = saveLogoLink;

                diary.CreatedDay = DateTime.Now;
                diary.LastUpdate = DateTime.Now;
                diary.Status = true;

                var milestoneList = _diaryService.CreateDiary(diary);

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
