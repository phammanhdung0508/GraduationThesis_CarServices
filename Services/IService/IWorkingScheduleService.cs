using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IWorkingScheduleService
    {
        Task<List<WorkingScheduleListResponseDto>?> View(PageDto page);
        Task<List<WorkingScheduleListResponseDto>?> FilterWorkingScheduleByGarage(int id, PageDto page);
        Task<List<WorkingScheduleListResponseDto>?> FilterWorkingScheduleByMechanic(int id, PageDto page);
        Task<List<WorkingScheduleListResponseDto>?> FilterWorkingScheduleWhoAvailable(int id, PageDto page);
        Task<WorkingScheduleDetailResponseDto?> Detail(int id);
        Task<bool> Create(WorkingScheduleCreateRequestDto requestDto);
        Task<bool> UpdateStatus(WorkingScheduleUpdateStatusDto requestDto);
    }
}