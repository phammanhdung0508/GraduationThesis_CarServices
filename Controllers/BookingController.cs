using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_bookingServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        public readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpPost("view-all-booking")]
        public async Task<IActionResult> ViewAllBooking(PageDto page)
        {
            var list = await bookingService.View(page)!;
            return Ok(list);
        }

        [HttpPost("get-garage-bookings")]
        public async Task<IActionResult> GerReviewPerGarage(PagingBookingPerGarageRequestDto requestDto)
        {
            var list = await bookingService.FilterBookingByGarageId(requestDto)!;
            return Ok(list);
        }

        [HttpPost("check-booking")]
        public async Task<IActionResult> CheckBooking(BookingCheckRequestDto bookingCheckRequestDto)
        {

            var list = await bookingService.IsBookingAvailable(bookingCheckRequestDto);
            return Ok(list);
        }

        [HttpGet("detail-booking/{id}")]
        public async Task<IActionResult> DetailBooking(int id)
        {
            var car = await bookingService.Detail(id);
            return Ok(car);
        }

        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            await bookingService.Create(bookingCreateRequestDto);
            throw new Exception("Successfully.");
        }

        [HttpPut("update-status-booking")]
        public async Task<IActionResult> UpdateBooking(BookingStatusRequestDto bookingStatusRequestDto)
        {
            await bookingService.UpdateStatus(bookingStatusRequestDto);
            throw new Exception("Successfully.");
        }
    }
}