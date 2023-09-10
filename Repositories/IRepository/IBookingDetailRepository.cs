using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingDetailRepository
    {
        Task<List<BookingDetail>> FilterBookingDetailByBookingId(int bookingId);
        Task<bool> IsBookingWaitForAccept(int bookingId);
        Task<List<BookingDetail>> FilterAllBookingDetailByBooking(int bookingId);
        Task Create(List<BookingDetail> serviceBooking);
        Task Update(List<BookingDetail> serviceBookings);
        Task<BookingDetail?> Detail(int bookingDetailId);
        Task Delete(BookingDetail bookingDetail);
    }
}