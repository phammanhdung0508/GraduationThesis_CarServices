using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<List<BookingListResponseDto>?> View(PageDto page);
        Task<BookingDetailResponseDto?> Detail(int id);
        Task<bool> Create(BookingCreateRequestDto requestDto);
        Task<bool> Update(BookingUpdateRequestDto requestDto);
    }
}