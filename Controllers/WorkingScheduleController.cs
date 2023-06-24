// using GraduationThesis_CarServices.Models.DTO.Exception;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.Search;
// using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
// using GraduationThesis_CarServices.Services.IService;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace GraduationThesis_CarServices.Controllers
// {
//     [ApiController]
//     [Route("api/working-schedule")]
//     public class WorkingScheduleController : ControllerBase
//     {
//         private readonly IWorkingScheduleService workingScheduleService;
//         public WorkingScheduleController(IWorkingScheduleService workingScheduleService)
//         {
//             this.workingScheduleService = workingScheduleService;
//         }

//         [Authorize(Roles = "Admin, Manager")]
//         [HttpPost("view-all-working-schedule")]
//         public async Task<IActionResult> ViewWorkingSchedule(PageDto page)
//         {
//             var workingScheduleList = await workingScheduleService.View(page)!;
//             return Ok(workingScheduleList);
//         }

//         [Authorize(Roles = "Admin, Manager")]
//         [HttpGet("get-working-schedule-by-garage/id={garageId}&day={daysOfTheWeek}")]
//         public async Task<IActionResult> GetWorkingScheduleByGarage(int garageId, string daysOfTheWeek)
//         {
//             var workingScheduleList = await workingScheduleService.FilterWorkingScheduleByGarage(garageId, daysOfTheWeek)!;
//             return Ok(workingScheduleList);
//         }

//         [Authorize(Roles = "Admin, Manager, Staff")]
//         [HttpGet("get-working-schedule-by-mechanic/{id}")]
//         public async Task<IActionResult> GetWorkingScheduleByMechanic(int id)
//         {
//             var workingScheduleList = await workingScheduleService.FilterWorkingScheduleByMechanic(id)!;
//             return Ok(workingScheduleList);
//         }

//         [Authorize(Roles = "Admin, Manager, Staff")]
//         [HttpGet("get-working-schedule-who-available/id={garageId}&day={daysOfTheWeek}")]
//         public async Task<IActionResult> GetWorkingScheduleWhoAvailable(int garageId, string daysOfTheWeek)
//         {
//             var workingScheduleList = await workingScheduleService.FilterWorkingScheduleWhoAvailable(garageId, daysOfTheWeek)!;
//             return Ok(workingScheduleList);
//         }

//         [Authorize(Roles = "Admin, Manager, Staff")]
//         [HttpGet("detail-working-schedule/{id}")]
//         public async Task<IActionResult> DetailWorkingSchedule(int id)
//         {
//             var workingSchedule = await workingScheduleService.Detail(id);
//             return Ok(workingSchedule);
//         }

//         [Authorize(Roles = "Manager")]
//         [HttpPost("create-working-schedule")]
//         public async Task<IActionResult> CreateWorkingSchedule(WorkingScheduleCreateRequestDto workingScheduleCreateDto)
//         {
//             await workingScheduleService.Create(workingScheduleCreateDto);
//             throw new MyException("Successfully.", 200);
//         }

//         [Authorize(Roles = "Manager")]
//         [HttpPut("update-working-schedule-status")]
//         public async Task<ActionResult> UpdateWorkingScheduleStatus(WorkingScheduleUpdateStatusDto workingScheduleUpdateStatusDto)
//         {
//             await workingScheduleService.UpdateStatus(workingScheduleUpdateStatusDto);
//             throw new MyException("Successfully.", 200);
//         }
//     }
// }