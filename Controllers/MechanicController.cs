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

        /// <summary>
        /// View all mechanics. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-mechanic")]
        public async Task<IActionResult> ViewAll(PageDto page)
        {
            var list = await mechanicService.View(page);
            return Ok(list);
        }

        /// <summary>
        /// Filter Mechanics by specific garage. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("get-garage-mechanic/{garageId}")]
        public async Task<IActionResult> FilterMechanics(int garageId)
        {
            var list = await mechanicService.FilterMechanicsByGarageId(garageId);
            return Ok(list);
        }

        /// <summary>
        /// View detail a specific Mechainc.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-mechanic/{mechanicId}")]
        public async Task<IActionResult> DetailMechanic(int mechanicId)
        {
            var mechanic = await mechanicService.Detail(mechanicId);
            return Ok(mechanic);
        }

        /// <summary>
        /// Allow Customers to view which mechanic has been applied to their booking. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("get-mechanic-by-booking/{bookingId}")]
        public async Task<IActionResult> GetMechanicByBooking(int bookingId)
        {
            var list = await mechanicService.GetMechanicByBooking(bookingId);

            return Ok(list);
        }

        /// <summary>
        /// Filter mechanic working on the specific garage. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("get-mechanic-by-garage/{garageId}")]
        public async Task<IActionResult> GetMechanicByGarage(int garageId)
        {
            var list = await mechanicService.GetMechanicByGarage(garageId);

            return Ok(list);
        }
    }
}