using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.PaymentGateway;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<GenericObject<List<BookingListResponseDto>>> View(PageDto page);
        Task<GenericObject<List<BookingListResponseDto>>> ViewAndFilter(ViewAllAndFilterBooking page);
        Task<List<BookingPerHour>> IsBookingAvailable(BookingCheckRequestDto requestDto);
        Task<BookingDetailResponseDto?> Detail(int id);
        /*Task CreateForManager(BookingCreateForManagerRequestDto requestDto);*/
        Task<PaymentLinkDto> Create(BookingCreateRequestDto requestDto);
        Task UpdateStatus(int bookingId, BookingStatus bookingStatus, int userId);
        Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByGarageId(PagingBookingPerGarageRequestDto requestDto);
        Task<GenericObject<List<FilterByCustomerResponseDto>>> FilterBoookingByCustomer(FilterByCustomerRequestDto requestDto);
        Task<BookingDetailForStaffResponseDto> RunQRCode(int bookingId, int garageId);
        Task<GenericObject<List<BookingListResponseDto>>> SearchByBookingCode(SearchBookingByUserRequestDto requestDto);
        Task<GenericObject<List<BookingListResponseDto>>> FilterBookingByStatus(FilterByStatusRequestDto requestDto);
        Task<GenericObject<List<BookingListResponseDto>>> FilterBookingStatusAndDate(FilterByStatusAndDateRequestDto requestDto);
        Task<BookingRevenueResponseDto> CountRevune(int garageId);
        Task<CheckOutResponseDto> CheckOut(CheckOutRequestDto requestDto);
        Task<List<FilterByBookingStatusResponseDto>> FilterBookingByStatusCustomer(int bookingStatus, int userId);
        Task<BookingDetailForCustomerResponseDto> DetailBookingForCustomer(int bookingId);
        Task<List<BookingDetailStatusForBookingResponseDto>> GetBookingDetailStatusByBooking(int bookingId);
        Task<List<HourDto>> FilterListBookingByGarageAndDate(int bookingId, string date);
        Task<BookingServiceStatusForStaffResponseDto> GetBookingServiceStatusByBooking(int bookingId);
        Task<BookingCountResponseDto> CountBookingPerStatus(int? garageId);
        Task UpdateBookingDetailStatus(int bookingDetailId, int status);
        Task ConfirmBookingArePaid(int bookingId);
        Task UpdateBookingDetailForManager(int bookingDetailId, int productId);
        Task<List<BookingListByCalenderResponseDto>> GetBookingByGarageCalendar(int? garageId);
        Task ConfirmChangeInBookingDetail(int bookingId, bool isAccepted);
        Task CreateWarrantyForBooking(BookingCreateWarrantyRequestDto requestDto);
        Task<BookingDetailWarrantyDto> GetBookingByBookingId(int bookingId);
    }
}