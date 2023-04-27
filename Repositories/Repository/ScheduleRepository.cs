using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ScheduleRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ScheduleDto>?> View(PageDto page)
        {
            try
            {
                List<Schedule> list = await PagingConfiguration<Schedule>.Create(context.Schedules, page);
                return mapper.Map<List<ScheduleDto>>(list);
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
                ScheduleDto schedule = mapper.Map<ScheduleDto>(await context.Schedules.FirstOrDefaultAsync(c => c.ScheduleId == id));
                return schedule;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateScheduleDto scheduleDto)
        {
            try
            {
                Schedule schedule = mapper.Map<Schedule>(scheduleDto);
                context.Schedules.Add(schedule);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateScheduleDto scheduleDto)
        {
            try
            {
                var schedule = context.Schedules.FirstOrDefault(c => c.ScheduleId == scheduleDto.ScheduleId)!;
                mapper.Map<UpdateScheduleDto, Schedule?>(scheduleDto, schedule);
                context.Schedules.Update(schedule);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteScheduleDto scheduleDto)
        {
            try
            {
                var schedule = context.Schedules.FirstOrDefault(c => c.ScheduleId == scheduleDto.ScheduleId)!;
                mapper.Map<DeleteScheduleDto, Schedule?>(scheduleDto, schedule);
                context.Schedules.Update(schedule);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}