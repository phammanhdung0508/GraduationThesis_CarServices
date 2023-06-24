// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;

// namespace GraduationThesis_CarServices.Services.IService
// {
//     public interface IWorkingScheduleService
//     {
//         Task<List<WorkingScheduleListResponseDto>?> View(PageDto page);
//         Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleByGarage(int garageId, string daysOfTheWeek);
//         Task<List<WorkingScheduleByMechanicDto>?> FilterWorkingScheduleByMechanic(int mechanicId);
//         Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleWhoAvailable(int garageId, string daysOfTheWeek);
//         Task<WorkingScheduleDetailResponseDto?> Detail(int id);
//         Task Create(WorkingScheduleCreateRequestDto requestDto);
//         Task UpdateStatus(WorkingScheduleUpdateStatusDto requestDto);
//     }
// }