using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingRepository
    {
        Task<List<BookingResponseDto>?> View(PageDto page);
        Task<BookingResponseDto?> Detail(int id);
        Task Create(Booking booking);
        Task Update(UpdateBookingDto bookingDto);
        Task Delete(DeleteBookingDto bookingDto);
    }
}