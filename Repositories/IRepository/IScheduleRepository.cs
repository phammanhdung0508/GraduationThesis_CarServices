using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IScheduleRepository
    {
        Task<List<ScheduleDto>?> View(PageDto page);
        Task<ScheduleDto?> Detail(int id);
        Task Create(Schedule schedule);
        Task Update(UpdateScheduleDto scheduleDto);
        Task Delete(DeleteScheduleDto scheduleDto);
    }
}