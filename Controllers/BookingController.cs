using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Notification;
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
        /// View detail of a specific Booking. [Staff]
        /// </summary>
        [Authorize(Roles = "Staff")]
        [Route("detail-booking-for-staff/{bookingId}")]
        [AcceptVerbs("GET")]
        public async Task<IActionResult> DetailBookingForStaff(int bookingId)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int garageId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "garageId")!.Value);

            var booking = await bookingService.RunQRCode(bookingId, garageId);
            return Ok(booking);
        }

        /// <summary>
        /// Filter list booking by date for staff. [Staff]
        /// </summary>
        [Authorize(Roles = "Staff")]
        [HttpGet("get-booking-by-date-for-staff")]
        public async Task<IActionResult> FilterListBookingByGarageAndDate([FromQuery][Required] string date)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int garageId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "garageId")!.Value);

            var list = await bookingService.FilterListBookingByGarageAndDate(garageId, date);

            return Ok(list);
        }

        /// <summary>
        /// View revenue of a specific Garage. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("get-revenue-by-garage/{garageId}")]
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
        [Authorize(Roles = "Admin, Manager")]
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
        /// Creates new a Booking for Manager. [Manager]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post create new booking.
        ///     {
        ///         "customerName": "name",
        ///         "customerPhone": "phone",
        ///         "customerEmail": "email",
        ///         "dateSelected": "12/12/2023",
        ///         "timeSelected": "08:00:00",
        ///         "serviceList": [
        ///             {
        ///                 ServiceDetailId: 7,
        ///                 ProductId: 1
        ///             }
        ///         ],
        ///         "mechanicId": 0,
        ///         "carId": 1,
        ///         "garageId": 1,
        ///         "couponId": 0,
        ///
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost("create-booking-for-manager")]
        public async Task<IActionResult> CreateBookingForManager(BookingCreateForManagerRequestDto requestDto)
        {
            await bookingService.CreateForManager(requestDto);
            throw new MyException("Thành công.", 200);
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
        ///         "dateSelected": "12/12/2023",
        ///         "timeSelected": "08:00:00",
        ///         "serviceList": [
        ///             1
        ///         ],
        ///         "mechanicId": 0,
        ///         "carId": 1,
        ///         "garageId": 1,
        ///         "couponId": 0,
        ///
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Customer")]
        [HttpPost("create-booking")]
        public async Task<IActionResult> CreateBooking(BookingCreateRequestDto bookingCreateRequestDto)
        {
            var response = await bookingService.Create(bookingCreateRequestDto);
            return Ok(response);
        }

        /// <summary>
        /// Update booking status. [Staff]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Booking status.
        ///     {
        ///         "Pending": 0,
        ///         "Canceled": 1,
        ///         "CheckIn": 2,
        ///         "Completed": 4,
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Customer, Staff, Admin, Manager")]
        [HttpPut]
        [Route("update-status-booking/{bookingId}&{bookingStatus}")]
        // [AcceptVerbs("PUT")]
        public async Task<IActionResult> UpdateBookingStatus([FromRoute] int bookingId, [FromRoute] int bookingStatus)
        {
            await bookingService.UpdateStatus(bookingId, (BookingStatus)bookingStatus);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Count Bookings for every booking status. [Admin]
        /// </summary>
        [HttpGet("count-booking-per-status")]
        public async Task<IActionResult> CountBookingPerStatus(int? garageId)
        {
            var count = await bookingService.CountBookingPerStatus(garageId);

            return Ok(count);
        }

        /// <summary>
        /// Get booking detail status by booking for customer. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("get-booking-detail-status-by-booking-customer/{bookingId}")]
        public async Task<IActionResult> GetBookingDetailStatusByBooking(int bookingId)
        {
            var list = await bookingService.GetBookingDetailStatusByBooking(bookingId);

            return Ok(list);
        }

        /// <summary>
        /// Get booking detail status by booking for staff. [Staff]
        /// </summary>
        [Authorize(Roles = "Staff")]
        [HttpGet("get-booking-detail-status-by-booking-staff/{bookingId}")]
        public async Task<IActionResult> GetBookingServiceStatusByBooking(int bookingId)
        {
            var booking = await bookingService.GetBookingServiceStatusByBooking(bookingId);

            return Ok(booking);
        }

        /// <summary>
        /// Update a booking detail status. [Staff]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put status:
        ///     {
        ///         "NotStart": "0",
        ///         "Done": "1",
        ///         "Error": "2"
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Staff, Admin, Manager")]
        [HttpPut("update-booking-detail-status/{bookingDetailId}&{status}")]
        public async Task<IActionResult> UpdateBookingDetailStatus(int bookingDetailId, int status)
        {
            await bookingService.UpdateBookingDetailStatus(bookingDetailId, status);
            throw new MyException("Thành công.", 200);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPut("confirm-booking-are-paid/{bookingId}")]
        public async Task<IActionResult> ConfirmBookingArePaid(int bookingId)
        {
            await bookingService.ConfirmBookingArePaid(bookingId);
            throw new MyException("Thành công.", 200);
        }


        /// <summary>
        /// Update a specific booking detail. [Admin, Manager]
        /// </summary>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPut("update-booking-detail-for-manager/{bookingDetailId}&{productId}")]
        public async Task<IActionResult> UpdateBookingDetailForManager(int bookingDetailId, int productId)
        {
            await bookingService.UpdateBookingDetailForManager(bookingDetailId, productId);
            throw new MyException("Thành công.", 200);
        }
    }
}