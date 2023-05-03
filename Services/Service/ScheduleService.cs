using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IMapper mapper;
        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.scheduleRepository = scheduleRepository;
        }

        public async Task<List<ScheduleDto>?> View(PageDto page)
        {
            try
            {
                List<ScheduleDto>? list = await scheduleRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ScheduleDto?> Detail(int id)
        {
            try
            {
                ScheduleDto? schedule = mapper.Map<ScheduleDto>(await scheduleRepository.Detail(id));
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateScheduleDto createScheduleDto)
        {
            try
            {
                // await scheduleRepository.Create(createScheduleDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateScheduleDto updateScheduleDto)
        {
            try
            {
                await scheduleRepository.Update(updateScheduleDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteScheduleDto deleteScheduleDto)
        {
            try
            {
                await scheduleRepository.Delete(deleteScheduleDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}