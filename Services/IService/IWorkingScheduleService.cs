using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IWorkingScheduleService
    {
        Task<List<WorkingScheduleListResponseDto>?> View(PageDto page);
        Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleByGarage(int id);
        Task<List<WorkingScheduleByMechanicDto>?> FilterWorkingScheduleByMechanic(int id);
        Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleWhoAvailable(int id);
        Task<WorkingScheduleDetailResponseDto?> Detail(int id);
        Task<bool> Create(WorkingScheduleCreateRequestDto requestDto);
        Task<bool> UpdateStatus(WorkingScheduleUpdateStatusDto requestDto);
    }
}