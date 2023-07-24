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

        /// <summary>
        /// View detail of a specific Booking.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-booking/{id}")]
        public async Task<IActionResult> DetailBooking(int id)
        {
            var car = await bookingService.Detail(id);
            return Ok(car);
        }

        /// <summary>
        /// View revenue of a specific Garage.
        /// </summary>
        [HttpGet("get-revenue-by-garage/garageId={garageId}")]
        public async Task<IActionResult> CountRevune(int garageId)
        {
            var revenue = await bookingService.CountRevune(garageId);
            return Ok(revenue);
        }

        /// <summary>
        /// Calculate check out the price for Booking.
        /// </summary>
        [HttpPost("get_check_out")]
        public async Task<IActionResult> CheckOut(CheckOutRequestDto requestDto)
        {
            var list = await bookingService.CheckOut(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// Search bookings by booking code. [Admin]
        /// </summary>
        [HttpPost("search-service-by-code")]
        public async Task<IActionResult> SearchByBookingCode(SearchBookingByUserRequestDto requestDto)
        {
            var list = await bookingService.SearchByBookingCode(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// View all Booking. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-booking")]
        public async Task<IActionResult> ViewAllBooking(PageDto page)
        {
            var list = await bookingService.View(page)!;
            return Ok(list);
        }

        /// <summary>
        /// Filter Bookings by specific Garage. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("filter-booking-by-garage")]
        public async Task<IActionResult> FilterBookingByGarage(PagingBookingPerGarageRequestDto requestDto)
        {
            var list = await bookingService.FilterBookingByGarageId(requestDto)!;
            return Ok(list);
        }

        /// <summary>
        /// Filter Bookings by booking status. [Admin]
        /// </summary>
        [HttpPost("filter-booking-by-status")]
        public async Task<IActionResult> FilterBookingByStatus(FilterByStatusRequestDto requestDto)
        {
            var list = await bookingService.FilterBookingByStatus(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// Dynamic filter Bookings by date and booking status. [Admin]
        /// </summary>
        [HttpPost("filter-booking-by-date-and-status")]
        public async Task<IActionResult> FilterBookingStatusAndDate(FilterByStatusAndDateRequestDto requestDto)
        {
            var list = await bookingService.FilterBookingStatusAndDate(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// Filter Bookings by specific Customer.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("filter-booking-by-customer")]
        public async Task<IActionResult> FilterBookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            var list = await bookingService.FilterBoookingByCustomer(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// Check if the specific dates a user has chosen there are any times available or not.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("check-booking")]
        public async Task<IActionResult> CheckBooking(BookingCheckRequestDto bookingCheckRequestDto)
        {
            var list = await bookingService.IsBookingAvailable(bookingCheckRequestDto);
            return Ok(list);
        }

        /// <summary>
        /// Creates new a Booking.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            await bookingService.Create(bookingCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Creates new a Booking.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("generate-qr-code/{bookingId}")]
        public async Task<IActionResult> GenerateQRCode(int bookingId)
        {
            var qrString = await bookingService.GenerateQRCode(bookingId);
            return Ok(qrString);
        }

        /// <summary>
        /// Creates new a Booking.
        /// </summary>
        [Authorize(Roles = "Staff")]
        [HttpPut]
        [Route("update-status-booking/{bookingId}&{bookingStatus}")]
        // [AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateBooking([FromRoute] int bookingId, [FromRoute] int bookingStatus)
        {
            await bookingService.UpdateStatus(bookingId, (BookingStatus)bookingStatus);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Run a specific function after a user scan QR code.
        /// </summary>
        // [HttpGet]
        [Authorize(Roles = "Staff")]
        [Route("run-qr/{bookingId}")]
        [AcceptVerbs("GET")]
        public async Task<IActionResult> RunQR(int bookingId)
        {
            await bookingService.RunQRCode(bookingId);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Count Bookings for every booking status.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("count-booking-per-status")]
        public async Task<IActionResult> CountBookingPerStatus()
        {
            var count = await bookingService.CountBookingPerStatus();
            return Ok(count);
        }
    }
}