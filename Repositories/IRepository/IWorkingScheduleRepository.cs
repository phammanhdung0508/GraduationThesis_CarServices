using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IWorkingScheduleRepository
    {
        Task<List<WorkingSchedule>?> View(PageDto page);
        Task<List<WorkingSchedule>?> FilterWorkingScheduleByGarage(int garageId, PageDto page);
        Task<List<WorkingSchedule>?> FilterWorkingScheduleByMechanic(int mechanicId, PageDto page);
        Task<List<WorkingSchedule>?> FilterWorkingScheduleWhoAvailable(int garageId, PageDto page);
        Task<WorkingSchedule?> Detail(int id);
        Task Create(WorkingSchedule workingSchedule);
        Task Update(WorkingSchedule workingSchedule);
    }
}