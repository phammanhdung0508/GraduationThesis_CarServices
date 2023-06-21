using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_bookingServices.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        public readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-booking")]
        public async Task<IActionResult> ViewAllBooking(PageDto page)
        {
            var list = await bookingService.View(page)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("get-garage-bookings")]
        public async Task<IActionResult> GetReviewPerGarage(PagingBookingPerGarageRequestDto requestDto)
        {
            var list = await bookingService.FilterBookingByGarageId(requestDto)!;
            return Ok(list);
        }

        [HttpPost("filter-booking-by-customer")]
        public async Task<IActionResult> FilterBookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            var list = await bookingService.FilterBoookingByCustomer(requestDto);
            return Ok(list);
        }

        // [Authorize(Roles = "Customer")]
        [HttpPost("check-booking")]
        public async Task<IActionResult> CheckBooking(BookingCheckRequestDto bookingCheckRequestDto)
        {
            var list = await bookingService.IsBookingAvailable(bookingCheckRequestDto);
            return Ok(list);
        }

        // [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-booking/{id}")]
        public async Task<IActionResult> DetailBooking(int id)
        {
            var car = await bookingService.Detail(id);
            return Ok(car);
        }

        // [Authorize(Roles = "Customer")]
        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            await bookingService.Create(bookingCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPost("generate-qr-code/{bookingId}")]
        public async Task<IActionResult> GenerateQRCode(int bookingId)
        {
            var qrString = await bookingService.GenerateQRCode(bookingId);
            return Ok(qrString);
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("update-status-booking/{bookingId}&{bookingStatus}")]
        public async Task<IActionResult> UpdateBooking(int bookingId, int bookingStatus)
        {
            await bookingService.UpdateStatus(bookingId, (BookingStatus)bookingStatus);
            throw new MyException("Successfully.", 200);
        }
    }
}