using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingRepository
    {
        Task<(List<Booking>?, int count)> View(PageDto page);
        Task<Booking?> Detail(int id);
        Task<int> Create(Booking booking);
        Task Update(Booking booking);
        Task<List<Booking>?> FilterBookingByDate(DateTime dateSelect, int garageId);
        Task<List<Booking>?> FilterBookingByDateCheck(DateTime dateSelect, int garageId);
        Task<int> CountBookingByTimePerDay(DateTime dateTime, int garageId);
        Task<bool> IsBookingExist(int bookingId);
        Task<(List<Booking>, int count)> FilterBookingByGarage(int garageId, PageDto page);
        Task<(List<Booking>?, int count)> FilterBookingByCustomer(int userId, PageDto page);
        Task<(List<Booking>?, int count)> SearchByBookingCode(int userId, string search, PageDto page);
        Task<(List<Booking>?, int count)> FilterBookingByStatus(BookingStatus? status, PageDto page, int? garageId);
        Task<(List<Booking>?, int count)> FilterBookingStatusAndDate(DateTime? dateFrom, DateTime? dateTo, BookingStatus? status, PageDto page);
        Task<(decimal, decimal, decimal, decimal, decimal, int, int, int, int)> CountRevenue(int garageId);
        Task<List<Booking>> FilterBookingByStatusCustomer(int bookingStatus, int userId);
        Task<Booking> DetailBookingForCustomer(int bookingId);
        Task<List<Booking>> FilterBookingByGarage(int garageId);
        Task<List<Booking>> FilterListBookingByGarageAndDate(int garageId, DateTime date);
        Task<(int, int, int, int, int)> CountBookingPerStatus(int? garageId);
        Task ConfirmBookingArePaid(int bookingId);
        Task<int?> GetRole(int userId);
        Task<List<Booking>> GetBookingByGarageCalendar(int? garageId);
        Task<Garage?> GetGarage(string bookingCode);
        Task<string?> GetBookingCodeByBookingId(int bookingId);
        Task<List<Booking>> GetBookingByBookingCode(string bookingCode);
    }
}