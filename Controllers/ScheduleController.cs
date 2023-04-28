using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        public readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        [HttpPost("view-all-schedule")]
        public async Task<ActionResult<List<ScheduleDto>>> ViewCategory(PageDto page)
        {
            try
            {
                var list = await scheduleService.View(page)!;
                return Ok(list);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpGet("detail-schedule/{id}")]
        public async Task<ActionResult<ScheduleDto>> DetailCategory(int id)
        {
            try
            {
                var schedule = await scheduleService.Detail(id);
                return Ok(schedule);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPost("create-schedule")]
        public async Task<ActionResult<bool>> CreateCategory(CreateScheduleDto ScheduleDto)
        {
            try
            {
                if (await scheduleService.Create(ScheduleDto))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-schedule")]
        public async Task<ActionResult<bool>> UpdateCategory(UpdateScheduleDto ScheduleDto)
        {
            try
            {
                if (await scheduleService.Update(ScheduleDto))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("delete-schedule")]
        public async Task<ActionResult<bool>> DeleteCategory(DeleteScheduleDto ScheduleDto)
        {
            try
            {
                if (await scheduleService.Delete(ScheduleDto))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }
    }
}