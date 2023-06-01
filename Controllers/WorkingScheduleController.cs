using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkingScheduleController : ControllerBase
    {
        private readonly IWorkingScheduleService workingScheduleService;
        public WorkingScheduleController(IWorkingScheduleService workingScheduleService)
        {
            this.workingScheduleService = workingScheduleService;
        }

        [HttpPost("view-all-working-schedule")]
        public async Task<IActionResult> ViewWorkingSchedule(PageDto page)
        {
            try
            {
                var list = await workingScheduleService.View(page)!;
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

        [HttpGet("get-working-schedule-by-garage/id={garageId}&day={daysOfTheWeek}")]
        public async Task<IActionResult> GetWorkingScheduleByGarage(int garageId, string daysOfTheWeek)
        {
            try
            {
                var workingSchedule = await workingScheduleService.FilterWorkingScheduleByGarage(garageId, daysOfTheWeek)!;
                return Ok(workingSchedule);
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

        [HttpGet("get-working-schedule-by-mechanic/{id}")]
        public async Task<IActionResult> GetWorkingScheduleByMechanic(int id)
        {
            try
            {
                var workingSchedule = await workingScheduleService.FilterWorkingScheduleByMechanic(id)!;
                return Ok(workingSchedule);
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

        [HttpGet("get-working-schedule-who-available/id={garageId}&day={daysOfTheWeek}")]
        public async Task<IActionResult> GetWorkingScheduleWhoAvailable(int garageId, string daysOfTheWeek)
        {
            try
            {
                var workingSchedule = await workingScheduleService.FilterWorkingScheduleWhoAvailable(garageId, daysOfTheWeek)!;
                return Ok(workingSchedule);
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

        [HttpGet("detail-working-schedule/{id}")]
        public async Task<IActionResult> DetailWorkingSchedule(int id)
        {
            try
            {
                var workingSchedule = await workingScheduleService.Detail(id);
                return Ok(workingSchedule);
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

        [HttpPost("create-working-schedule")]
        public async Task<IActionResult> CreateWorkingSchedule(WorkingScheduleCreateRequestDto workingScheduleCreateDto)
        {
            try
            {
                if (await workingScheduleService.Create(workingScheduleCreateDto))
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

        [HttpPut("update-working-schedule-status")]
        public async Task<ActionResult> UpdateWorkingScheduleStatus(WorkingScheduleUpdateStatusDto workingScheduleUpdateStatusDto)
        {
            try
            {
                if (await workingScheduleService.UpdateStatus(workingScheduleUpdateStatusDto))
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