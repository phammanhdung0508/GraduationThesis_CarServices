using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IScheduleRepository
    {
        Task<List<ScheduleDto>?> View(PageDto page);
        Task<ScheduleDto?> Detail(int id);
        Task Create(CreateScheduleDto scheduleDto);
        Task Update(UpdateScheduleDto scheduleDto);
        Task Delete(DeleteScheduleDto scheduleDto);
    }
}