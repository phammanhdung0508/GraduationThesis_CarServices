using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IMechanicService
    {
        Task<GenericObject<List<MechanicListResponseDto>>> View(PageDto page);
        Task<List<MechanicListResponseDto>> FilterMechanicsByGarage(int garageId);
        Task<List<MechanicListResponseDto>> FilterMechanicsByBooking(int bookingId);
        Task<MechanicDetailResponseDto?> Detail(int mechanicId);
        Task<List<MechanicWorkForBookingResponseDto>> GetMechanicByBooking(int bookingId);
        Task<List<MechanicWorkForGarageResponseDto>> GetMechanicByGarage(int garageId);
        // Task<List<WorkingScheduleListResponseDto>> FilterWorkingSchedulesByMechanicId(int mechanicId);
        Task AddAvaliableMechanicToBooking(EditMechanicBookingRequestDto requestDto);
        Task RemoveMechanicfromBooking(EditMechanicBookingRequestDto requestDto);
    }
}