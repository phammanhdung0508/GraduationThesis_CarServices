using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IMechanicService
    {
        Task<List<MechanicListResponseDto>> FilterMechanicsByGarageId(int garageId);
        Task<MechanicDetailResponseDto?> Detail(int mechanicId);
        Task<List<WorkingScheduleListResponseDto>> FilterWorkingSchedulesByMechanicId(int mechanicId);
    }
}