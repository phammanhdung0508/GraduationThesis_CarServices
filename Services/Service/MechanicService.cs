using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class MechanicService : IMechanicService
    {
        private readonly IMapper mapper;
        private readonly IMechanicRepository mechanicRepository;
        private readonly IGarageRepository garageRepository;

        public MechanicService(IMapper mapper, IMechanicRepository mechanicRepository, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.mechanicRepository = mechanicRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<List<MechanicListResponseDto>> View(PageDto page)
        {
            try
            {
                var list = mapper.Map<List<MechanicListResponseDto>>(await mechanicRepository.View(page));

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

        public async Task<List<MechanicListResponseDto>> FilterMechanicsByGarageId(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var list = mapper.Map<List<MechanicListResponseDto>>(await mechanicRepository.FilterMechanicsByGarageId(garageId));

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

        public async Task<MechanicDetailResponseDto?> Detail(int mechanicId)
        {
            try
            {
                var mechanic = mapper.Map<MechanicDetailResponseDto>(await mechanicRepository.Detail(mechanicId));

                switch (false)
                {
                    case var isExist when isExist == (mechanic != null):
                        throw new MyException("The mechanic doesn't exist.", 404);
                }

                return mechanic;
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

        public async Task<List<WorkingScheduleListResponseDto>> FilterWorkingSchedulesByMechanicId(int mechanicId)
        {
            try
            {
                var isMechanicExist = await mechanicRepository.IsMechanicExist(mechanicId);

                switch (false)
                {
                    case var isExist when isExist == isMechanicExist:
                        throw new MyException("The mechanic doesn't exist.", 404);
                }

                var list = mapper.Map<List<WorkingScheduleListResponseDto>>(await mechanicRepository.FilterWorkingSchedulesByMechanicId(mechanicId));

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
    }
}