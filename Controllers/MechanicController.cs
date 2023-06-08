using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MechanicController : ControllerBase
    {
        private readonly IMechanicService mechanicService;
        public MechanicController(IMechanicService mechanicService)
        {
            this.mechanicService = mechanicService;
        }

        [HttpGet("get-garage-mechanic/{garageId}")]
        public async Task<IActionResult> FilterMechanics(int garageId)
        {
            var list = await mechanicService.FilterMechanicsByGarageId(garageId);
            return Ok(list);
        }

        [HttpGet("detail-mechanic/{mechanicId}")]
        public async Task<IActionResult> DetailMechanic(int mechanicId)
        {
            var mechanic = await mechanicService.Detail(mechanicId);
            return Ok(mechanic);
        }

        [HttpGet("get-working-schedule-mechanic/{mechanicId}")]
        public async Task<IActionResult> FilterWorkingSchedules(int mechanicId)
        {
            var list = await mechanicService.FilterWorkingSchedulesByMechanicId(mechanicId);
            return Ok(list);
        }

        [HttpPost("view-all-mechanic")]
        public async Task<IActionResult> View(PageDto page)
        {
            var list = await mechanicService.View(page)!;
            return Ok(list);
        }
    }
}