using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<List<BookingResponseDto>?> View(PageDto page);
        Task<BookingResponseDto?> Detail(int id);
        Task<bool> Create(CreateRequestBookingDto createBookingDto);
        Task<bool> Update(UpdateBookingDto updateBookingDto);
        Task<bool> Delete(DeleteBookingDto deleteBookingDto);
    }
}