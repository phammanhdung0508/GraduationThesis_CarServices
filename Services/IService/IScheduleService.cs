using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IScheduleService
    {
        Task<List<ScheduleDto>?> View(PageDto page);
        Task<ScheduleDto?> Detail(int id);
        Task<bool> Create(CreateScheduleDto createScheduleDto);
        Task<bool> Update(UpdateScheduleDto updateScheduleDto);
        Task<bool> Delete(DeleteScheduleDto deleteScheduleDto);
    }
}