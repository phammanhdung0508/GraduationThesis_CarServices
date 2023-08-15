using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
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
        /// Filter Mechanics by specific garage. [Admin, Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("filter-mechanic-by-garage/{garageId}")]
        public async Task<IActionResult> FilterMechanicsByGarage(int garageId)
        {
            var list = await mechanicService.FilterMechanicsByGarage(garageId);
            return Ok(list);
        }

        /// <summary>
        /// Filter Mechanics by specific garage. [Admin, Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("ffilter-mechanic-by-booking/{bookingId}")]
        public async Task<IActionResult> FilterMechanicsByBooking(int bookingId)
        {
            var list = await mechanicService.FilterMechanicsByBooking(bookingId);
            return Ok(list);
        }

        /// <summary>
        /// View detail a specific Mechainc. [Admin, Manager]
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

        /// <summary>
        /// Add avaliable mechanic to booking. [Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("add-mechanic-to-booking")]
        public async Task<IActionResult> AddAvaliableMechanicToBooking(EditMechanicBookingRequestDto requestDto)
        {
            await mechanicService.AddAvaliableMechanicToBooking(requestDto);
            throw new MyException("Successfully", 200);
        }

        /// <summary>
        /// Remove mechanic from booking. [Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("remove-mechanic-from-booking")]
        public async Task<IActionResult> RemoveMechanicfromBooking(EditMechanicBookingRequestDto requestDto)
        {
            await mechanicService.RemoveMechanicfromBooking(requestDto);
            throw new MyException("Successfully", 200);
        }

        /// <summary>
        /// View bookings having mechanic appplied[Admin]
        /// </summary>
        [HttpPost("get-booking-mechanic-applied")]
        public async Task<IActionResult> GetBookingMechanicApplied(FilterBookingByMechanicRequestDto requestDto)
        {
            var list = await mechanicService.GetBookingMechanicApplied(requestDto);
            return Ok(list);
        }
    }
}