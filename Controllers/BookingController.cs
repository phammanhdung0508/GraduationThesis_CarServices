using System.IdentityModel.Tokens.Jwt;
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
        /// View detail of a specific Booking. [Admin, Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-booking/{id}")]
        public async Task<IActionResult> DetailBooking(int id)
        {
            var booking = await bookingService.Detail(id);
            return Ok(booking);
        }

        /// <summary>
        /// View detail of a specific Booking. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("detail-booking-for-customer/{bookingId}")]
        public async Task<IActionResult> DetailBookingForCustomer(int bookingId)
        {
            var booking = await bookingService.DetailBookingForCustomer(bookingId);
            return Ok(booking);
        }

        /// <summary>
        /// View revenue of a specific Garage. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("get-revenue-by-garage/garageId={garageId}")]
        public async Task<IActionResult> CountRevune(int garageId)
        {
            var revenue = await bookingService.CountRevune(garageId);
            return Ok(revenue);
        }

        /// <summary>
        /// Filter overall booking by status for customer mobile. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("filter-booking-by-overall-status/{bookingStatus}")]
        public async Task<IActionResult> FilterBookingByStatusCustomer(int bookingStatus)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);
            
            var list = await bookingService.FilterBookingByStatusCustomer(bookingStatus, userId);
            return Ok(list);
        }

        /// <summary>
        /// Calculate check out the price for Booking. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
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
        /// Filter Bookings by specific Customer. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("filter-booking-by-customer")]
        public async Task<IActionResult> FilterBookingByCustomer(FilterByCustomerRequestDto requestDto)
        {
            var list = await bookingService.FilterBoookingByCustomer(requestDto);
            return Ok(list);
        }

        /// <summary>
        /// Check if the specific dates a user has chosen there are any times available or not. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("check-booking")]
        public async Task<IActionResult> CheckBooking(BookingCheckRequestDto bookingCheckRequestDto)
        {
            var list = await bookingService.IsBookingAvailable(bookingCheckRequestDto);
            return Ok(list);
        }

        /// <summary>
        /// Creates new a Booking. [Customer]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post create new booking.
        ///     {
        ///         "customerName": "name",
        ///         "customerPhone": "phone",
        ///         "customerEmail": "email",
        ///         "dateSelected": "MM/dd/yyyy",
        ///         "timeSelected": "hh:mm:ss",
        ///         "serviceList": [
        ///             {
        ///                 "serviceDetailId": 1
        ///             }
        ///         ],
        ///         "mechanicId": 1,
        ///         "carId": 1,
        ///         "garageId": 1,
        ///         "couponId": 1,
        ///
        ///         "versionNumber": "AAAAAAAAEBM=" /*version number get when call garage detail. Must have*/
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Customer")]
        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            await bookingService.Create(bookingCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Generate new QR code for Customer when they check-in. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("generate-qr-code/{bookingId}")]
        public async Task<IActionResult> GenerateQRCode(int bookingId)
        {
            await bookingService.GenerateQRCode(bookingId);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Update booking status.
        /// </summary>
        //[Authorize(Roles = "Staff")]
        [HttpPut]
        [Route("update-status-booking/{bookingId}&{bookingStatus}")]
        // [AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateBookingStatus([FromRoute] int bookingId, [FromRoute] int bookingStatus)
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
        /// Count Bookings for every booking status. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("count-booking-per-status")]
        public async Task<IActionResult> CountBookingPerStatus()
        {
            var count = await bookingService.CountBookingPerStatus();
            return Ok(count);
        }
    
        /// <summary>
        /// Count Bookings for every booking status. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("get-booking-detail-status-by-booking-customer/{bookingId}")]
        public async Task<IActionResult> GetBookingDetailStatusByBooking(int bookingId)
        {
            var list = await bookingService.GetBookingDetailStatusByBooking(bookingId);

            return Ok(list);
        }
    }
}