using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/mechanic")]
    public class MechanicController : ControllerBase
    {
        private readonly IMechanicService mechanicService;
        public MechanicController(IMechanicService mechanicService)
        {
            this.mechanicService = mechanicService;
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("get-garage-mechanic/{garageId}")]
        public async Task<IActionResult> FilterMechanics(int garageId)
        {
            var list = await mechanicService.FilterMechanicsByGarageId(garageId);
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-mechanic/{mechanicId}")]
        public async Task<IActionResult> DetailMechanic(int mechanicId)
        {
            var mechanic = await mechanicService.Detail(mechanicId);
            return Ok(mechanic);
        }

        [Authorize(Roles = "Staff")]
        [HttpGet("get-working-schedule-mechanic/{mechanicId}")]
        public async Task<IActionResult> FilterWorkingSchedules(int mechanicId)
        {
            var list = await mechanicService.FilterWorkingSchedulesByMechanicId(mechanicId);
            return Ok(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-mechanic")]
        public async Task<IActionResult> View(PageDto page)
        {
            var list = await mechanicService.View(page)!;
            return Ok(list);
        }
    }
}