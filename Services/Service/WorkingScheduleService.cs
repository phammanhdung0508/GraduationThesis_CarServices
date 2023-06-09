using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class WorkingScheduleService : IWorkingScheduleService
    {
        private readonly IWorkingScheduleRepository workingScheduleRepository;
        private readonly IMapper mapper;
        public WorkingScheduleService(IWorkingScheduleRepository workingScheduleRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.workingScheduleRepository = workingScheduleRepository;
        }

        public async Task<List<WorkingScheduleListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<WorkingScheduleListResponseDto>>(await workingScheduleRepository.View(page));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleByGarage(int garageId, string daysOfTheWeek)
        {
            try
            {
                var list = mapper
                .Map<List<WorkingScheduleByGarageDto>>(await workingScheduleRepository.FilterWorkingScheduleByGarage(garageId, daysOfTheWeek));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<List<WorkingScheduleByMechanicDto>?> FilterWorkingScheduleByMechanic(int mechanicId)
        {
            try
            {
                var list = mapper
                .Map<List<WorkingScheduleByMechanicDto>>(await workingScheduleRepository.FilterWorkingScheduleByMechanic(mechanicId));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<List<WorkingScheduleByGarageDto>?> FilterWorkingScheduleWhoAvailable(int garageId, string daysOfTheWeek)
        {
            try
            {
                var list = mapper
                .Map<List<WorkingScheduleByGarageDto>>(await workingScheduleRepository.FilterWorkingScheduleWhoAvailable(garageId, daysOfTheWeek));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<WorkingScheduleDetailResponseDto?> Detail(int id)
        {
            try
            {
                var workingSchedule = mapper
                .Map<WorkingScheduleDetailResponseDto>(await workingScheduleRepository.Detail(id));

                return workingSchedule;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Create(WorkingScheduleCreateRequestDto requestDto)
        {
            try
            {
                var workingSchedule = mapper.Map<WorkingScheduleCreateRequestDto, WorkingSchedule>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.WorkingScheduleStatus = WorkingScheduleStatus.NotAvailable;
                }));
                if (checkValidatedDaysOfTheWeek(requestDto.DaysOfTheWeek)) {
                    await workingScheduleRepository.Create(workingSchedule);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdateStatus(WorkingScheduleUpdateStatusDto requestDto)
        {
            try
            {
                var w = await workingScheduleRepository.Detail(requestDto.WorkingScheduleId);
                var workingSchedule = mapper.Map<WorkingScheduleUpdateStatusDto, WorkingSchedule>(requestDto, w!);
                await workingScheduleRepository.Update(workingSchedule);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public Boolean checkValidatedDaysOfTheWeek(string daysOfTheWeek)
        {
            switch (daysOfTheWeek)
            {
                case "Monday":
                    return true;
                case "Tuesday":
                    return true;
                case "Wednesday":
                    return true;
                case "Thursday":
                    return true;
                case "Friday":
                    return true;
                case "Saturday":
                    return true;
                case "Sunday":
                    return true;
                default:
                    return false;

            }
        }

    }
}